using AELEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AELEP.Utils;

namespace AELEP.Services
{
    /// <summary>
    /// Classe responsável pela montagem da matriz de rigidez global da estrutura.
    /// </summary>
    public class StiffnessMatrixService
    {
        /// <summary>
        /// Gera a matriz de rigidez global da estrutura, armazenada pelo método do perfil.
        /// </summary>
        /// <param name="elements">Lista de elementos da estrutura</param>
        /// <param name="pv">Vetor apontador</param>
        /// <returns>Matriz de rigidez e vetor apontador</returns>
        public static double[] GetGlobalStiffnessMatrix(List<Element> elements, int[] pv)
        {
            var kGlobal = new double[pv.LastOrDefault()];

            foreach (var elem in elements)
            {
                var kElemL = GetLocalStiffnessMatrix(elem);
                var R = elem.GetRotationMatrix();
                var Rt = Matrix2DUtils.CreateTranspose(R);

                var kElemG = (Rt.Product(kElemL)).Product(R);

                // Soma das contribuições da rigidez do elemento na matriz global
                var displacements = elem.GlobalDispVector;
                for (int k = 0; k < displacements.Length; k++)
                {
                    int i = displacements[k];
                    if (i <= 0) continue;

                    for (int l = 0; l < displacements.Length; l++)
                    {
                        int j = displacements[l];
                        if (j >= i)
                        {
                            var pos = pv[j - 1] + i - j;
                            kGlobal[pos - 1] += kElemG[k, l];
                        }
                    }
                }
            }

            return kGlobal;
        }

        /// <summary>
        /// Gera o vetor apontador.
        /// </summary>
        /// <param name="elements">Lista de elementos da estrutura</param>
        /// <param name="coordCount">Número de coordenadas globais da estrutura</param>
        public static int[] GetPointer(List<Element> elements, int coordCount)
        {
            // Operação preliminar
            var pv = new int[coordCount];
            for (int i = 0; i < coordCount; i++)
            {
                pv[i] = coordCount + 1;
            }

            foreach (var elem in elements)
            {
                var displacements = elem.GlobalDispVector;
                int min = 1;
                foreach (int disp in displacements)
                {
                    if (disp > 0 && disp <= min)
                    {
                        min = disp;
                    }
                }

                foreach (int disp in displacements)
                {
                    if (disp == 0)
                    {
                        continue;
                    }
                    else if (pv[disp - 1] > min)
                    {
                        pv[disp - 1] = min;
                    }
                }
            }

            // Cálculo da altura do perfil
            for (int i = 2; i < coordCount + 1; i++)
            {
                pv[i - 1] = i - pv[i - 1] + 1;
            }

            // Acumulação do vetor apontador
            for (int i = 1; i < coordCount; i++)
            {
                pv[i] += pv[i - 1];
            }

            return pv;
        }

        /// <summary>
        /// Gera a matriz de rigidez do elemento paralela às coordenadas locais do mesmo.
        /// </summary>
        /// <param name="elem">Elemento para o qual se deseja obter a matriz de rigidez</param>
        public static double[,] GetLocalStiffnessMatrix(Element elem)
        {
            //Inicia a matriz zerada
            var kElem = new double[6, 6];

            //Propriedades do elemento
            double A = elem.Section.Area;
            double Iz = elem.Section.Iz;
            double E = elem.Material.E;
            double L = elem.GetElementLength();

            //Preenchimento da matriz de rigidez da diagonal principal para cima
            //Obs: em C# os índices de arrays começam a partir do 0, portanto o índice [0,0] corresponde ao índice K11 da matriz
            kElem[0, 0] = E * A / L;
            kElem[0, 3] = -(E * A / L);
            kElem[1, 1] = 12 * E * Iz / Math.Pow(L, 3);
            kElem[1, 2] = 6 * E * Iz / Math.Pow(L, 2);
            kElem[1, 4] = -(12 * E * Iz / Math.Pow(L, 3));
            kElem[1, 5] = 6 * E * Iz / Math.Pow(L, 2);
            kElem[2, 2] = 4 * E * Iz / L;
            kElem[2, 4] = -(6 * E * Iz / Math.Pow(L, 2));
            kElem[2, 5] = 2 * E * Iz / L;
            kElem[3, 3] = E * A / L;
            kElem[4, 4] = 12 * E * Iz / Math.Pow(L, 3);
            kElem[4, 5] = -(6 * E * Iz / Math.Pow(L, 2));
            kElem[5, 5] = 4 * E * Iz / L;

            //Preenchimento do restante da matriz
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (j > i)
                    {
                        kElem[j, i] = kElem[i, j];
                    }
                }
            }

            return kElem;
        }
    }
}
