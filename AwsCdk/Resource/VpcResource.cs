using Amazon.CDK;
using Amazon.CDK.AWS.EC2;


namespace AwsCdk.Resource
{
    internal class VpcResource : AbhstractResource
    {
        public CfnVPC Vpc { get; private set; }

        public VpcResource()
        {

        }

        internal override void CreateResources(Construct scope)
        {
            Vpc = new CfnVPC(scope, "Vpc", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                Tags = new[] { new CfnTag() { Key = "Name", Value = CreateResourceName(scope, "vpc") } }
            });
        }
    }
}