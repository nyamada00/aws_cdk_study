using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
using System;

namespace AwsCdk.Resource
{
    internal class IamResource
    {
        private record ResourceInfo(string Id, PolicyStatementProps PolicyStatementProps, string[] ManagedPolicyArns, string RoleName, Action<CfnRole> Assign);

        internal CfnRole? Ec2Role { get; private set; }
        internal CfnRole? RdsRole { get; private set; }

        internal CfnInstanceProfile? InstanceProfileEc2 { get; private set; }

        private IamResource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IamResource(Construct scope) : base()
        {
            var resourcesInfos = new[]{
                new ResourceInfo(
                    "RoleEc2",
                    new PolicyStatementProps{
                        Effect=Effect.ALLOW,
                        Principals=new[]{new ServicePrincipal("ec2.amazonaws.com")},
                        Actions=new[]{"sts:AssumeRole"}
                    },
                    new[]{
                        "arn:aws:iam::aws:policy/AmazonSSMManagedInstanceCore"
                    },
                    "role-ec2",
                    role=>this.Ec2Role=role
                ),
                new ResourceInfo(
                    "RoleRds",
                    new PolicyStatementProps{
                        Effect=Effect.ALLOW,
                        Principals=new[]{new ServicePrincipal("monitoring.rds.amazonaws.com")},
                        Actions=new[]{"sts:AssumeRole"}
                    },
                    new[]{
                        "arn:aws:iam::aws:policy/service-role/AmazonRDSEnhancedMonitoringRole"
                    },
                    "role-rds",
                    role=>this.RdsRole=role
                )
            };

            CreateResources(scope, resourcesInfos);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourceInfoList"></param>
        private void CreateResources(Construct scope, ResourceInfo[] resourceInfoList)
        {
            foreach (var resourceInfo in resourceInfoList)
            {
                var eip = CreateRole(scope, resourceInfo);
                resourceInfo.Assign(eip);
            }

            InstanceProfileEc2 = new CfnInstanceProfile(scope, "InstanceProfileEc2", new CfnInstanceProfileProps
            {
                InstanceProfileName = Ec2Role!.RoleName,
                Roles = new[] { Ec2Role.Ref }
            });
        }

        /// <summary>
        /// IAMRole作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="resourcesInfo"></param>
        /// <returns></returns>
        private static CfnRole CreateRole(Construct scope, ResourceInfo resourcesInfo)
        {
            var policyStatement = new PolicyStatement(resourcesInfo.PolicyStatementProps);
            var policyDocument = new PolicyDocument(new PolicyDocumentProps
            {
                Statements = new[] { policyStatement }
            });

            return new CfnRole(scope, resourcesInfo.Id, new CfnRoleProps
            {
                AssumeRolePolicyDocument = policyDocument,
                ManagedPolicyArns = resourcesInfo.ManagedPolicyArns,
                RoleName = ResourceUtility.CreateResourceName(scope, resourcesInfo.RoleName)
            });
        }
    }
}