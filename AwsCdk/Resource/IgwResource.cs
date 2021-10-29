using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace AwsCdk.Resource
{
    internal class IgwResource : AbstractResource
    {
        internal CfnInternetGateway? Igw { get; private set; }

        private readonly CfnVPC vpc;

        public IgwResource(CfnVPC vpc) : base()
        {
            this.vpc = vpc;
        }

        /// <inheritdoc/>
        internal override void CreateResources(Construct scope)
        {
            Igw = new CfnInternetGateway(scope, "InternetGateway", new CfnInternetGatewayProps()
            {
                Tags = new[]{
                    new CfnTag{Key="Name", Value=CreateResourceName(scope,"igw")}
                }
            });

            new CfnVPCGatewayAttachment(scope, "VpcGateway", new CfnVPCGatewayAttachmentProps()
            {
                VpcId = vpc.Ref,
                InternetGatewayId = Igw.Ref
            });
        }
    }
}
