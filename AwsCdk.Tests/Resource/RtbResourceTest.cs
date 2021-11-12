using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
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
