using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// IAM テスト
        /// </summary>
        [Fact]
        public void IamTest()
        {
            template.ResourceCountIs("AWS::IAM::Role", 2);
            template.HasResourceProperties("AWS::IAM::Role", new Dictionary<string, object>{
                { "AssumeRolePolicyDocument",
                    new Dictionary<string, object> {
                        { "Statement", new[]{
                            new Dictionary<string, object> {
                                {"Effect","Allow"},
                                {"Principal", new Dictionary<string,object>{
                                    {"Service", Match.AnyValue()}
                                }},
                                {"Action", "sts:AssumeRole"}
                            }
                        } }
                    }
                },
                {
                    "ManagedPolicyArns", new[]{"arn:aws:iam::aws:policy/AmazonSSMManagedInstanceCore"}
                },
                {"RoleName",$"{SYSTEM_NAME}-{ENV_TYPE}-role-ec2"}
            });
            template.HasResourceProperties("AWS::IAM::Role", new Dictionary<string, object>{
                { "AssumeRolePolicyDocument",
                    new Dictionary<string, object> {
                        { "Statement", new[]{
                            new Dictionary<string, object> {
                                {"Effect","Allow"},
                                {"Principal", new Dictionary<string,object>{
                                    {"Service", "monitoring.rds.amazonaws.com"}
                                }},
                                {"Action", "sts:AssumeRole"}
                            }
                        } }
                    }
                },
                {
                    "ManagedPolicyArns", new[]{"arn:aws:iam::aws:policy/service-role/AmazonRDSEnhancedMonitoringRole"}
                },
                {"RoleName",$"{SYSTEM_NAME}-{ENV_TYPE}-role-rds"}
            });
            template.ResourceCountIs("AWS::IAM::InstanceProfile", 1);
            template.HasResourceProperties("AWS::IAM::InstanceProfile", new Dictionary<string, object>{
                {
                    "Roles", Match.AnyValue()
                },
                {"InstanceProfileName",$"{SYSTEM_NAME}-{ENV_TYPE}-role-ec2"}
            });
        }
    }
}
