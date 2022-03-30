using System.Collections.Generic;

namespace RestAPI.HyperMedia
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnrichers { get; set; } = new List<IResponseEnricher>();
    }
}
