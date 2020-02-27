using AELEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AELEP.Utils;

namespace AELEP.Services
{
    /// <summary>
    /// Classe responsável pela montagem do vetor de carregamentos aplicados, no referencial global.
    /// </summary>
    public class LoadsService
    {
        /// <summary>
        /// Gera o vetor de carregamentos no referencial global.
        /// </summary>
        /// <param name="structure">estrutura a ser analisada</param>
        /// <param name="coordCount">Lista de carregamentos aplicados nos elementos</param>
        /// <returns></returns>
        public static double[] GetLoadsVector(Structure structure, int coordCount)
        {
            var F = new double[coordCount];

            F = ApplyNodalLoads(structure.NodalLoads, structure.Nodes, F);
            F = ApplyElementLoads(structure.Elements, structure.ElementLoads, F);

            return F;
        }

        /// <summary>
        /// Faz a contribuição das cargas nodais no vetor de cargas no referencial global.
        /// </summary>
        /// <param name="nodalLoads">Lista de carregamentos nodais aplicados</param>
        /// <returns></returns>
        private static double[] ApplyNodalLoads(List<NodalLoad> nodalLoads, List<Node> nodes, double[] F)
        {
            foreach (var load in nodalLoads)
            {
                var node = nodes.Where(n => n.Number == load.Node).FirstOrDefault();
                for (int i = 0; i < node.GlobalCoords.Count(); i++)
                {
                    int coord = node.GlobalCoords[i];

                    if (coord == 0)
                    {
                        continue;
                    }
                    else if (i == 0)
                    {
                        F[coord - 1] += load.Fx;
                    }
                    else if (i == 1)
                    {
                        F[coord - 1] += load.Fy;
                    }
                    else if (i == 2)
                    {
                        F[coord - 1] += load.Mz;
                    }
                }
            }

            return F;
        }

