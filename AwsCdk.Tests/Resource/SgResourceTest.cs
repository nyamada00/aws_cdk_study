using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// SecurityGroup テスト
        /// </summary>
        [Fact]
        public void SgTest()
        {
            template.ResourceCountIs("AWS::EC2::SecurityGroup", 3);
            template.HasResourceProperties("AWS::EC2::SecurityGroup", new Dictionary<string, object>{
                {"GroupDescription", "for ALB"},
                {"GroupName", $"{SYSTEM_NAME}-{ENV_TYPE}-sg-alb"},
                {"VpcId", Match.AnyValue()},
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-sg-alb" }
                        }
                    }
                }

                }
            );
            template.HasResourceProperties("AWS::EC2::SecurityGroup", new Dictionary<string, object>{
                {"GroupDescription", "for EC2"},
                {"GroupName", $"{SYSTEM_NAME}-{ENV_TYPE}-sg-ec2"},
                {"VpcId", Match.AnyValue()},
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-sg-ec2" }
                        }
                    }
                }

                }
            );
            template.HasResourceProperties("AWS::EC2::SecurityGroup", new Dictionary<string, object>{
                {"GroupDescription", "for RDS"},
                {"GroupName", $"{SYSTEM_NAME}-{ENV_TYPE}-sg-rds"},
                {"VpcId", Match.AnyValue()},
                { "Tags",new  []{
                        new Dictionary<string, object> {
                            { "Key", "Name" },
                            { "Value", $"{SYSTEM_NAME}-{ENV_TYPE}-sg-rds" }
                        }
                    }
                }
                }
            );
            template.ResourceCountIs("AWS::EC2::SecurityGroupIngress", 4);
            template.HasResourceProperties("AWS::EC2::SecurityGroupIngress", new Dictionary<string, object>{
                {"IpProtocol", "tcp"},
                {"CidrIp", "0.0.0.0/0"},
                {"FromPort", 80},
                {"ToPort", 80},
                {"GroupId", Match.AnyValue()}
                }
            );
            template.HasResourceProperties("AWS::EC2::SecurityGroupIngress", new Dictionary<string, object>{
                {"IpProtocol", "tcp"},
                {"CidrIp", "0.0.0.0/0"},
                {"FromPort", 443},
                {"ToPort", 443},
                {"GroupId", Match.AnyValue()}
                }
            );
            template.HasResourceProperties("AWS::EC2::SecurityGroupIngress", new Dictionary<string, object>{
                {"IpProtocol", "tcp"},
                {"FromPort", 80},
                {"ToPort", 80},
                {"GroupId", Match.AnyValue()},
                {"SourceSecurityGroupId", Match.AnyValue()}
                }
            );
            template.HasResourceProperties("AWS::EC2::SecurityGroupIngress", new Dictionary<string, object>{
                {"IpProtocol", "tcp"},
                {"FromPort", 3306},
                {"ToPort", 3306},
                {"GroupId", Match.AnyValue()},
                {"SourceSecurityGroupId", Match.AnyValue()}
                }
           );
            // template.HasResourceProperties("AWS::IAM::Role", new Dictionary<string, object>{
            //     { "AssumeRolePolicyDocument",
            //         new Dictionary<string, object> {
            //             { "Statement", new[]{
            //                 new Dictionary<string, object> {
            //                     {"Effect","Allow"},
            //                     {"Principal", new Dictionary<string,object>{
            //                         {"Service", "monitoring.rds.amazonaws.com"}
            //                     }},
            //                     {"Action", "sts:AssumeRole"}
            //                 }
            //             } }
            //         }
            //     },
            //     {
            //         "ManagedPolicyArns", new[]{"arn:aws:iam::aws:policy/service-role/AmazonRDSEnhancedMonitoringRole"}
            //     },
            //     {"RoleName",$"{SYSTEM_NAME}-{ENV_TYPE}-role-rds"}
            // });
            // template.ResourceCountIs("AWS::IAM::InstanceProfile", 1);
            // template.HasResourceProperties("AWS::IAM::InstanceProfile", new Dictionary<string, object>{
            //     {
            //         "Roles", Match.AnyValue()
            //     },
            //     {"InstanceProfileName",$"{SYSTEM_NAME}-{ENV_TYPE}-role-ec2"}
            // });
        }
    }
}
