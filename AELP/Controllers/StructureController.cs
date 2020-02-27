using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AELEP.Managers;
using AELEP.Models;
using AELEP.Services;
using AELEP.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AELEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructureController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetResult(string structure)
        {
            var parsedStructure = JsonConvert.DeserializeObject<Structure>(structure);
            var structManager = new StructureManager(parsedStructure);

            var result = structManager.SolveStructure();
            var resultJson = JsonConvert.SerializeObject(result);

            return Ok(resultJson);
        }
    }
}