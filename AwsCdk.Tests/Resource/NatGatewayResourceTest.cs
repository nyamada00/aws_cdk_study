using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// NatGateway テスト
        /// </summary>
        [Fact]
        public void NatGatewayTest()
        {
            //NatGateway
            template.ResourceCountIs("AWS::EC2::NatGateway", 2);
            template.HasResourceProperties("AWS::EC2::NatGateway", new Dictionary<string, object>{
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-ngw-1a" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::NatGateway", new Dictionary<string, object>{
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-ngw-1c" }
                        }
                    }
                }
            });
        }
    }
}
