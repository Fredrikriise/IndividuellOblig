using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IndividuellOblig.Models;

namespace IndividuellOblig.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KundeServiceController : Controller
    {
        private readonly KundeServiceContext _dbcontext;

        //Metode for tilkobling til database
        public KundeServiceController(KundeServiceContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //GET api / Lister alle faq
        [HttpGet]
        public JsonResult Get()
        {
            var kundeserviceDb = new KundeServiceDB(_dbcontext);
            List<faq> alleFaq = kundeserviceDb.hentAlleFaq();
            return Json(alleFaq);
        }

        //GET api / Henter spesifikk faq
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var kundeserviceDb = new KundeServiceDB(_dbcontext);
            faq enFaq = kundeserviceDb.hentEnFaq(id);
            return Json(enFaq);
        }

        // POST api / Oppretter ny faq 
        [HttpPost]
        public JsonResult Post([FromBody]faq innFaq)
        {
            if (ModelState.IsValid)
            {
                var kundeserviceDb = new KundeServiceDB(_dbcontext);
                bool OK = kundeserviceDb.largeEnFaq(innFaq);
                if (OK)
                {
                    return Json("Opprettelse av ny FAQ var suksessfult!");
                }
            }
            return Json("Opprettelse av ny FAQ feilet!");
        }

        // PUT api / Endring av FAQ 
        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody]faq innFaq)
        {
            if (ModelState.IsValid)
            {
                var kundeserviceDb = new KundeServiceDB(_dbcontext);
                bool OK = kundeserviceDb.endreEnFaq(id, innFaq);
                if (OK)
                {
                    return Json("FAQ endret!");
                }
            }
            return Json("Endring av FAQ feilet!");
        }

        // DELETE api / Sletting av FAQ
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var kundeserviceDb = new KundeServiceDB(_dbcontext);
            bool OK = kundeserviceDb.slettEnFaq(id);
            if (!OK)
            {
                return Json("Sletting av FAQ feilet!");
            }
            return Json("FAQ suksessfult slettet!");
        }
    }
}
