using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.HyperMedia
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }

        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (!(context.Result is OkObjectResult okObjectResult))
                return;

            var enricher = _hyperMediaFilterOptions
                .ContentResponseEnrichers
                .FirstOrDefault(x => x.CanEnrich(context));

            if (enricher == null)
                return;

            Task.FromResult(enricher.Enrich(context));
        }
    }
}
