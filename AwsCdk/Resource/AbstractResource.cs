using Amazon.CDK;

namespace AwsCdk.Resource
{
    internal abstract class AbhstractResource
    {
        abstract internal void CreateResources(Construct scope);
        protected internal string CreateResourceName(Construct scope, string originalName)
        {
            var systemName = scope.Node.TryGetContext("systemName");
            var envType = scope.Node.TryGetContext("envType");
            return $"{systemName}-{envType}-{originalName}";
        }
    }
}