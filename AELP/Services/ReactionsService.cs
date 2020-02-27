using AELEP.Models;
using AELEP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Services
{
    public class ReactionsService
    {
        public static List<NodeDisplacements> GetNodalDisplacements(List<Node> nodes, double[] solutions)
        {
            var displacements = new List<NodeDisplacements>();

            foreach (var node in nodes)
            {
                var nodeDisplacements = new NodeDisplacements();
                nodeDisplacements.Node = node.Number;

                for (int i = 0; i < node.GlobalCoords.Length; i++)
                {
                    int coord = node.GlobalCoords[i];
                    if (i == 0)
                    {
                        if (coord == 0)
                        {
                            nodeDisplacements.Dx = 0;
                        }
                        else
                        {
                            nodeDisplacements.Dx = solutions[coord - 1];
                        }
                    }
                    else if (i == 1)
                    {
                        if (coord == 0)
                        {
                            nodeDisplacements.Dy = 0;
                        }
                        else
                        {
                            nodeDisplacements.Dy = solutions[coord - 1];
                        }
                    }
                    else if (i == 2)
                    {
                        if (coord == 0)
                        {
                            nodeDisplacements.Rz = 0;
                        }
                        else
                        {
                            nodeDisplacements.Rz = solutions[coord - 1];
                        }
                    }
                }

                displacements.Add(nodeDisplacements);
            }

            return displacements;
        }

        public static List<ElementStress> GetElementStress(Structure structure, double[] solutions)
        {
            var stresses = new List<ElementStress>();

            var displacements = GetNodalDisplacements(structure.Nodes, solutions);
            foreach (var elem in structure.Elements)
            {
                var nodes = new List<Node>() { elem.I, elem.J };
                var dispVector = new double[elem.GlobalDispVector.Length];
                for (int i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var disp = displacements.Where(d => d.Node == node.Number).FirstOrDefault();

                    if (i == 0)
                    {
                        dispVector[0] = disp.Dx;
                        dispVector[1] = disp.Dy;
                        dispVector[2] = disp.Rz;
                    }
                    else if (i == 1)
                    {
                        dispVector[3] = disp.Dx;
                        dispVector[4] = disp.Dy;
                        dispVector[5] = disp.Rz;
                    }
                }

                var R = elem.GetRotationMatrix();
                var dEl = R.Product(dispVector);

                var kElemL = StiffnessMatrixService.GetLocalStiffnessMatrix(elem);

                var stressVector = kElemL.Product(dEl);

                // Preenchimento do objeto referente aos esforços.
                var elemStress = new ElementStress();
                elemStress.Element = elem.Number;
                elemStress.Ni = stressVector[0];
                elemStress.Qi = stressVector[1];
                elemStress.Mi = stressVector[2];
                elemStress.Nj = stressVector[3];
                elemStress.Qj = stressVector[4];
                elemStress.Mj = stressVector[5];

                stresses.Add(elemStress);
            }

            return stresses;
        }
    }
}
