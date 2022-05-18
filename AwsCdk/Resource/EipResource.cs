using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using System;

namespace AwsCdk.Resource
{
    internal class EipResource
    {
        private record ResourceInfo(string Id, string ResourceName, Action<CfnEIP> Assign);

        internal CfnEIP? Eip1a { get; private set; }
        internal CfnEIP? Eip1c { get; private set; }

        private EipResource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EipResource(Construct scope) : base()
        {
            var resourcesInfos = new[]{
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

            CreateResources(scope, resourcesInfos);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourceInfoList"></param>
        private static void CreateResources(Construct scope, ResourceInfo[] resourceInfoList)
        {
            foreach (var resourceInfo in resourceInfoList)
            {
                var eip = CreateEip(scope, resourceInfo);
                resourceInfo.Assign(eip);
            }
        }

        /// <summary>
        /// EIP作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private static CfnEIP CreateEip(Construct scope, ResourceInfo resourcesInfo)
        {
            return new CfnEIP(scope, resourcesInfo.ResourceName, new CfnEIPProps
            {
                Domain = "vpc",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=ResourceUtility.CreateResourceName(scope, resourcesInfo.ResourceName)
                    }
                }
            });
        }
    }
}