using Amazon.CDK;
using AwsCdk;

var app = new App();
_ = new AwsCdkStack(app, "AwsCdkStack", new StackProps
{
    Env = new Amazon.CDK.Environment
    {
        Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
        Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
    }
});

app.Synth();
