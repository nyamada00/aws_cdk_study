using Amazon.CDK;
using AwsCdk.Resource;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps? props = null) : base(scope, id, props)
        {
            var vpcRes = new VpcResource();
            vpcRes.CreateResources(this);

            var subnetRes = new SubnetResource(vpcRes.Vpc!);
            subnetRes.CreateResources(this);

            var igwRes = new IgwResource(vpcRes.Vpc!);
            igwRes.CreateResources(this);

            var eipRes = new EipResource();
            eipRes.CreateResources(this);

            var ngwRes = new NatGatewayResource(subnetRes.SubnetPublic1a!, subnetRes.SubnetPublic1c!, eipRes.Eip1a!, eipRes.Eip1c!);
            ngwRes.CreateResources(this);

            var rtbRes = new RtbResource(
                vpcRes.Vpc!,
                subnetRes.SubnetPublic1a!,
                subnetRes.SubnetPublic1c!,
                subnetRes.SubnetApp1a!,
                subnetRes.SubnetApp1c!,
                subnetRes.SubnetDb1a!,
                subnetRes.SubnetDb1c!,
                igwRes.Igw!,
                ngwRes.Ngw1a!,
                ngwRes.Ngw1c!
            );
            rtbRes.CreateResources(this);
        }
    }
}
