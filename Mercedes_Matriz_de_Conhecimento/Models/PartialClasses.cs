using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class PartialClasses
    {

        [MetadataType(typeof(WorkzoneMetadata))]
        public partial class tblWorkzone
        {
        }

        [MetadataType(typeof(EmployeeMetadata))]
        public partial class tblFuncionarios
        {
        }
    }
}