using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// Subnet テスト
        /// </summary>
        [Fact]
        public void SubnetTest()
        {
            //Subnet
            template.ResourceCountIs("AWS::EC2::Subnet", 6);
            template.HasResourceProperties("AWS::EC2::Subnet", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.11.0/24"},
                { "AvailabilityZone", "ap-northeast-1a" },
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-subnet-public-1a" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::Subnet", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.12.0/24"},
                { "AvailabilityZone", "ap-northeast-1c" },
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-subnet-public-1c" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::Subnet", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.21.0/24"},
                { "AvailabilityZone", "ap-northeast-1a" },
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-subnet-app-1a" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::Subnet", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.22.0/24"},
                { "AvailabilityZone", "ap-northeast-1c" },
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-subnet-app-1c" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::Subnet", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.31.0/24"},
                { "AvailabilityZone", "ap-northeast-1a" },
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-subnet-db-1a" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::Subnet", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.32.0/24"},
                { "AvailabilityZone", "ap-northeast-1c" },
                { "Tags",new  [] {
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-subnet-db-1c" }
                        }
                    }
                }
            });
        }
    }
}
