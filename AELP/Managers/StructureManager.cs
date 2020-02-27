using AELEP.Models;
using AELEP.Services;
using AELEP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AELEP.Managers
{
    public class StructureManager
    {
        public Structure structure;

        public StructureManager(Structure structure)
        {
            this.structure = structure;
        }

        public Result SolveStructure()
        {
            try
            {
                int coordCount = Node.SetGlobalCoords(structure.Nodes);
                foreach (var elem in structure.Elements)
                {
                    elem.SetNodes(structure.Nodes);
                    elem.SetGlobalDispVector();
                }

                var pv = StiffnessMatrixService.GetPointer(structure.Elements, coordCount);
                var kGlobal = StiffnessMatrixService.GetGlobalStiffnessMatrix(structure.Elements, pv);
                var F = LoadsService.GetLoadsVector(structure, coordCount);

                Solver.SolveSystem(kGlobal, F, pv, coordCount, out double[] gaussK, out double[] solutions);

                var result = new Result();

                var displacements = ReactionsService.GetNodalDisplacements(structure.Nodes, solutions);
                var stresses = ReactionsService.GetElementStress(structure, solutions);

                result.Displacements = displacements;
                result.Stresses = stresses;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
