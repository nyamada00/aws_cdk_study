using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.ObjectModel;


namespace AwsCdk.Resource
{
    internal class SubnetResource : AbstractResource
    {
        private record ResourceInfo(string Id, string CidrBlock,string AvailabilityZone,string ResourceName,Action<CfnSubnet> Assign);

        internal CfnSubnet? SubnetPublic1a { get; private set; }
        internal CfnSubnet? SubnetPublic1c { get; private set; }
        internal CfnSubnet? SubnetApp1a { get; private set; }
        internal CfnSubnet? SubnetApp1c { get; private set; }
        internal CfnSubnet? SubnetDb1a { get; private set; }
        internal CfnSubnet? SubnetDb1c { get; private set; }

        private readonly CfnVPC vpc;

        private ReadOnlyCollection<ResourceInfo> resourceInfoList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vpcRes">VPC</param>
        public SubnetResource(CfnVPC vpc) : base()
        {
            this.vpc = vpc;

            var resourcesInfo = new[]{
                new ResourceInfo(
                    "SubnetPublic1a",
                    "10.0.11.0/24",
                    "ap-northeast-1a",
                    "subnet-public-1a",
                    subnet =>SubnetPublic1a=subnet
                ),
                new ResourceInfo(
                    "SubnetPublic1c",
                    "10.0.12.0/24",
                    "ap-northeast-1c",
                    "subnet-public-1c",
                    subnet =>SubnetPublic1c=subnet
                ),
                new ResourceInfo(
                    "SubnetApp1a",
                    "10.0.21.0/24",
                    "ap-northeast-1a",
                    "subnet-app-1a",
                    subnet =>SubnetApp1a=subnet
                ),
                new ResourceInfo(
                    "SubnetApp1c",
                    "10.0.22.0/24",
                    "ap-northeast-1c",
                    "subnet-app-1c",
                    subnet =>SubnetApp1c=subnet
                ),
                new ResourceInfo(
                    "SubnetDb1a",
                    "10.0.31.0/24",
                    "ap-northeast-1a",
                    "subnet-db-1a",
                    subnet =>SubnetDb1a=subnet
                ),
                new ResourceInfo(
                    "SubnetDb1c",
                    "10.0.32.0/24",
                    "ap-northeast-1c",
                    "subnet-db-1c",
                    subnet =>SubnetDb1c=subnet
                ),
            };

            resourceInfoList = new ReadOnlyCollection<ResourceInfo>(resourcesInfo);
        }

        /// <inheritdoc/>
        internal override void CreateResources(Construct scope)
        {
            foreach (var resourceInfo in resourceInfoList)
            {
                var subnet = createSubnet(scope, resourceInfo);
                resourceInfo.Assign(subnet);
            }
        }

        /// <summary>
        /// サブネット作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private CfnSubnet createSubnet(Construct scope, ResourceInfo resourcesInfo)
        {
            return new CfnSubnet(scope, resourcesInfo.ResourceName, new CfnSubnetProps
            {
                CidrBlock = resourcesInfo.CidrBlock,
                VpcId = vpc.Ref,
                AvailabilityZone = resourcesInfo.AvailabilityZone,
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=CreateResourceName(scope, resourcesInfo.ResourceName)
                    }
                }
            });
        }
    }
}
