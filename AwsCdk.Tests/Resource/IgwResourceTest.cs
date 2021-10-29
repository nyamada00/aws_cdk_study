using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests.Resource
{
    public class IgwResourceTest
    {
        const string SYSTEM_NAME = "IgwResourceTest";
        const string ENV_TYPE = "test";

        private App app;
        private Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IgwResourceTest()
        {
            app = new App(new AppProps
            {
                Context = new Dictionary<string, object>{
                    {"systemName",SYSTEM_NAME},
                    {"envType", ENV_TYPE}
                }
            });

            template = Template.FromStack(new AwsCdkStack(app, "IgwResourceTestStack", new StackProps
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
