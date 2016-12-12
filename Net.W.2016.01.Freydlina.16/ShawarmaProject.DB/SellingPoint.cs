//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShawarmaProject.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class SellingPoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SellingPoint()
        {
            this.PriceControllers = new HashSet<PriceController>();
            this.Sellers = new HashSet<Seller>();
        }
    
        public int SellingPointId { get; set; }
        public string Address { get; set; }
        public int SellingPointCategory { get; set; }
        public string ShawarmaTitle { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceController> PriceControllers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seller> Sellers { get; set; }
        public virtual SellingPointCategory SellingPointCategory1 { get; set; }
    }
}