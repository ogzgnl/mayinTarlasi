//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mayinTarlasi
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonelMessage
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual TBLuserData TBLuserData { get; set; }
        public virtual TBLuserData TBLuserData1 { get; set; }
    }
}
