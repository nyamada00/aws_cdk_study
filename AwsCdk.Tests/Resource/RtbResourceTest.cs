using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests.Resource
{
    public class RtbResourceTest
    {
        const string SYSTEM_NAME = "RtbResourceTest";
        const string ENV_TYPE = "test";

        private App app;
        private Template template;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RtbResourceTest()
        {
            app = new App(new AppProps
            {
                Context = new Dictionary<string, object>{
                    {"systemName",SYSTEM_NAME},
                    {"envType", ENV_TYPE}
                }
            });

            template = Template.FromStack(new AwsCdkStack(app, "RtbResourceTestStack", new StackProps
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
        /// RouteTable テスト
        /// </summary>
        [Fact]
        public void RtbTest()
        {
            template.ResourceCountIs("AWS::EC2::RouteTable", 4);
            template.HasResourceProperties("AWS::EC2::RouteTable", new Dictionary<string, object>{
                {"VpcId", Match.AnyValue()},
                {"Tags", new []{new Dictionary<string, object>{{ "Key", "Name"}, {"Value", $"{SYSTEM_NAME}-{ENV_TYPE}-rtb-public" }}}}
            });
            template.HasResourceProperties("AWS::EC2::RouteTable", new Dictionary<string, object>{
                {"VpcId", Match.AnyValue()},
                {"Tags", new []{new Dictionary<string, object>{{ "Key", "Name"}, {"Value", $"{SYSTEM_NAME}-{ENV_TYPE}-rtb-app-1a" }}}}
            });
            template.HasResourceProperties("AWS::EC2::RouteTable", new Dictionary<string, object>{
                {"VpcId", Match.AnyValue()},
                {"Tags", new []{new Dictionary<string, object>{{ "Key", "Name"}, {"Value", $"{SYSTEM_NAME}-{ENV_TYPE}-rtb-app-1c" }}}}
            });
            template.HasResourceProperties("AWS::EC2::RouteTable", new Dictionary<string, object>{
                {"VpcId", Match.AnyValue()},
                {"Tags", new []{new Dictionary<string, object>{{ "Key", "Name"}, {"Value", $"{SYSTEM_NAME}-{ENV_TYPE}-rtb-db" }}}}
            });

            template.ResourceCountIs("AWS::EC2::Route", 3);
            template.HasResourceProperties("AWS::EC2::Route", new Dictionary<string, object>{
                {"RouteTableId", Match.AnyValue()},
                {"DestinationCidrBlock", "0.0.0.0/0"},
                {"GatewayId", Match.AnyValue()},
            });
            template.HasResourceProperties("AWS::EC2::Route", new Dictionary<string, object>{
                {"RouteTableId", Match.AnyValue()},
                {"DestinationCidrBlock", "0.0.0.0/0"},
                {"NatGatewayId", Match.AnyValue()},
            });

            template.ResourceCountIs("AWS::EC2::SubnetRouteTableAssociation", 6);
            template.HasResourceProperties("AWS::EC2::SubnetRouteTableAssociation", new Dictionary<string, object>{
                {"RouteTableId", Match.AnyValue()},
                {"SubnetId", Match.AnyValue()},
            });
        }
    }
}
