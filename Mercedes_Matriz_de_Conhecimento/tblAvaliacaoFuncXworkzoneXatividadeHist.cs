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
    
    public partial class tblAvaliacaoFuncXworkzoneXatividadeHist
    {
        public int idFuncionario { get; set; }
        public int idWorkzoneAtividade { get; set; }
        public int idPerfilAtivItem { get; set; }
        public int idPrincipalIdent { get; set; }
        public bool FuncResponsavel { get; set; }
        public bool FuncAuxiliar { get; set; }
        public System.DateTime DataRegistro { get; set; }
        public Nullable<bool> AlocacaoForcada { get; set; }
        public string MotivoAlocacaoForcada { get; set; }
        public string UsuarioAlocacaoForcada { get; set; }
        public Nullable<bool> MotivoTransicao { get; set; }
    
        public virtual tblFuncionarios tblFuncionarios { get; set; }
        public virtual tblPerfilAtivItem tblPerfilAtivItem { get; set; }
        public virtual tblWorkzoneXAtividades tblWorkzoneXAtividades { get; set; }
    }
}
