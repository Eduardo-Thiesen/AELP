using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Utils
{
    /// <summary>
    /// Contém métodos para resolução de sistemas de equações
    /// </summary>
    public class Solver
    {
        /// <summary>
        /// Resolve o sistema simétrico de equações algébricas lineares armazenado em perfil.
        /// </summary>
        /// <param name="K">Matriz de rigidez da estrutura no referencial global armazenada em perfil</param>
        /// <param name="F">Vetor de cargas aplicadas no referencial global</param>
        /// <param name="pv">vetor apontador da matriz de rigidez armazenada em perfil</param>
        /// <param name="coordCount"></param>
        /// <param name="gaussK">Decomposição de Gauss da matriz K</param>
        /// <param name="solutions">Vetor com as soluções do sistema</param>
        /// <summary>
        public static void SolveSystem(double[] K, double[] F, int[] pv, int coordCount, out double[] gaussK, out double[] solutions)
        {
            int lj;
            int li;
            int ko;
            int ij;
            int ki;
            int kj;

            //Triangularização
            for (int j = 2; j <= coordCount; j++)
            {
                lj = j - pv[j - 1] + pv[j - 2] + 1;
                for (int i = (lj + 1); i <= (j - 1); i++)
                {
                    li = i - pv[i - 1] + pv[i - 2] + 1;
                    if (li > lj)
                    {
                        ko = li;
                    }
                    else
                    {
                        ko = lj;
                    }
                    for (int k = ko; k <= (i - 1); k++)
                    {
                        ij = pv[j - 1] - j + i;
                        ki = pv[i - 1] - i + k;
                        kj = pv[j - 1] - j + k;
                        K[ij - 1] = K[ij - 1] - K[ki - 1] * K[kj - 1];
                    }
                }

                for (int k = lj; k <= (j - 1); k++)
                {
                    kj = pv[j - 1] - j + k;
                    int pvJ = pv[j - 1];
                    int pvK = pv[k - 1];
                    K[pvJ - 1] = K[pvJ - 1] - (K[kj - 1] / K[pvK - 1]) * K[kj - 1];
                    K[kj - 1] = K[kj - 1] / K[pvK - 1];
                }
            }
            // Substituição
            for (int i = 2; i <= coordCount; i++)
            {
                li = i - pv[i - 1] + pv[i - 2] + 1;
                for (int k = li; k < (i - 1); k++)
                {
                    ki = pv[i - 1] - i + k;
                    F[i - 1] = F[i - 1] - K[ki - 1] * F[k - 1];
                }
            }
            for (int i = 1; i <= coordCount; i++)
            {
                int pvI = pv[i - 1];
                F[i - 1] = F[i - 1] / K[pvI - 1];
            }

            //Retro-substituição
            for (int i = coordCount; i >= 2; i--)
            {
                li = i - pv[i - 1] + pv[i - 2] + 1;
                for (int k = li; k <= (i - 1); k++)
                {
                    ki = pv[i - 1] - i + k;
                    F[k - 1] = F[k - 1] - K[ki - 1] * F[i - 1];
                }
            }

            gaussK = K;
            solutions = F;
        }
    }
}
