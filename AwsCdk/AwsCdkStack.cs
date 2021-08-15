using Amazon.CDK;
using AwsCdk.Resource;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var vpcRes = new VpcResource();
            vpcRes.CreateResources(this);

            var subnetRes = new SubnetResource(vpcRes.Vpc);
            subnetRes.CreateResources(this);

            var igwRes = new IgwResource(vpcRes.Vpc);
            igwRes.CreateResources(this);

            var eipRes = new EipResource();
            eipRes.CreateResources(this);
        }
    }
}
