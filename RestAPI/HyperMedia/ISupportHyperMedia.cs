using System.Collections.Generic;

namespace RestAPI.HyperMedia
{
    public interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
