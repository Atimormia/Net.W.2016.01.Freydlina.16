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
    
    public partial class Ingradient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingradient()
        {
            this.ShawarmaRecipes = new HashSet<ShawarmaRecipe>();
        }
    
        public int IngradientId { get; set; }
        public string IngradientName { get; set; }
        public int TotalWeight { get; set; }
        public Nullable<int> CategoryId { get; set; }
    
        public virtual IngradientCategory IngradientCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShawarmaRecipe> ShawarmaRecipes { get; set; }
    }
}
