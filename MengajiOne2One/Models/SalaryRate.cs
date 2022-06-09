using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(Salary_RateMetaDeta))]
    public partial class Salary_Rate
    {
        public class Salary_RateMetaDeta
        {
            
            public int sr_id { get; set; }
            [DisplayName("Kadar Elaun Per Jam")]
            public int sr_val { get; set; }
        }
    }
}