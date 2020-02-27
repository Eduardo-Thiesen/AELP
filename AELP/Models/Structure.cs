using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Models
{
    /// <summary>
    /// Representa a estrutura a ser analisada.
    /// </summary>
    public class Structure
    {
        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }

        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }

        [JsonProperty("nodalLoads")]
        public List<NodalLoad> NodalLoads { get; set; }

        [JsonProperty("elementLoads")]
        public List<ElementLoad> ElementLoads { get; set; }
    }
}
