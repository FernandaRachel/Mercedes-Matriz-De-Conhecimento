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
    
    public partial class tblWorkzone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblWorkzone()
        {
            this.tblMatrizWorkzone = new HashSet<tblMatrizWorkzone>();
            this.tblWorkzoneXAtividades = new HashSet<tblWorkzoneXAtividades>();
            this.tblWorkzoneXFuncionario = new HashSet<tblWorkzoneXFuncionario>();
            this.tblMatrizWorkzoneTemp = new HashSet<tblMatrizWorkzoneTemp>();
        }
    
        public int IdWorkzone { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UsuarioCriacao { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public string UsuarioDesativacao { get; set; }
        public string DataDesativacao { get; set; }
        public int PessoasNecessarias { get; set; }
        public Nullable<System.DateTime> DataAlteracao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string DadosUltimaAlteracao { get; set; }
        public string MotivoUltimaAlteracao { get; set; }
        public bool FlagAtivo { get; set; }
        public string idBU { get; set; }
        public int idCC { get; set; }
        public int idLinha { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizWorkzone> tblMatrizWorkzone { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblWorkzoneXAtividades> tblWorkzoneXAtividades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblWorkzoneXFuncionario> tblWorkzoneXFuncionario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizWorkzoneTemp> tblMatrizWorkzoneTemp { get; set; }
    }
}
