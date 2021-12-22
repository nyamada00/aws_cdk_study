using Constructs;
using Amazon.CDK;
using AwsCdk.Resource;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps? props = null) : base(scope, id, props)
        {
            var vpcRes = new VpcResource(this);

            var subnetRes = new SubnetResource(this, vpcRes.Vpc!);

            var igwRes = new IgwResource(this, vpcRes.Vpc!);

            var eipRes = new EipResource(this);

            var ngwRes = new NatGatewayResource(
                this,
                subnetRes.SubnetPublic1a!,
                subnetRes.SubnetPublic1c!,
                eipRes.Eip1a!,
                eipRes.Eip1c!);

            var rtbRes = new RtbResource(
                this,
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

            var iamRes = new IamResource(this);
            var sgRes = new SgResource(this, vpcRes.Vpc!);
            var ec2Res = new Ec2Resource(this,
                subnetRes.SubnetApp1a!,
                subnetRes.SubnetApp1c!,
                iamRes.InstanceProfileEc2!,
                sgRes.ec2!);
        }
    }
}
