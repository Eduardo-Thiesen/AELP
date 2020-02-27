using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Models
{
    public class NodalLoad
    {
        [JsonProperty("fx")]
        public double Fx { get; set; }

        [JsonProperty("fy")]
        public double Fy { get; set; }

        [JsonProperty("mz")]
        public double Mz { get; set; }

        [JsonProperty("node")]
        public int Node { get; set; }
    }

    public class ElementLoad
    {
        [JsonProperty("i")]
        public double I { get; set; }

        [JsonProperty("j")]
        public double J { get; set; }

        [JsonProperty("direction")]
        public XY Direction { get; set; }

        [JsonProperty("qi")]
        public double Qi { get; set; }

        [JsonProperty("qj")]
        public double Qj { get; set; }

        [JsonProperty("element")]
        public int Element { get; set; }

        [JsonProperty("loadType")]
        public LoadType LoadType { get; set; }
    }

    public enum LoadType
    {
        Concentrated = 1,
        Trapezoidal = 2,
        Moment = 3,
        UniformTemp = 4,
        TempGradient = 5
    }
}
