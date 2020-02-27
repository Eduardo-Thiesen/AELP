using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Models
{
    public class Result
    {
        [JsonProperty("displacements")]
        public List<NodeDisplacements> Displacements { get; set; }

        [JsonProperty("reactions")]
        public List<ElementStress> Stresses { get; set; }
    }

    public class NodeDisplacements
    {
        [JsonProperty("node")]
        public int Node { get; set; }

        [JsonProperty("dx")]
        public double Dx { get; set; }

        [JsonProperty("dy")]
        public double Dy { get; set; }

        [JsonProperty("rz")]
        public double Rz { get; set; }
    }

    public class ElementStress
    {
        [JsonProperty("element")]
        public int Element { get; set; }

        [JsonProperty("ni")]
        public double Ni { get; set; }

        [JsonProperty("qi")]
        public double Qi { get; set; }

        [JsonProperty("mi")]
        public double Mi { get; set; }

        [JsonProperty("nj")]
        public double Nj { get; set; }

        [JsonProperty("qj")]
        public double Qj { get; set; }

        [JsonProperty("mj")]
        public double Mj { get; set; }
    }
}
