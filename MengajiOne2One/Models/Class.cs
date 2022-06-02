using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(Class_RecordMetaDeta))]
    public partial class Class_Record
    {
        public class Class_RecordMetaDeta
        {
            [DisplayName("ID Kelas")]
            public int c_id { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Tarikh")]
            [DataType(DataType.Date)]
            public System.DateTime c_date { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nama Pelajar")]
            public string c_studentID { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Tempoh (Minit)")]
            public int c_duration { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nama Tutor")]
            public string c_teacherID { get; set; }
        }
    }
}