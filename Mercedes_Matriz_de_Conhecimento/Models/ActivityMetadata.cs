using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ActivityMetadata
    {
        public int idAtividade { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [StringLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sigla deve ser preenchida")]
        [StringLength(2, ErrorMessage = "Sigla deve conter no máximo 2 caracteres")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "Descrição deve ser preenchida")]
        [StringLength(300, ErrorMessage = "Descrição deve conter no máximo 300 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string UsuarioCriacao { get; set; }

        public System.DateTime DataCriacao { get; set; }

        [Display(Name = "Grupo de Atividades")]
        public bool IndicaGrupoDeAtividades { get; set; }

        [Required(ErrorMessage = "Perfil de Atividade deve ser preenchido")]
        [Display(Name = "Perfil de Atividades")]
        public Nullable<int> idPerfilAtividade { get; set; }

        [Display(Name = "Tipo Equipamento GSA")]
        public Nullable<int> idTipoEquipamentoGSA { get; set; }
    }
}