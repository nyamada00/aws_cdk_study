using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests.Resource
{
    public class SubnetResourceTest
    {
        const string SYSTEM_NAME = "awscdk_study";
        const string ENV_TYPE = "test";

        private App app;
        private Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SubnetResourceTest()
        {
            app = new App(new AppProps
            {
                Context = new Dictionary<string, object>{
                    {"systemName",SYSTEM_NAME},
                    {"envType", ENV_TYPE}
                }
            });

            template = Template.FromStack(new AwsCdkStack(app, "AwsCdkStack", new StackProps
            {
                Env = new Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }

                // For more information, see https://docs.aws.amazon.com/cdk/latest/guide/environments.html
            }));
        }

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
