using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOblig.Models
{
    public class innsporsmal
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ.0-9 \\-]{5,9999}$")]
        public string sporsmal { get; set; }
    }
}
