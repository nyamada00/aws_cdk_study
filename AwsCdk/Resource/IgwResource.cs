using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace AwsCdk.Resource
{
    internal class IgwResource
    {
        internal CfnInternetGateway? Igw { get; private set; }

        private readonly CfnVPC? vpc;

        private IgwResource() { }

        public IgwResource(Construct scope, CfnVPC vpc) : base()
        {
            this.vpc = vpc;
            CreateResources(scope);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        private void CreateResources(Construct scope)
        {
            Igw = new CfnInternetGateway(scope, "InternetGateway", new CfnInternetGatewayProps()
            {
                Tags = new[]{
                    new CfnTag{Key="Name", Value=ResourceUtility.CreateResourceName(scope,"igw")}
                }
            });

            _ = new CfnVPCGatewayAttachment(scope, "VpcGateway", new CfnVPCGatewayAttachmentProps()
            {
                VpcId = vpc!.Ref,
                InternetGatewayId = Igw.Ref
            });
        }
    }
}