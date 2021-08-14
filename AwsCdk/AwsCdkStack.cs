using Amazon.CDK;
using Amazon.CDK.AWS.EC2;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            new CfnVPC(this, "Vpc", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                Tags = new[] { new CfnTag() { Key = "Name", Value = "awscdk-vpc" } }
            });
        }
    }
}
