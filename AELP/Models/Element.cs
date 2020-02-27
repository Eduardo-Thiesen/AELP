using AELEP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP
{
    /// <summary>
    /// Representa um elemento da estrutura.
    /// </summary>
    public class Element
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("i")]
        public int Inumber { get; set; }

        [JsonProperty("j")]
        public int Jnumber { get; set; }

        public Node I { get; set; }

        public Node J { get; set; }

        [JsonProperty("material")]
        public Material Material { get; set; }

        [JsonProperty("section")]
        public Section Section { get; set; }

        /// <summary>
        /// Vetor contendo os números das coordenadas globais para o elemento.
        /// </summary>
        public int[] GlobalDispVector { get; set; }

        /// <summary>
        /// Calcula o comprimento do elemento.
        /// </summary>
        public double GetElementLength()
        {
            double length = Math.Sqrt(Math.Pow(I.X - J.X, 2) + Math.Pow(I.Y - J.Y, 2));

            return length;
        }

        /// <summary>
        /// Faz o preenchimento do vetor de coordenadas globais do elemento.
        /// </summary>
        public void SetGlobalDispVector()
        {
            var nodes = new List<Node>() { I, J };

            var displacements = new List<int>();
            foreach (var node in nodes)
            {
                foreach (int coord in node.GlobalCoords)
                {
                    displacements.Add(coord);
                }
            }

            this.GlobalDispVector = displacements.ToArray();
        }

        public void SetNodes(List<Node> nodes)
        {
            I = nodes.Where(n => n.Number == this.Inumber).FirstOrDefault();
            J = nodes.Where(n => n.Number == this.Jnumber).FirstOrDefault();
        }

        /// <summary>
        /// Gera a matriz de rotação para o elemento de pórtico plano.
        /// </summary>
        /// <param name="elem">Elemento para que se deseja obter a matriz de rotação</param>
        public double[,] GetRotationMatrix()
        {
            //Inicia a matriz zerada
            var R = new double[6, 6];

            double L = this.GetElementLength();
            double sin = (this.J.Y - this.I.Y) / L;
            double cos = (this.J.X - this.I.X) / L;

            //Preenchimento da matriz
            R[0, 0] = cos;
            R[0, 1] = sin;
            R[1, 0] = -sin;
            R[1, 1] = cos;
            R[2, 2] = 1;
            R[3, 3] = cos;
            R[3, 4] = sin;
            R[4, 3] = -sin;
            R[4, 4] = cos;
            R[5, 5] = 1;

            return R;
        }
    }

    /// <summary>
    /// Representa o material de um elemento.
    /// </summary>
    public class Material
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("elasticityModule")]
        public double E { get; set; }

        [JsonProperty("poisson")]
        public double Poisson { get; set; }

        [JsonProperty("thermalExpCoef")]
        public double ThermalExpCoef { get; set; }
    }

    /// <summary>
    /// Representa a seção transversal de um elemento.
    /// </summary>
    public class Section
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("inertia")]
        public double Iz { get; set; }

        [JsonProperty("area")]
        public double Area { get; set; }
    }
}
