//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MengajiOne2One.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student_Record
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student_Record()
        {
            this.Class_Record = new HashSet<Class_Record>();
            this.Student_Performance_Record = new HashSet<Student_Performance_Record>();
        }
    
        public string s_id { get; set; }
        public string s_pwd { get; set; }
        public string s_name { get; set; }
        public Nullable<int> s_age { get; set; }
        public string s_address { get; set; }
        public string s_contactNo { get; set; }
        public System.DateTime s_regDate { get; set; }
        public string s_teacherID { get; set; }
        public string s_package { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class_Record> Class_Record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Performance_Record> Student_Performance_Record { get; set; }
        public virtual User_Record User_Record { get; set; }
    }
}
