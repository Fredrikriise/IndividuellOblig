using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IndividuellOblig.Models;

namespace IndividuellOblig.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InnSporsmalController : Controller
    {
        private readonly KundeServiceContext _dbcontext;

        //Metode for tilkobling til database
        public InnSporsmalController(KundeServiceContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //GET api / Lister alle spørsmål
        [HttpGet]
        public JsonResult Get()
        {
            var kundeserviceDb = new KundeServiceDB(_dbcontext);
            List<innsporsmal> alleInnSporsmal = kundeserviceDb.hentAlleInnSporsmal();
            return Json(alleInnSporsmal);
        }

        // POST api / Oppretter nytt spørsmål 
        [HttpPost]
        public JsonResult Post([FromBody]innsporsmal innSporsmal)
        {
            if (ModelState.IsValid)
            {
                var kundeserviceDb = new KundeServiceDB(_dbcontext);
                bool OK = kundeserviceDb.lagreEtSporsmal(innSporsmal);
                if (OK)
                {
                    return Json("Opprettelse av nytt spørsmål var suksessfult!");
                }
            }
            return Json("Opprettelse av nytt spørsmål feilet!");
        }
    }
}
