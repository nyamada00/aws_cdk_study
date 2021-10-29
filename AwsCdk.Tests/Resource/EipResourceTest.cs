using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests.Resource
{
    public class EipResourceTest
    {
        const string SYSTEM_NAME = "EipResourceTest";
        const string ENV_TYPE = "test";

        private App app;
        private Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EipResourceTest()
        {
            app = new App(new AppProps
            {
                Context = new Dictionary<string, object>{
                    {"systemName",SYSTEM_NAME},
                    {"envType", ENV_TYPE}
                }
            });

            template = Template.FromStack(new AwsCdkStack(app, "EipResourceTestStack", new StackProps
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
        /// EIP テスト
        /// </summary>
        [Fact]
        public void EipTest()
        {
            // VPC
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
