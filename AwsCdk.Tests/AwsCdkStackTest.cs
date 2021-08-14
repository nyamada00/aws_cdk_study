using System;
using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;


namespace AwsCdk.Tests
{
    public class AwsCdkStackTest
    {
        [Fact]
        public void VpcTest()
        {
            var app = new App();
            var stack = new AwsCdkStack(app, "AwsCdkStack", new StackProps
            {
                // If you don't specify 'env', this stack will be environment-agnostic.
                // Account/Region-dependent features and context lookups will not work,
                // but a single synthesized template can be deployed anywhere.

                // Uncomment the next block to specialize this stack for the AWS Account
                // and Region that are implied by the current CLI configuration.

                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }

                // Uncomment the next block if you know exactly what Account and Region you
                // want to deploy the stack to.
                /*
                Env = new Amazon.CDK.Environment
                {
                    Account = "123456789012",
                    Region = "us-east-1",
                }
                */

                // For more information, see https://docs.aws.amazon.com/cdk/latest/guide/environments.html
            });
            var template = Template.FromStack(stack);
            template.ResourceCountIs("AWS::EC2::VPC", 1);
            template.HasResourceProperties("AWS::EC2::VPC", new Dictionary<string, object>{
                { "CidrBlock" , "10.0.0.0/16"}
            });
            template.HasResourceProperties("AWS::EC2::VPC", new Dictionary<string, object>{
                { "Tags",new  []
                    {
                        new Dictionary<string, object> { { "Key", "Name" }, { "Value", "awscdk-vpc" } }
                    }
                }
            });
        }
    }
}
