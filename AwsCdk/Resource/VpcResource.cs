using Amazon.CDK;
using Amazon.CDK.AWS.EC2;


namespace AwsCdk.Resource
{
    internal class VpcResource
    {
        public CfnVPC Vpc { get; private set; }

        public VpcResource()
        {

        }

        public void CreateResources(Construct scope)
        {
            var systemName = scope.Node.TryGetContext("systemName");
            var envType = scope.Node.TryGetContext("envType");

            Vpc = new CfnVPC(scope, "Vpc", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                Tags = new[] { new CfnTag() { Key = "Name", Value = $"{systemName}-{envType}-vpc" } }
            });
        }
    }
}
