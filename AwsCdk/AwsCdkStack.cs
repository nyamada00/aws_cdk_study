using Amazon.CDK;
using Amazon.CDK.AWS.EC2;


namespace AwsCdk
{
    public class AwsCdkStack : Stack
    {
        internal AwsCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var systemName = this.Node.TryGetContext("systemName");
            var envType = this.Node.TryGetContext("envType");

            var vpc = new CfnVPC(this, "Vpc", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                Tags = new[] { new CfnTag() { Key = "Name", Value = $"{systemName}-{envType}-vpc" } }
            });

            var subnetPublic1a = new CfnSubnet(this, "SubnetPublic1a", new CfnSubnetProps
            {
                CidrBlock = "10.0.11.0/24",
                VpcId = vpc.Ref,
                AvailabilityZone = "ap-northeast-1a",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-public-1a"
                    }
                }
            });
            var subnetPublic1c = new CfnSubnet(this, "SubnetPublic1c", new CfnSubnetProps
            {
                CidrBlock = "10.0.12.0/24",
                VpcId = vpc.Ref,
                AvailabilityZone = "ap-northeast-1c",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-public-1c"
                    }
                }
            });
            var subnetApp1a = new CfnSubnet(this, "SubnetApp1a", new CfnSubnetProps
            {
                CidrBlock = "10.0.21.0/24",
                VpcId = vpc.Ref,
                AvailabilityZone = "ap-northeast-1a",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-app-1a"
                    }
                }
            });
            var subnetApp1c = new CfnSubnet(this, "SubnetApp1c", new CfnSubnetProps
            {
                CidrBlock = "10.0.22.0/24",
                VpcId = vpc.Ref,
                AvailabilityZone = "ap-northeast-1c",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-app-1c"
                    }
                }
            });
            var subnetDb1a = new CfnSubnet(this, "SubnetDb1a", new CfnSubnetProps
            {
                CidrBlock = "10.0.31.0/24",
                VpcId = vpc.Ref,
                AvailabilityZone = "ap-northeast-1a",
                Tags = new[]{
                    new CfnTag{
                        Key="Name",
                        Value=$"{systemName}-{envType}-subnet-db-1a"
                    }
                }
            });
            var subnetDb1c = new CfnSubnet(this, "SubnetDb1c", new CfnSubnetProps
            {
                CidrBlock = "10.0.32.0/24",
                VpcId = vpc.Ref,
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
