using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(Student_RecordMetaDeta))]
    public partial class Student_Record
    {
        public class Student_RecordMetaDeta
        {
            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("ID Pelajar")]
            public string s_id { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Kata Laluan")]
            public string s_pwd { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nama Pelajar")]
            public string s_name { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Umur")]
            [Range(1, 120, ErrorMessage = "Umur (Tahun) hendaklah berada dalam lingkungan 1 tahun hingga 120 tahun sahaja.")]
            public Nullable<int> s_age { get; set; }


            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Alamat")]
            public string s_address { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nombor Telefon")]
            public string s_contactNo { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Tarikh Daftar")]
            [DataType(DataType.Date)]
            public System.DateTime s_regDate { get; set; }

            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nama Tutor")]
            public string s_teacherID { get; set; }

            [DisplayName("Pakej")]
            public string s_package { get; set; }
        }
    }
}