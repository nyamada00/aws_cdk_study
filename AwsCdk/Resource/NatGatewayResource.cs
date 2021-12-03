using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;


namespace AwsCdk.Resource
{
    internal class NatGatewayResource : AbstractResource
    {
        private record ResourceInfo(string Id, string AllocationId, string SubnetId, string ResourceName, Action<CfnNatGateway> Assign);

        internal CfnNatGateway? Ngw1a { get; private set; }
        internal CfnNatGateway? Ngw1c { get; private set; }

        private CfnSubnet? SubnetPublic1a { get; }
        private CfnSubnet? SubnetPublic1c { get; }

        private CfnEIP? EipNgw1a { get; }
        private CfnEIP? EipNgw1c { get; }

        private NatGatewayResource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vpcRes">VPC</param>
        public NatGatewayResource(
            Construct scope,
            CfnSubnet subnetPublic1a,
            CfnSubnet subnetPublic1c,
            CfnEIP eipNgw1a,
            CfnEIP eipNgw1c
        ) : base()
        {
            this.SubnetPublic1a = subnetPublic1a;
            this.SubnetPublic1c = subnetPublic1c;
            this.EipNgw1a = eipNgw1a;
            this.EipNgw1c = eipNgw1c;

            var resourcesInfos = new[]{
                new ResourceInfo(
                    "NatGateway1a",
                    eipNgw1a.AttrAllocationId,
                    subnetPublic1a.Ref,
                    "ngw-1a",
                    ngw =>Ngw1a=ngw
                ),
                new ResourceInfo(
                    "NatGateway1c",
                    eipNgw1c.AttrAllocationId,
                    subnetPublic1c.Ref,
                    "ngw-1c",
                    ngw =>Ngw1c=ngw
                ),
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
                var subnet = createNgw(scope, resourceInfo);
                resourceInfo.Assign(subnet);
            }
        }

        /// <summary>
        /// サブネット作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private CfnNatGateway createNgw(Construct scope, ResourceInfo resourcesInfo)
        {
            return new CfnNatGateway(scope, resourcesInfo.ResourceName, new CfnNatGatewayProps
            {
                AllocationId = resourcesInfo.AllocationId,
                SubnetId = resourcesInfo.SubnetId,
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
