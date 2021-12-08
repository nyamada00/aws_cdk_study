using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;


namespace AwsCdk.Resource
{
    internal class SgResource : AbstractResource
    {
        private record IngressInfo(string Id, CfnSecurityGroupIngressProps SecurityGroupIngressProps, Func<string> GroupId, Func<string>? SourceSecurityGroupId);
        private record ResourceInfo(string Id, string GroupDescription, IngressInfo[] Ingresses, string ResourceName, Action<CfnSecurityGroup> Assign);

        public CfnSecurityGroup? alb;
        public CfnSecurityGroup? ec2;
        public CfnSecurityGroup? rds;

        private CfnVPC? vpc;

        private SgResource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SgResource(Construct scope, CfnVPC vpc) : base()
        {
            this.vpc = vpc;

            var resourcesInfos = new[]{
                new ResourceInfo(
                    "SecurityGroupAlb",
                    "for ALB",
                    new []{
                        new IngressInfo(
                            "SecurityGroupIngressAlb1",
                            new CfnSecurityGroupIngressProps{
                                IpProtocol= "tcp",
                                CidrIp= "0.0.0.0/0",
                                FromPort= 80,
                                ToPort= 80
                            },
                            () => this.alb!.AttrGroupId,
                            null
                        ),
                        new IngressInfo(
                            "SecurityGroupIngressAlb2",
                            new CfnSecurityGroupIngressProps {
                                IpProtocol= "tcp",
                                CidrIp= "0.0.0.0/0",
                                FromPort= 443,
                                ToPort= 443
                            },
                            () => this.alb!.AttrGroupId,
                            null
                        )
                    },
                    "sg-alb",
                    securityGroup => this.alb = securityGroup
                ),
                new ResourceInfo(
                    "SecurityGroupEc2",
                    "for EC2",
                    new []{
                        new IngressInfo(
                            "SecurityGroupIngressEc21",
                            new CfnSecurityGroupIngressProps {
                                IpProtocol= "tcp",
                                FromPort= 80,
                                ToPort= 80
                            },
                            () => this.ec2!.AttrGroupId,
                            () => this.alb!.AttrGroupId
                        )
                    },
                    "sg-ec2",
                    securityGroup => this.ec2 = securityGroup
                ),
                new ResourceInfo(
                    "SecurityGroupRds",
                    "for RDS",
                    new []{
                        new IngressInfo(
                            "SecurityGroupIngressRds1",
                            new CfnSecurityGroupIngressProps {
                                IpProtocol="tcp",
                                FromPort= 3306,
                                ToPort= 3306
                            },
                            () => this.rds!.AttrGroupId,
                            () => this.ec2!.AttrGroupId
                        )
                    },
                    "sg-rds",
                    securityGroup => this.rds = securityGroup
                )
            };

            CreateResources(scope, resourcesInfos);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourceInfoList"></param>
        private void CreateResources(Construct scope, ResourceInfo[] resourceInfoList)
        {
            foreach (var resourceInfo in resourceInfoList)
            {
                var securityGroup = this.createSecurityGroup(scope, resourceInfo);
                resourceInfo.Assign(securityGroup);

                this.createSecurityGroupIngress(scope, resourceInfo);
            }
        }

        /// <summary>
        /// SecurityGroup作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private CfnSecurityGroup createSecurityGroup(Construct scope, ResourceInfo resourcesInfo)
        {
            var resourceName = CreateResourceName(scope, resourcesInfo.ResourceName);
            var securityGroup = new CfnSecurityGroup(scope, resourcesInfo.Id, new CfnSecurityGroupProps
            {
                GroupDescription = resourcesInfo.GroupDescription,
                GroupName = resourceName,
                VpcId = this.vpc!.Ref,
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=resourceName
                    }
                }
            });

            return securityGroup;
        }

        private void createSecurityGroupIngress(Construct scope, ResourceInfo resourceInfo)
        {
            foreach (var ingress in resourceInfo.Ingresses)
            {
                var securityGroupIngress = new CfnSecurityGroupIngress(scope, ingress.Id, ingress.SecurityGroupIngressProps);
                securityGroupIngress.GroupId = ingress.GroupId();

                securityGroupIngress.SourceSecurityGroupId = ingress.SourceSecurityGroupId?.Invoke();
            }
        }
    }
}
