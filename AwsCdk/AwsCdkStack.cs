using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using AwsCdk.Resource;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var vpcRes = new VpcResource();
            vpcRes.CreateResources(this);

            var subnetRes = new SubnetResource(vpcRes);
            subnetRes.CreateResources(this);
        }
    }
}
