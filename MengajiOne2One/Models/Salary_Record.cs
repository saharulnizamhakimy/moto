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
    
    public partial class Salary_Record
    {
        public int sal_ID { get; set; }
        public Nullable<double> sal_amount { get; set; }
        public System.DateTime sal_date { get; set; }
        public string sal_teacherID { get; set; }
        public string sal_month { get; set; }
        public string sal_status { get; set; }
    
        public virtual User_Record User_Record { get; set; }
    }
}
