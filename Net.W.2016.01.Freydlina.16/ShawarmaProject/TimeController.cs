//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShawarmaProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeController
    {
        public int TimeControllerId { get; set; }
        public int SellerId { get; set; }
        public System.DateTime WorkStart { get; set; }
        public System.DateTime WorkEnd { get; set; }
    
        public virtual Seller Seller { get; set; }
    }
}
