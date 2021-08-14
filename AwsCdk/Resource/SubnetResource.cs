using Amazon.CDK;
using Amazon.CDK.AWS.EC2;


namespace AwsCdk.Resource
{
    internal class SubnetResource
    {
        public CfnSubnet SubnetPublic1a { get; private set; }
        public CfnSubnet SubnetPublic1c { get; private set; }
        public CfnSubnet SubnetApp1a { get; private set; }
        public CfnSubnet SubnetApp1c { get; private set; }
        public CfnSubnet SubnetDb1a { get; private set; }
        public CfnSubnet SubnetDb1c { get; private set; }

        private VpcResource vpcRes;

        public SubnetResource(VpcResource vpcRes)
        {
            this.vpcRes = vpcRes;
        }

        public void CreateResources(Construct scope)
        {
            var systemName = scope.Node.TryGetContext("systemName");
            var envType = scope.Node.TryGetContext("envType");
            SubnetPublic1a = new CfnSubnet(scope, "SubnetPublic1a", new CfnSubnetProps
            {
                CidrBlock = "10.0.11.0/24",
                VpcId = vpcRes.Vpc.Ref,
                AvailabilityZone = "ap-northeast-1a",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-public-1a"
                    }
                }
            });
            var subnetPublic1c = new CfnSubnet(scope, "SubnetPublic1c", new CfnSubnetProps
            {
                CidrBlock = "10.0.12.0/24",
                VpcId = vpcRes.Vpc.Ref,
                AvailabilityZone = "ap-northeast-1c",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-public-1c"
                    }
                }
            });
            var subnetApp1a = new CfnSubnet(scope, "SubnetApp1a", new CfnSubnetProps
            {
                CidrBlock = "10.0.21.0/24",
                VpcId = vpcRes.Vpc.Ref,
                AvailabilityZone = "ap-northeast-1a",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-app-1a"
                    }
                }
            });
            var subnetApp1c = new CfnSubnet(scope, "SubnetApp1c", new CfnSubnetProps
            {
                CidrBlock = "10.0.22.0/24",
                VpcId = vpcRes.Vpc.Ref,
                AvailabilityZone = "ap-northeast-1c",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-app-1c"
                    }
                }
            });
            var subnetDb1a = new CfnSubnet(scope, "SubnetDb1a", new CfnSubnetProps
            {
                CidrBlock = "10.0.31.0/24",
                VpcId = vpcRes.Vpc.Ref,
                AvailabilityZone = "ap-northeast-1a",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-db-1a"
                    }
                }
            });
            var subnetDb1c = new CfnSubnet(scope, "SubnetDb1c", new CfnSubnetProps
            {
                CidrBlock = "10.0.32.0/24",
                VpcId = vpcRes.Vpc.Ref,
                AvailabilityZone = "ap-northeast-1c",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-db-1c"
                    }
                }
            });
        }
    }
}
