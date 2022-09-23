using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// VPC テスト
        /// </summary>
        [Fact]
        public void VpcTest()
        {
            // VPC
            template.ResourceCountIs("AWS::EC2::VPC", 1);
            template.HasResourceProperties("AWS::EC2::VPC", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.0.0/16"},
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-vpc" }
                        }
                    }
                }
            });
        }
    }
}