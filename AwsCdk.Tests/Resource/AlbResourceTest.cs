using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// ALB テスト
        /// </summary>
        [Fact]
        public void AlbResourceTest()
        {
            template.ResourceCountIs("AWS::ElasticLoadBalancingV2::LoadBalancer", 1);
            template.HasResourceProperties("AWS::ElasticLoadBalancingV2::LoadBalancer", new Dictionary<string, object>{
                {"IpAddressType", "ipv4"},
                {"Name", $"{SYSTEM_NAME}-{ENV_TYPE}-alb"},
                {"Scheme", "internet-facing"},
                {"SecurityGroups", Match.AnyValue()},
                {"Subnets", Match.AnyValue()},
                {"Type", "application"}
            });

            template.ResourceCountIs("AWS::ElasticLoadBalancingV2::TargetGroup", 1);
            template.HasResourceProperties("AWS::ElasticLoadBalancingV2::TargetGroup", new Dictionary<string, object>{
                {"Name", $"{SYSTEM_NAME}-{ENV_TYPE}-tg"},
                {"Port", 80},
                {"Protocol", "HTTP"},
                {"TargetType", "instance"},
                {"Targets", Match.AnyValue()},
                {"VpcId", Match.AnyValue()}
            });

            template.ResourceCountIs("AWS::ElasticLoadBalancingV2::Listener", 1);
            template.HasResourceProperties("AWS::ElasticLoadBalancingV2::Listener", new Dictionary<string, object>{
                {"DefaultActions", new []{
                    new Dictionary<string, object>{
                        {"Type", "forward"},
                        {"ForwardConfig", new Dictionary<string, object>{
                            {"TargetGroups",new [] {
                                new Dictionary<string, object>{
                                    {"TargetGroupArn", Match.AnyValue()},
                                    {"Weight", 1}
                                }
                                }
                            }
                        }
                        },
                    }
                }},
                {"LoadBalancerArn", Match.AnyValue()},
                {"Port", 80},
                {"Protocol", "HTTP"}
            });
        }
    }
}
