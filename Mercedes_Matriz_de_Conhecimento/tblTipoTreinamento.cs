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
    
    public partial class tblTipoTreinamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTipoTreinamento()
        {
            this.tblTreinamento = new HashSet<tblTreinamento>();
        }
    
        public int IdTipoTreinamento { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public string UsuarioCriacao { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public string UsuarioDesativacao { get; set; }
        public Nullable<System.DateTime> DataDesativacao { get; set; }
        public bool TipoAtivo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTreinamento> tblTreinamento { get; set; }
    }
}
