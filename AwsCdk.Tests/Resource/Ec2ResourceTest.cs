using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// EC2 テスト
        /// </summary>
        [Fact]
        public void Ec2Test()
        {
            template.ResourceCountIs("AWS::EC2::Instance", 2);
            template.HasResourceProperties("AWS::EC2::Instance", new Dictionary<string, object>{
                { "AvailabilityZone", "ap-northeast-1a" },
                { "IamInstanceProfile", Match.AnyValue() },
                { "ImageId", "ami-06631ebafb3ae5d34" },
                { "InstanceType", "t2.micro" },
                { "SecurityGroupIds", Match.AnyValue()  },
                { "SubnetId", Match.AnyValue()  },
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-ec2-1a" }
                        }
                    }
                }
            });
            template.HasResourceProperties("AWS::EC2::Instance", new Dictionary<string, object>{
                { "AvailabilityZone", "ap-northeast-1c" },
                { "IamInstanceProfile", Match.AnyValue() },
                { "ImageId", "ami-06631ebafb3ae5d34" },
                { "InstanceType", "t2.micro" },
                { "SecurityGroupIds", Match.AnyValue()  },
                { "SubnetId", Match.AnyValue()  },
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-ec2-1c" }
                        }
                    }
                }
            });
        }
    }
}