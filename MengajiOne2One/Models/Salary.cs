using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(Salary_RecordMetaDeta))]
    public partial class Salary_Record
    {
        public class Salary_RecordMetaDeta
        {
            [DisplayName("ID Elaun")]
            public int sal_ID { get; set; }

            [DisplayName("Amaun")]
            public Nullable<double> sal_amount { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Tarikh")]
            [DataType(DataType.Date)]
            public System.DateTime sal_date { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("ID Tutor")]
            public string sal_teacherID { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Bulan Elaun")]
            public string sal_month { get; set; }
            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Tahun Elaun")]
            public string sal_year { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Status")]
            public string sal_status { get; set; }


            public virtual User_Record User_Record { get; set; }
        }
    }
}