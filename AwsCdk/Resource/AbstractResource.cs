using Constructs;

namespace AwsCdk.Resource
{
    internal abstract class AbstractResource
    {
        /// <summary>
        /// リソース名作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="originalName"></param>
        /// <returns></returns>
        protected internal static string CreateResourceName(Construct scope, string originalName)
        {
            var systemName = scope.Node.TryGetContext("systemName");
            var envType = scope.Node.TryGetContext("envType");
            return $"{systemName}-{envType}-{originalName}";
        }
    }
}