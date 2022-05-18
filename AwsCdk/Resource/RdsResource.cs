using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.EC2;
using System;
using System.Collections.Generic;

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
            CreateClusterParameterGroup(scope);
            CreateParameterGroup(scope);
        }

        /// <summary>
        /// ParameterGroup作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private CfnDBParameterGroup CreateParameterGroup(Construct scope)
        {
            return new CfnDBParameterGroup(scope, "ParameterGroupRds", new CfnDBParameterGroupProps
            {
                Description = "Parameter Group for RDS",
                Family = "aurora-mysql5.7"
            });
        }

        /// <summary>
        /// ClusterParameterGroup作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private CfnDBClusterParameterGroup CreateClusterParameterGroup(Construct scope)
        {
            return new CfnDBClusterParameterGroup(scope, "ClusterParameterGroupRds", new CfnDBClusterParameterGroupProps
            {
                Description = "Cluster Parameter Group for RDS",
                Family = "aurora-mysql5.7",
                Parameters = new Dictionary<string, object> { { "time_zone", "UTC" } }
            });
        }

        /// <summary>
        /// SubnetGroup作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
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