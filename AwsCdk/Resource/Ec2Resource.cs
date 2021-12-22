using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using Amazon.CDK.AWS.IAM;

namespace AwsCdk.Resource
{
    internal class Ec2Resource : AbstractResource
    {
        private record ResourceInfo(string Id, string AvailableZone, string ResourceName, Func<string> SubnetId, Action<CfnInstance> Assign);

        internal CfnInstance? Instance1a { get; private set; }
        internal CfnInstance? Instance1c { get; private set; }

        private const string LatestImageIdAmazonLinux2 = "ami-06631ebafb3ae5d34";
        private const string InstanceType = "t2.micro";
        private readonly CfnSubnet? subnetApp1a;
        private readonly CfnSubnet? subnetApp1c;
        private readonly CfnInstanceProfile? instanceProfileEc2;
        private readonly CfnSecurityGroup? securityGroupEc2;

        private Ec2Resource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Ec2Resource(Construct scope,
            CfnSubnet subnetApp1a,
            CfnSubnet subnetApp1c,
            CfnInstanceProfile instanceProfileEc2,
            CfnSecurityGroup securityGroupEc2
        ) : base()
        {
            this.subnetApp1a = subnetApp1a;
            this.subnetApp1c = subnetApp1c;
            this.instanceProfileEc2 = instanceProfileEc2;
            this.securityGroupEc2 = securityGroupEc2;

            var resourcesInfos = new[]{
                new ResourceInfo(
                    "Ec2Instance1a",
                    "ap-northeast-1a",
                    "ec2-1a",
                    ()=>this.subnetApp1a.Ref,
                    instance=>this.Instance1a=instance
                ),
                new ResourceInfo(
                    "Ec2Instance1c",
                    "ap-northeast-1c",
                    "ec2-1c",
                    ()=>this.subnetApp1c.Ref,
                    instance=>this.Instance1c=instance
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
                var instance = createInstance(scope, resourceInfo);
                resourceInfo.Assign(instance);
            }
        }

        /// <summary>
        /// Instance作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private CfnInstance createInstance(Construct scope, ResourceInfo resourcesInfo)
        {
            return new CfnInstance(scope, resourcesInfo.Id, new CfnInstanceProps
            {
                AvailabilityZone = resourcesInfo.AvailableZone,
                IamInstanceProfile = this.instanceProfileEc2!.Ref,
                ImageId = LatestImageIdAmazonLinux2,
                InstanceType = InstanceType,
                SecurityGroupIds = new[] { this.securityGroupEc2!.AttrGroupId },
                SubnetId = resourcesInfo.SubnetId(),
                Tags = new[]{
                    new CfnTag{Key="Name",Value=CreateResourceName(scope,resourcesInfo.ResourceName)}
                }
            });
        }
    }
}
