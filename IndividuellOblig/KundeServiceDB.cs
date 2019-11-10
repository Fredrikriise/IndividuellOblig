using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndividuellOblig.Models;

namespace IndividuellOblig
{
    public class KundeServiceDB
    {
        private readonly KundeServiceContext _dbcontext;

        //Tilkobling til db
        public KundeServiceDB(KundeServiceContext dbcontext) {
            _dbcontext = dbcontext;
            _dbcontext.Database.EnsureCreated();
        }

        //Metoder for faq
        public List<faq> hentAlleFaq()
        {
            List<faq> alleFaq = _dbcontext.AlleFaq.Select(f => new faq()
            {
                id = f.id,
                sporsmal = f.sporsmal,
                svar = f.svar
            }).ToList();

            return alleFaq;
        }

        public faq hentEnFaq(int id)
        {
            //Bruker FirstOrDefault etter som at lazy loading ikke støttes av core
            FAQ enDBFaq = _dbcontext.AlleFaq.FirstOrDefault(f => f.id == id);

            var enFaq = new faq()
            {
                id = enDBFaq.id,
                sporsmal = enDBFaq.sporsmal,
                svar = enDBFaq.svar
            };
            return enFaq;
        }

        public bool largeEnFaq(faq innFaq)
        {
            var nyFaq = new FAQ
            {
                id = innFaq.id,
                sporsmal = innFaq.sporsmal,
                svar = innFaq.svar
            };

            try
            {
                _dbcontext.AlleFaq.Add(nyFaq);
                _dbcontext.SaveChanges();
            } 
            catch (Exception error)
            {
                return false;
            }
            return true;
        }

        public bool endreEnFaq(int id, faq innFaq)
        {
            //Bruker FirstOrDefault etter som at lazy loading ikke støttes av core
            FAQ funnetFaq = _dbcontext.AlleFaq.FirstOrDefault(f => f.id == id);
            if(funnetFaq == null)
            {
                return false;
            }

            funnetFaq.sporsmal = innFaq.sporsmal;
            funnetFaq.svar = innFaq.svar;

            try
            {
                _dbcontext.SaveChanges();
            }
            catch(Exception error)
            {
                return false;
            }
            return true;
        }

        public bool slettEnFaq(int id)
        {
            try
            {
                //Bruker FirstOrDefault etter som at lazy loading ikke støttes av core
                FAQ finnFaq = _dbcontext.AlleFaq.FirstOrDefault(f => f.id == id);
                _dbcontext.AlleFaq.Remove(finnFaq);
                _dbcontext.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }
        
        //Metoder for innsporsmal
        public bool lagreEtSporsmal(innsporsmal innSporsmal)
        {
            var nyttSporsmal = new InnSporsmal
            {
                sporsmal = innSporsmal.sporsmal
            };

            try
            {
                _dbcontext.Add(nyttSporsmal);
                _dbcontext.SaveChanges();
            }
            catch (Exception error)
            {
                return false;
            }
            return true;
        }

        public List<innsporsmal> hentAlleInnSporsmal()
        {
            List<innsporsmal> alleInnSporsmal = _dbcontext.AlleInnSporsmal.Select(s => new innsporsmal()
            {
                id = s.id,
                sporsmal = s.sporsmal
            }).ToList();
            return alleInnSporsmal;
        }
    }
}
