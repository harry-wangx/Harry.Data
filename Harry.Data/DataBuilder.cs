using Microsoft.Extensions.DependencyInjection;

namespace Harry.Data
{
    public class DataBuilder : IDataBuilder
    {
        public DataBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
