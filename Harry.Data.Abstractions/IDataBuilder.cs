using Microsoft.Extensions.DependencyInjection;

namespace Harry.Data
{
    public interface IDataBuilder
    {
        IServiceCollection Services { get; }
    }
}
