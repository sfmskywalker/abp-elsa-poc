using System;
using System.Threading.Tasks;
using Elsa.Workflows;
using Elsa.Workflows.Activities.Flowchart.Attributes;
using Elsa.Workflows.Attributes;

namespace ElsaWorkflows.Activities;

[Activity(
    Namespace = "ABP",
    Category = "Deployment",
    DisplayName = "Deploy Site",
    Description = "Deploys the site to the specified environment.",
    Kind = ActivityKind.Task)]
[FlowNode("Success", "Failure")]
public class DeploySite : Activity
{
    protected override async ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        Console.WriteLine("Deploying site...");
        await Task.Delay(1000); // Simulate deployment delay
        Console.WriteLine("Site deployed successfully!");
        await context.CompleteActivityWithOutcomesAsync("Success");
    }
}