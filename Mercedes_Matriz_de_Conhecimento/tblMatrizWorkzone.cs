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
    
    public partial class tblMatrizWorkzone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMatrizWorkzone()
        {
            this.tblMatrizFuncXAtividades = new HashSet<tblMatrizFuncXAtividades>();
            this.tblMatrizFuncXTreinamento = new HashSet<tblMatrizFuncXTreinamento>();
        }
    
        public int idMatrizWZ { get; set; }
        public int idWorkzone { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public string Usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizFuncXAtividades> tblMatrizFuncXAtividades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizFuncXTreinamento> tblMatrizFuncXTreinamento { get; set; }
        public virtual tblWorkzone tblWorkzone { get; set; }
    }
}