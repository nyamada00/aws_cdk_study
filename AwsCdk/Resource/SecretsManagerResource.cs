using Constructs;
using Amazon.CDK;
using System;
using Amazon.CDK.AWS.SecretsManager;

namespace AwsCdk.Resource
{
    internal class SecretsManagerResource
    {
        internal CfnSecret? rdsCluster { get; private set; }

        private const string rdsClusterMasterUsername = "admin";

        private const string MasterUsernameKey = "MasterUsername";

        private const string MasterUserPasswordKey = "MasterUserPassword";

        private record ResourceInfo(string Id, string Description, CfnSecret.GenerateSecretStringProperty GenerateSecretString, string ResourceName, Action<CfnSecret> Assign);

        private SecretsManagerResource() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SecretsManagerResource(Construct scope) : base()
        {
            var resourceInfo = new ResourceInfo(
                "SecretRdsCluster",
                "for RDS cluster",
                new CfnSecret.GenerateSecretStringProperty
                {
                    ExcludeCharacters = "\"@/\\'",
                    GenerateStringKey = MasterUserPasswordKey,
                    PasswordLength = 16,
                    SecretStringTemplate = $"{{\"{MasterUsernameKey}\": \"{rdsClusterMasterUsername}\"}}"
                },
                "secrets-rds-cluster",
                secret => rdsCluster = secret
            );

            CreateResources(scope, resourceInfo);
        }

        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope"></param>
        private void CreateResources(Construct scope, ResourceInfo resourceInfo)
        {
            var secret = CreateSecret(scope, resourceInfo);
            resourceInfo.Assign(secret);

        }

        /// <summary>
        /// Secret作成
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private CfnSecret CreateSecret(Construct scope, ResourceInfo resourceInfo)
        {
            return new CfnSecret(scope, resourceInfo.Id, new CfnSecretProps
            {
                Description = resourceInfo.Description,
                GenerateSecretString = resourceInfo.GenerateSecretString,
                Name = ResourceUtility.CreateResourceName(scope, resourceInfo.ResourceName)
            });
        }

    }
}
