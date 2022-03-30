using System.Text;

namespace RestAPI.HyperMedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }

        private string _href;
        public string Href 
        { 
            get
            {
                var _lock = new object();
                lock (_lock)
                {
                    var sb = new StringBuilder(_href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set => _href = value; 
        }

        public string Type { get; set; }
        public string Action { get; set; }
    }
}
