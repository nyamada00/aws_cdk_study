using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;

namespace AwsCdk.Resource
{
    internal class AlbResource
    {
        internal CfnLoadBalancer? LoadBalancer { get; private set; }
        private readonly CfnVPC? vpc;
        private readonly CfnSubnet? subnetPublic1a;
        private readonly CfnSubnet? subnetPublic1c;
        private readonly CfnSecurityGroup? securityGroupAlb;
        private readonly CfnInstance? ec2Instance1a;
        private readonly CfnInstance? ec2Instance1c;

        private AlbResource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AlbResource(Construct scope,
            CfnVPC vpc,
            CfnSubnet subnetPublic1a,
            CfnSubnet subnetPublic1c,
            CfnSecurityGroup securityGroupAlb,
            CfnInstance ec2Instance1a,
            CfnInstance ec2Instance1c
        ) : base()
        {
            this.vpc = vpc;
            this.subnetPublic1a = subnetPublic1a;
            this.subnetPublic1c = subnetPublic1c;
            this.securityGroupAlb = securityGroupAlb;
            this.ec2Instance1a = ec2Instance1a;
            this.ec2Instance1c = ec2Instance1c;

            CreateResources(scope);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        private void CreateResources(Construct scope)
        {
            LoadBalancer = CreateLoadBalancer(scope);
            var targetGroup = CreateTargetGroup(scope);
            CreateListener(scope, LoadBalancer, targetGroup);
        }

        /// <summary>
        /// LoadBalancer作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private CfnLoadBalancer CreateLoadBalancer(Construct scope)
        {
            return new CfnLoadBalancer(scope, "Alb", new CfnLoadBalancerProps
            {
                IpAddressType = "ipv4",
                Name = ResourceUtility.CreateResourceName(scope, "alb"),
                Scheme = "internet-facing",
                SecurityGroups = new[] { securityGroupAlb!.AttrGroupId },
                Subnets = new[] { subnetPublic1a!.Ref, subnetPublic1c!.Ref },
                Type = "application"
            });
        }

        /// <summary>
        /// TargetGroup作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private CfnTargetGroup CreateTargetGroup(Construct scope)
        {
            return new CfnTargetGroup(scope, "AlbTargetGroup", new CfnTargetGroupProps
            {
                Name = ResourceUtility.CreateResourceName(scope, "tg"),
                Port = 80,
                Protocol = "HTTP",
                TargetType = "instance",
                Targets = new[] {
                    new CfnTargetGroup.TargetDescriptionProperty {
                        Id=ec2Instance1a!.Ref
                    },
                    new CfnTargetGroup.TargetDescriptionProperty{
                        Id= ec2Instance1c!.Ref
                    }
                },
                VpcId = vpc!.Ref
            }
                );
        }

        /// <summary>
        /// Listener作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private static CfnListener CreateListener(Construct scope, CfnLoadBalancer loadBalancer, CfnTargetGroup targetGroup)
        {
            return new CfnListener(scope, "AlbListener", new CfnListenerProps
            {
                DefaultActions = new[]{
                    new CfnListener.ActionProperty{
                        Type= "forward",
                        ForwardConfig= new CfnListener.ForwardConfigProperty{
                            TargetGroups=new[]{
                                new CfnListener.TargetGroupTupleProperty{
                                    TargetGroupArn= targetGroup.Ref,
                                    Weight= 1
                                }
                            }
                        },
                    }
                },
                LoadBalancerArn = loadBalancer.Ref,
                Port = 80,
                Protocol = "HTTP"
            });
        }
    }
}