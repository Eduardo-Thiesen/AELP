using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Models
{
    public class Node
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("restrictions")]
        public int[] Restrictions { get; set; }

        /// <summary>
        /// Guarda a numeração das coordenadas globais da estrutura.
        /// </summary>
        public int[] GlobalCoords { get; set; }

        /// <summary>
        /// Faz a numeração das coordenadas globais da estrutura e retorna o número de coordenadas.
        /// Obs: As coordenadas são sempre numeradas na ordem [Dx, Dy, Mz] e preenchidas com 0 se o deslocamento for impedido.
        /// </summary>
        /// <param name="nodes">Lista de nós da estrutura</param>
        public static int SetGlobalCoords(List<Node> nodes)
        {
            int count = 0;
            foreach (var node in nodes)
            {
                var coords = new int[node.Restrictions.Count()];
                for (int i = 0; i < node.Restrictions.Count(); i++)
                {
                    int rest = node.Restrictions[i];
                    if (rest == 0)
                    {
                        count++;
                        coords[i] = count;
                    }
                    else
                    {
                        coords[i] = 0;
                    }
                }

                node.GlobalCoords = coords;
            }

            return count;
        }
    }
}
