using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesContoller : ControllerBase
    {
        private List<Devise> devises;
        public List<Devise> _devises
        {
            get { return devises; }
            set { devises = value; }
        }


        ///<summary>Get the currency list.</summary>
        ///<returns>Http response</returns>
        ///<response code = "200" > When the currency list is return</response>
        // GET: api/<DeviseContoller>
        [ProducesResponseType(200)]
        [HttpGet]
        public IEnumerable<Devise> GetById()
        {
            return _devises;
        }

        ///<summary>Get a single currency.</summary>
        ///<returns>Http response</returns>
        ///<param name = "id" > The id of the currency</param>
        ///<response code = "200" > When the currency id is found</response>
        ///<response code = "404" > When the currency id is not found</response>
        // GET api/<DeviseContoller>/5
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise =
                    (from d in devises
                     where d.Id == id
                     select d).FirstOrDefault();
            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        ///<summary>Post a single currency.</summary>
        ///<returns>Http response</returns>
        ///<param name = "devise" > The instance of the currency</param>
        ///<response code = "201" > When the currency is create</response>
        ///<response code = "400" > When bad request</response>
        // POST api/<DeviseContoller>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        ///<summary>Change a single currency.</summary>
        ///<returns>Http response</returns>
        ///<param name = "id" > The id of the currency</param>
        ///<param name = "devise" > The instance of the currency</param>
        ///<response code = "204" > When the currency changment is available</response>
        ///<response code = "400" > When bad request</response>
        // PUT api/<DeviseContoller>/5
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();
        }

        ///<summary>Delete a single currency.</summary>
        ///<returns>Http response</returns>
        ///<param name = "id" > The id of the currency</param>
        ///<response code = "200" > When the currency is delete</response>
        ///<response code = "404" > When the currency id is not found</response>
        // DELETE api/<DeviseContoller>/5
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public ActionResult<Devise> Delete(int id)
        {
            var delDevise = GetById(id);

            if (delDevise.Value == null)
            {
                return NotFound();
            }

            _devises.Remove(_devises[id]);
            return delDevise;
        }

        public DevisesContoller()
        {
            _devises = new List<Devise>();

            _devises.Add(new Devise(1, "Dollar", 1.08));
            _devises.Add(new Devise(2, "Franc Suisse", 1.07));
            _devises.Add(new Devise(3, "Yen", 120));
        }
    }
}
