using Assignment.Services.StoryServices;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Services.CommonService
{
    public class ServiceInjector
    {
        public ServiceInjector(IServiceCollection services)
        {
            services.AddTransient(typeof(HttpClient));
            services.AddScoped(typeof(IStoryService), typeof(StoryService));
        }
    }                                                           
}
