using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestAPI.HyperMedia
{
    public interface IResponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}
