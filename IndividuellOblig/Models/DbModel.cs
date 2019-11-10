using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOblig.Models
{
    public class FAQ
    {
        [Key]
        public int id { get; set; }
        public string sporsmal { get; set; }
        public string svar { get; set; }
    }

    public class InnSporsmal
    {
        [Key]
        public int id { get; set; }
        public string sporsmal { get; set; }
    }

    public class KundeServiceContext : DbContext
    {
        public KundeServiceContext(DbContextOptions<KundeServiceContext> options)
        : base(options) { }

        public DbSet<FAQ> AlleFaq { get; set; }
        public DbSet<InnSporsmal> AlleInnSporsmal { get; set; }
    }
}
