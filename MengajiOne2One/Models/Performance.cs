using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(Student_Performance_RecordMetaDeta))]
    public partial class Student_Performance_Record
    {
        public class Student_Performance_RecordMetaDeta
        {
            [DisplayName("ID Prestasi")]
            public int per_ID { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Ulasan Prestasi")]
            public string per_desc { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Tarikh")]
            [DataType(DataType.Date)]
            public System.DateTime per_date { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("ID Pelajar")]
            public string per_studentID { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Bulan")]
            public string per_month { get; set; }
        }
    }
}