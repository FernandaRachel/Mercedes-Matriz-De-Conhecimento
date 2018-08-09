﻿using Mercedes_Matriz_de_Conhecimento.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento
{

    [MetadataType(typeof(WorkzoneMetadata))]
    public partial class tblWorkzone
    {
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class tblFuncionarios
    {
    }

    [MetadataType(typeof(TrainingMetadata))]
    public partial class tblTreinamento
    {
    }

    [MetadataType(typeof(ProfilesMetadata))]
    public partial class tblPerfis
    {
    }

    [MetadataType(typeof(TrainingTypeMetada))]
    public partial class tblTipoTreinamento
    {
    }

    [MetadataType(typeof(ActivityMetadata))]
    public partial class tblAtividades
    {
    }

    [MetadataType(typeof(PerfilAtividadeXPerfilAtItemMetadata))]
    public partial class tblPerfilAtividadeXPerfilAtItem
    {
    }

    [MetadataType(typeof(ActivityProfileMetada))]
    public partial class tblPerfilAtividade
    {
    }

    [MetadataType(typeof(ActivityProfileItemMetadata))]
    public partial class tblPerfilAtivItem
    {
    }
    [MetadataType(typeof(ActivityItemMetadata))]
    public partial class tblPerfilItem
    {
    }
    
}