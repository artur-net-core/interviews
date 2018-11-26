using System.Collections.Generic;

namespace LoadBalancer.Host.Config
{
    public class Config
    {
        public IReadOnlyList<string> Hosts { get; set; }
    }
}
