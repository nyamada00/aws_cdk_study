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
            var app = new App(new AppProps
            {
                Context = new Dictionary<string, object>{
                    {"systemName","awscdk_study"},
                    {"envType", "test"}
                }
            });
            var stack = new AwsCdkStack(app, "AwsCdkStack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }

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
                        new Dictionary<string, object> { { "Key", "Name" }, { "Value", "awscdk_study-test-vpc" } }
                    }
                }
            });
        }
    }
}
