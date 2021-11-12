using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace AwsCdk.Resource
{
    internal class VpcResource : AbstractResource
    {
        internal CfnVPC? Vpc { get; private set; }

        private VpcResource()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public VpcResource(Construct scope) : base()
        {
            CreateResources(scope);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        private void CreateResources(Construct scope)
        {
            Vpc = new CfnVPC(scope, "Vpc", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                Tags = new[] { new CfnTag() { Key = "Name", Value = CreateResourceName(scope, "vpc") } }
            });
        }
    }
}
