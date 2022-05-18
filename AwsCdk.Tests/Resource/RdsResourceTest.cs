using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// Rds テスト
        /// </summary>
        [Fact]
        public void RdsResourceTest()
        {
            //DBSubnetGroup
            template.ResourceCountIs("AWS::RDS::DBSubnetGroup", 1);
            template.HasResourceProperties("AWS::RDS::DBSubnetGroup", new Dictionary<string, object>{
                { "DBSubnetGroupDescription" , "Subnet Group for RDS"},
                { "SubnetIds", Match.AnyValue() },
                { "DBSubnetGroupName" , $"{SYSTEM_NAME}-{ENV_TYPE}-sng-rds"}
            });
            //DBClusterParameterGroup
            template.ResourceCountIs("AWS::RDS::DBClusterParameterGroup", 1);
            template.HasResourceProperties("AWS::RDS::DBClusterParameterGroup", new Dictionary<string, object>{
                { "Description" , "Cluster Parameter Group for RDS"},
                { "Family", "aurora-mysql5.7"},
                { "Parameters" , new Dictionary<string, object>{{"time_zone", "UTC"} }}
            });
            //DBParameterGroup
            template.ResourceCountIs("AWS::RDS::DBParameterGroup", 1);
            template.HasResourceProperties("AWS::RDS::DBParameterGroup", new Dictionary<string, object>{
                { "Description" , "Parameter Group for RDS"},
                { "Family", "aurora-mysql5.7" },
            });

        }
    }
}