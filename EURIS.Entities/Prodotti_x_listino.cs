//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EURIS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prodotti_x_listino
    {
        public int id { get; set; }
        public int id_prodotto { get; set; }
        public int id_listino { get; set; }
        public System.DateTime insert_date { get; set; }
        public Nullable<System.DateTime> valid_until { get; set; }
        public Nullable<System.DateTime> valid_from { get; set; }
        public string notes { get; set; }
    
        public virtual Listino Listino { get; set; }
        public virtual Prodotto Prodotto { get; set; }
    }
}
