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
    
    public partial class tblAtividadeXTreinamentos
    {
        public int idAtividade { get; set; }
        public int idTreinamento { get; set; }
        public int idAtivTreinamento { get; set; }
    
        public virtual tblAtividades tblAtividades { get; set; }
        public virtual tblTreinamento tblTreinamento { get; set; }
    }
}
