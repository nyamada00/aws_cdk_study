using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests.Resource
{
    public class VpcResourceTest
    {
        const string SYSTEM_NAME = "VpcResourceTest";
        const string ENV_TYPE = "test";

        private App app;
        private Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VpcResourceTest()
        {
            app = new App(new AppProps
            {
                Context = new Dictionary<string, object>{
                    {"systemName",SYSTEM_NAME},
                    {"envType", ENV_TYPE}
                }
            });

            template = Template.FromStack(new AwsCdkStack(app, "VpcResourceTestStack", new StackProps
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
