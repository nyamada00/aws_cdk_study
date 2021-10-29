using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests.Resource
{
    public class NatGatewayResourceTest
    {
        private const string SYSTEM_NAME = "awscdk_study";
        private const string ENV_TYPE = "test";

        private App app;
        private Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NatGatewayResourceTest()
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
