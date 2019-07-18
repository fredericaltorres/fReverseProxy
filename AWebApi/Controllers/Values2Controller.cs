using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Values2Controller : ControllerBase
    {
        // GET api/values2
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var l = new List<string> { "FRED", "value2", DateTime.Now.ToShortDateString(), DateTime.Now.ToString() };
            var tutus = base.Request.Headers["tutu"].ToList();
            if (tutus.Count > 0)
                l.Add(tutus[0]);
            return l;
        }

        // GET api/values2/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"valid {id}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
