using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// EIP テスト
        /// </summary>
        [Fact]
        public void EipTest()
        {
            template.ResourceCountIs("AWS::EC2::EIP", 2);
            template.HasResourceProperties("AWS::EC2::EIP", new Dictionary<string, object>{
                { "Domain", "vpc" },
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-eip-ngw-1a" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::EIP", new Dictionary<string, object>{
                { "Domain", "vpc" },
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-eip-ngw-1c" }
                        }
                    }
                }
            });
        }
    }
}