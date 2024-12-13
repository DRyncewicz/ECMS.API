using ecms.Infrastructure.BackgroundJobs.EmailProcessor;
using Hangfire;

namespace ecms.API.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        IRecurringJobManager jobManager = app.Services.GetService<IRecurringJobManager>();

        jobManager.AddOrUpdate<EmailProcessorJob>("email-processor", job => job.ProcessAsync(), app.Configuration["BackgroundJobs:Emails:Schedule"]);

        return app;
    }
}