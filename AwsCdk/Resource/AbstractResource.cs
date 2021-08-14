using Amazon.CDK;

namespace AwsCdk.Resource
{
    internal abstract class AbstractResource
    {
        /// <summary>
        /// リソース作成
        /// </summary>
        /// <param name="scope">コンストラクト</param>
        abstract internal void CreateResources(Construct scope);
        /// <summary>
        /// リソース名作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="originalName"></param>
        /// <returns></returns>
        protected internal string CreateResourceName(Construct scope, string originalName)
        {
            var systemName = scope.Node.TryGetContext("systemName");
            var envType = scope.Node.TryGetContext("envType");
            return $"{systemName}-{envType}-{originalName}";
        }
    }
}