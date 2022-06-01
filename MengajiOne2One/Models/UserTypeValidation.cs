using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(User_RecordMetaDeta))]
    public partial class User_Type
    {
        public class User_RecordMetaDeta
        {
            
            [DisplayName("Jenis Pengguna")]
            public string t_desc { get; set; }

        }
    }
}