        public static double[] GetElementReactions(Element elem, ElementLoad load)
        {
            var f = new double[elem.GlobalDispVector.Length];

            // Seno e cosseno do elemento no sistema global
            double L = elem.GetElementLength();
            double SG = (elem.J.Y - elem.I.Y) / L;
            double CG = (elem.J.X - elem.I.X) / L;

            // Seno e cosseno do carregamento no sistema global
            double ST = load.Direction.Y;
            double CT = load.Direction.X;

            double B;
            switch (load.LoadType)
            {
                case LoadType.Concentrated:
                    double FA = (CG * CT + SG * ST) * load.Qi;
                    double FB = (-SG * CT + CG * ST) * load.Qi;
                    B = L - load.I;

                    f[0] = -(FA * B) / L;
                    f[3] = -(FA * B) / L;
                    f[2] = -(FB * load.I * Math.Pow(B, 2)) / Math.Pow(L, 2);
                    f[5] = (FB * Math.Pow(load.I, 2) * B) / Math.Pow(L, 2);
                    f[1] = -(FB * B) / L + f[2] / L + f[5] / L;
                    f[4] = -(FB * load.I) / L - f[2] / L - f[5] / L;
                    break;
                case LoadType.Trapezoidal:
                    double Qi = -(-SG * CT + CG * ST) * load.Qi;
                    double Qj = -(-SG * CT + CG * ST) * load.Qj;
                    double Wi = -(CG * CT + SG * ST) * load.Qi;
                    double Wj = -(CG * CT + SG * ST) * load.Qj;
                    double A = load.I;
                    double C = load.J - load.I;
                    B = L - load.I - C;

                    f[0] = ((L - A - B) / (6 * L)) * ((Wi * (2 * L - 2 * A + B)) + (Wj * (L - A + 2 * B)));
                    f[3] = ((L - A - B) / (6 * L)) * ((Wi * (L + 2 * A - B)) + (Wj * (2 * L + A - 2 * B)));
                    f[2] = ((Qi * Math.Pow(L - A, 3)) / (60 * Math.Pow(L, 2))) * (3 * (L + 4 * A)
                        - (B * (2 * L + 3 * A) / (L - A)) * (1 + (B / (L - A))
                        + (Math.Pow(B, 2) / Math.Pow(L - A, 2)))
                        + (3 * Math.Pow(B, 4) / Math.Pow(L - A, 3)))
                        + Qj * (Math.Pow(L - A, 3) / (60 * Math.Pow(L, 2)) * ((2 * L + 3 * A)
                        * (1 + (B / (L - A)) + (Math.Pow(B, 2) / Math.Pow(L - A, 2))) - (3 * Math.Pow(B, 3) / Math.Pow(L - A, 2))
                        * (1 + ((5 * L - 4 * B) / (L - A)))));
                    f[1] = (Qi * Math.Pow(L - A, 3) / (20 * Math.Pow(L, 3))) * (7 * L + 8 * A
                        - (B * (3 * L + 2 * A) / (L - A)) * (1 + (B / (L - A))
                        + (Math.Pow(B, 2) / Math.Pow(L - A, 2))) + ((2 * Math.Pow(B, 4)) / Math.Pow(L - A, 3)))
                        + (Qj * Math.Pow(L - A, 3) / (20 * Math.Pow(L, 3)))
                        * ((3 * L + 2 * A) * (1 + (B / (L - A))
                        + (Math.Pow(B, 2) / Math.Pow(L - A, 2)))
                        - (Math.Pow(B, 3) / (Math.Pow(L - A, 2)))
                        * (2 + ((15 * L - 8 * B) / (L - A))));
                    f[5] = ((((L - A - B) / 6) * ((Qi * (-2 * L + 2 * A - B))
                        - (Qj * (L - A + 2 * B))))
                        + f[1] * L - f[2]);
                    f[4] = ((((Qi + Qj) / 2) * (L - A - B)) - f[1]);
                    break;
                case LoadType.Moment:
                    double M = load.Qi;
                    B = L - load.I;

                    f[0] = 0;
                    f[3] = 0;
                    f[2] = (M * B / Math.Pow(L, 2)) * (2 * load.I - B);
                    f[1] = 6 * M * load.I * B / Math.Pow(L, 3);
                    f[5] = (M * load.I / Math.Pow(L, 2)) * (2 * B - load.I);
                    f[4] = -f[1];
                    break;
                case LoadType.UniformTemp:
                    f[0] = elem.Section.Area * elem.Material.E * elem.Material.ThermalExpCoef * load.Qi;
                    f[1] = 0;
                    f[2] = 0;
                    f[3] = -(elem.Section.Area * elem.Material.E * elem.Material.ThermalExpCoef * load.Qi);
                    f[4] = 0;
                    f[5] = 0;
                    break;
                case LoadType.TempGradient:
                    f[0] = 0;
                    f[1] = 0;
                    f[2] = elem.Material.E * elem.Section.Iz * elem.Material.ThermalExpCoef * (load.Qi - load.Qj);
                    f[3] = 0;
                    f[4] = 0;
                    f[5] = -(elem.Material.E * elem.Section.Iz * elem.Material.ThermalExpCoef * (load.Qi - load.Qj));
                    break;
                default:
                    break;
            }

            return f;
        }

        /// <summary>
        /// Faz a contribuição das cargas nos elementos no vetor de cargas no referencial global.
        /// </summary>
        /// <param name="elemLoads">Lista de carregamentos aplicados nos elementos</param>
        /// <returns></returns>
        private static double[] ApplyElementLoads(List<Element> elements, List<ElementLoad> elemLoads, double[] F)
        {
            foreach (var load in elemLoads)
            {
                var elem = elements.Where(e => e.Number == load.Element).FirstOrDefault();
                var f = GetElementReactions(elem, load);

                var R = elem.GetRotationMatrix();
                var T = Matrix2DUtils.CreateTranspose(R);

                f = T.Product(f);

                // Adição das forças do elemento no vetor de carga global
                for (int j = 0; j < f.Length; j++)
                {
                    var nodes = new List<Node>() { elem.I, elem.J };
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        var node = nodes[i];

                    }

                    int coord = elem.GlobalDispVector[j];
                    if (coord == 0)
                    {
                        continue;
                    }
                    else
                    {
                        F[coord - 1] -= f[j];
                    }
                }
            }

            return F;
        }
    }
}
