using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Utils
{
    public static class Matrix2DUtils
    {   
        /// <summary>
        /// Cria a matriz transposta.
        /// </summary>
        /// <param name="matrix">Matriz da qual se quer obter a transposta</param>
        public static double[,] CreateTranspose(double[,] matrix)
        {
            int iLength = matrix.GetLength(0);
            int jLength = matrix.GetLength(1);
            var transpose = new double[iLength, jLength];

            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    transpose[j, i] = matrix[i, j];
                }
            }

            return transpose;
        }

        /// <summary>
        /// Faz o produto de uma matriz com um vetor
        /// </summary>
        /// <param name="A"></param>
        /// <param name="Vector"></param>
        /// <returns></returns>
        public static double[] Product(this double[,] A, double[] Vector)
        {
            int iLength = A.GetLength(0);
            int jLength = A.GetLength(1);

            if (Vector.Length != jLength) return null;
            var result = new double[jLength];

            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    result[i] += A[i, j] * Vector[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Faz o produto de uma matriz com outra matriz
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double[,] Product(this double[,] A, double[,] B)
        {
            int iLength = A.GetLength(0);
            int jLength = B.GetLength(0);
            int kLength = B.GetLength(1);

            //TODO throw exception to specify matrix dimensions don't match
            if (A.GetLength(1) != jLength) return null;
            var result = new double[iLength, kLength];

            for (int i = 0; i < iLength; i++)
            {
                for (int k = 0; k < kLength; k++)
                {
                    for (int j = 0; j < jLength; j++)
                    {
                        result[i, k] += A[i, j] * B[j, k];
                    }
                }
            }

            return result;
        }
    }
}
