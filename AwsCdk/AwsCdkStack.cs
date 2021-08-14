using Amazon.CDK;
using Amazon.CDK.AWS.EC2;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var systemName=this.Node.TryGetContext("systemName");
            var envType=this.Node.TryGetContext("envType");

            new CfnVPC(this, "Vpc", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                Tags = new[] { new CfnTag() { Key = "Name", Value = $"{systemName}-{envType}-vpc" } }
            });
        }
    }
}
