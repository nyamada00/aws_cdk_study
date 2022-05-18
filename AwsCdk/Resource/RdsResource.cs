using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.EC2;

namespace AwsCdk.Resource
{
    internal class RdsResource
    {

        private readonly CfnSubnet? subnetDb1a;
        private readonly CfnSubnet? subnetDb1c;

        private RdsResource()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public RdsResource(Construct scope,
        CfnSubnet subnetDb1a,
        CfnSubnet subnetDb1c) : base()
        {
            this.subnetDb1a = subnetDb1a;
            this.subnetDb1c = subnetDb1c;
            CreateResources(scope);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        private void CreateResources(Construct scope)
        {
            CreateSubnetGroup(scope);
        }

        /// <summary>
        /// SubnetGroup作成
        /// </summary>
        /// <param name="scope"></param>
        private CfnDBSubnetGroup CreateSubnetGroup(Construct scope)
        {
            return new CfnDBSubnetGroup(scope, "SubnetGroupRds", new CfnDBSubnetGroupProps
            {
                DbSubnetGroupDescription = "Subnet Group for RDS",
                SubnetIds = new[] { this.subnetDb1a!.Ref, this.subnetDb1c!.Ref },
                DbSubnetGroupName = ResourceUtility.CreateResourceName(scope, "sng-rds")
            });
        }
    }
}
