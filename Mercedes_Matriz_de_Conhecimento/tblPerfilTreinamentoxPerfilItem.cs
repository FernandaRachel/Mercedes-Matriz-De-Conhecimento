//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mercedes_Matriz_de_Conhecimento
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPerfilTreinamentoxPerfilItem
    {
        public int IdPerfilTreinamento { get; set; }
        public int IdPerfilItem { get; set; }
        public Nullable<int> Ordem { get; set; }
    
        public virtual tblPerfilItem tblPerfilItem { get; set; }
        public virtual tblPerfilTreinamento tblPerfilTreinamento { get; set; }
    }
}
