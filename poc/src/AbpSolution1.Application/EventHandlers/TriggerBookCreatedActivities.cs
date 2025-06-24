using System.Collections.Generic;
using System.Threading.Tasks;
using AbpSolution1.Activities;
using AbpSolution1.Books;
using AbpSolution1.Stimuli;
using Elsa.Workflows.Helpers;
using Elsa.Workflows.Runtime;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace AbpSolution1.EventHandlers;

[UsedImplicitly]
public class TriggerBookCreatedActivities(IStimulusDispatcher stimulusDispatcher) : ILocalEventHandler<BookCreatedEvent>, ITransientDependency
{
    public async Task HandleEventAsync(BookCreatedEvent eventData)
    {
        await stimulusDispatcher.SendAsync(new()
        {
            ActivityTypeName = ActivityTypeNameHelper.GenerateTypeName<BookCreated>(),
            Stimulus = new BookCreatedStimulus(eventData.Book.Type),
            Metadata = new()
            {
                Input = new Dictionary<string, object>
                {
                    [nameof(BookDto)] = eventData.Book,
                }
            }
        });
    }
}