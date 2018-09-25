using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdressBook_WebAPI_CORE.Controller
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/<controller>
        /// <summary>
        /// The list of all the Values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<Value> Get()
        {
            return new Value[] { new Value { Id = 1, Text = "value1" }, new Value { Id = 2, Text = "value2" } };
        }

        // GET api/<controller>/5
        /// <summary>
        /// This will provide all values for a specific Id that is being passed.
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id, string query)
        {            
            //$"value {id} query= {query}";
            return Ok(new Value { Id = id, Text = "value: " + query });
        }

        // POST api/<controller>
        /// <summary>
        /// This will post a Value object to insert new values.
        /// </summary>
        /// <param name="todo">Mandatory</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("applicarion/json", Type = typeof(Value))]
        public IActionResult Post([FromBody]Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }

        // PUT api/<controller>/

        /// <summary>
        /// This will update a Value by a specific Id
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <param name="value">The update information by the value.</param>

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5

        /// <summary>
        /// This will delete a Value object by a specific Id
        /// </summary>
        /// <param name="id">Mandatory</param>
        /// <returns></returns>
        /// 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        public class Value
        {
            public int Id { get; set; }
            [MinLength(3)]
            [Required]
            public string Text { get; set; }
        }
    }
}
