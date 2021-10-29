using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.ObjectModel;


namespace AwsCdk.Resource
{
    internal class EipResource : AbstractResource
    {
        private readonly struct ResourceInfo
        {
            public string Id { get; }
            public string ResourceName { get; }

            public Action<CfnEIP> Assign { get; }

            public ResourceInfo(string id, string resourceName, Action<CfnEIP> assign)
            => (Id, ResourceName, Assign) = (id, resourceName, assign);
        }

        internal CfnEIP Eip1a { get; private set; }
        internal CfnEIP Eip1c { get; private set; }

        private ReadOnlyCollection<ResourceInfo> resourceInfoList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EipResource() : base()
        {
            var resourcesInfo = new[]{
                new ResourceInfo(
                    "ElasticIpNgw1a",
                    "eip-ngw-1a",
                    eip=>this.Eip1a=eip
                ),
                new ResourceInfo(
                    "ElasticIpNgw1c",
                    "eip-ngw-1c",
                    eip=>this.Eip1c=eip
                )
            };

            resourceInfoList = new ReadOnlyCollection<ResourceInfo>(resourcesInfo);
        }

        /// <inheritdoc/>
        internal override void CreateResources(Construct scope)
        {
            foreach (var resourceInfo in resourceInfoList)
            {
                var eip = createEip(scope, resourceInfo);
                resourceInfo.Assign(eip);
            }
        }

        /// <summary>
        /// EIP作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private CfnEIP createEip(Construct scope, ResourceInfo resourcesInfo)
        {
            return new CfnEIP(scope, resourcesInfo.ResourceName, new CfnEIPProps
            {
                Domain = "vpc",
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
