using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class WorkzoneMetadata
    {
        public int IdWorkzone { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [StringLength(2, ErrorMessage = "Sigla deve conter no máximo 2 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Descrição deve ser preenchida")]
        [StringLength(2, ErrorMessage = "Descrição deve conter no máximo 400 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        [Display(Name = "Usuario Desativação")]
        public string UsuarioDesativacao { get; set; }

        public string DataDesativacao { get; set; }

        [Required(ErrorMessage = "Pessoas Necessárias deve ser preenchida")]
        [MaxLength(5, ErrorMessage = "Pessoas Necessárias deve conter no máximo 5 caracteres")]
        [Display(Name = "Pessoas Necessárias")]
        public int PessoasNecessarias { get; set; }

        public DateTime DataAlteracao { get; set; }

        [StringLength(20,ErrorMessage = "Usuário Alteração deve ter no máximo 20 caracteres")]
        [Display(Name = "Usuário Alteração")]
        public string UsuarioAlteracao { get; set; }

        [StringLength(400, ErrorMessage = "Dados Ultima Alteração deve ter no máximo 400 caracteres")]
        [Display(Name = "Dados Ultima Alteração")]
        public string DadosUltimaAlteracao { get; set; }

        [StringLength(400, ErrorMessage = "Motivo Última Alteração deve ter no máximo 400 caracteres")]
        [Display(Name = "Motivo Última Alteração")]
        public string MotivoUltimaAlteracao { get; set; }

        [Display(Name = "Ativo")]
        public bool FlagAtivo { get; set; }

        [Required(ErrorMessage = "BU deve ser preenchida")]
        public string idBU { get; set; }

        [Required(ErrorMessage = "CC deve ser preenchido")]
        public string idCC { get; set; }
    }
}