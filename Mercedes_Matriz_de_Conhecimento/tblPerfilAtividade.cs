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
    
    public partial class tblPerfilAtividade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPerfilAtividade()
        {
            this.tblAtividade = new HashSet<tblAtividade>();
            this.tblPerfilAtividadeXPerfilAtItem = new HashSet<tblPerfilAtividadeXPerfilAtItem>();
        }
    
        public int idPerfilAtividade { get; set; }
        public string nome { get; set; }
        public string UsuarioCriacao { get; set; }
        public System.DateTime DataCriacao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAtividade> tblAtividade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPerfilAtividadeXPerfilAtItem> tblPerfilAtividadeXPerfilAtItem { get; set; }
    }
}
