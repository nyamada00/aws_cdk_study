using Constructs;

namespace AwsCdk.Resource
{
    internal sealed class ResourceUtility
    {
        /// <summary>
        /// リソース名作成
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="originalName"></param>
        /// <returns>リソース名</returns>
        internal static string CreateResourceName(Construct scope, string originalName)
        {
            var systemName = scope.Node.TryGetContext("systemName");
            var envType = scope.Node.TryGetContext("envType");
            return $"{systemName}-{envType}-{originalName}";
        }
    }
}