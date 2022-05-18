using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        private const string SYSTEM_NAME = "awscdk_study";
        private const string ENV_TYPE = "test";

        private readonly App app;
        private readonly Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AwsCdkStackTest()
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
        /// Stack テスト
        /// </summary>
        [Fact]
        public void StackTest()
        {
            // VPC
            template.ResourceCountIs("AWS::EC2::VPC", 1);
        }
    }
}