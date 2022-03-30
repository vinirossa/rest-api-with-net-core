using System.Collections.Generic;

namespace RestAPI.Data.Parsers
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origin);
    }
}
