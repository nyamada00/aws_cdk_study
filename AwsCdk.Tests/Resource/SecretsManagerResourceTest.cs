using Xunit;
using Amazon.CDK.Assertions;
using Amazon.CDK;
using System.Collections.Generic;

namespace AwsCdk.Tests
{
    public partial class AwsCdkStackTest
    {
        /// <summary>
        /// SecretsManager テスト
        /// </summary>
        [Fact]
        public void SecretsManagerResourceTest()
        {
            template.ResourceCountIs("AWS::SecretsManager::Secret", 1);
            template.HasResourceProperties("AWS::SecretsManager::Secret", new Dictionary<string, object>{
                {"Description", "for RDS cluster"},
                {"GenerateSecretString", new Dictionary<string, object>{
                        {"ExcludeCharacters", "\"@/\\'"},
                        {"GenerateStringKey" ,"MasterUserPassword"},
                        {"PasswordLength", 16},
                        {"SecretStringTemplate", "{\"MasterUsername\": \"admin\"}"}
                    }
                },
                {"Name", $"{SYSTEM_NAME}-{ENV_TYPE}-secrets-rds-cluster"},
            });

        }
    }
}
