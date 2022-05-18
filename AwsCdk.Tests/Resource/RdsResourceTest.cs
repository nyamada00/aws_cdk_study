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
            //Subnet
            template.ResourceCountIs("AWS::RDS::DBSubnetGroup", 1);
            template.HasResourceProperties("AWS::RDS::DBSubnetGroup", new Dictionary<string, object>{
                { "DBSubnetGroupDescription" , "Subnet Group for RDS"},
                { "SubnetIds", Match.AnyValue() },
                { "DBSubnetGroupName" , $"{SYSTEM_NAME}-{ENV_TYPE}-sng-rds"}
            });
        }
    }
}