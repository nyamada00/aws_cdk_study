using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// InternetGateway テスト
        /// </summary>
        [Fact]
        public void IgwTest()
        {
            // VPC
            template.ResourceCountIs("AWS::EC2::InternetGateway", 1);
            template.HasResourceProperties("AWS::EC2::InternetGateway", new Dictionary<string, object>{
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-igw" }
                        }
                    }
                }
            });
            template.ResourceCountIs("AWS::EC2::VPCGatewayAttachment", 1);
        }
    }
}
