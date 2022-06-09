using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MengajiOne2One.Models
{
    [MetadataType(typeof(PackageMetaDeta))]
    public partial class Package
    {
        public class PackageMetaDeta
        {
            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Nama Pakej")]
            public string pkg_id { get; set; }
            [Required(ErrorMessage = "Ruangan ini perlu diisi")]
            [DisplayName("Kadar Pakej (RM)")]
            [Range(1, 10000, ErrorMessage = "Kadar (RM) hendaklah sekurang kurangnya RM1.")]
            public Nullable<int> pkg_rate { get; set; }
            [DisplayName("Kadar Pakej Selepas Diskaun (RM)")]
            [Range(1, 10000, ErrorMessage = "Kadar (RM) hendaklah sekurang kurangnya RM1.")]
            public Nullable<int> pkg_discount { get; set; }
            [DisplayName("Jumlah Jam Minimum Untuk Diskaun")]
            [Range(1, 10000, ErrorMessage = "Jumlah jam hendaklah sekurang kurangnya 1.")]
            public Nullable<int> pkg_minhour { get; set; }
        }
    }
}