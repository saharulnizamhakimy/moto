using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(User_RecordMetaDeta))]
    public partial class User_Record
    {
        public class User_RecordMetaDeta
        {
            [Required(ErrorMessage ="Ruangan ini perlu diisi")]
            [DisplayName("ID")]
            public string u_id { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nama")]
            public string u_name { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Kata Laluan")]
            public string u_pwd { get; set; }



            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nombor Telefon")]
            public string u_contactNo { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Jenis")]
            public string u_type { get; set; }

            [DisplayName("Emel")]
            public string u_email { get; set; }
        }
    }
}