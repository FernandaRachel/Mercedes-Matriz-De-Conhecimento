﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbConnection : DbContext
    {
        public DbConnection()
            : base("name=DbConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tblAtividades> tblAtividades { get; set; }
        public virtual DbSet<tblAtividadeXTreinamentos> tblAtividadeXTreinamentos { get; set; }
        public virtual DbSet<tblFuncionarios> tblFuncionarios { get; set; }
        public virtual DbSet<tblGrupoAtividades> tblGrupoAtividades { get; set; }
        public virtual DbSet<tblGrupoTreinamentos> tblGrupoTreinamentos { get; set; }
        public virtual DbSet<tblMatrizFuncXAtividades> tblMatrizFuncXAtividades { get; set; }
        public virtual DbSet<tblMatrizFuncXTreinamento> tblMatrizFuncXTreinamento { get; set; }
        public virtual DbSet<tblMatrizWorkzone> tblMatrizWorkzone { get; set; }
        public virtual DbSet<tblPerfilAtividadeXPerfilAtItem> tblPerfilAtividadeXPerfilAtItem { get; set; }
        public virtual DbSet<tblPerfilItens> tblPerfilItens { get; set; }
        public virtual DbSet<tblPerfilTreinamentoxPerfilItem> tblPerfilTreinamentoxPerfilItem { get; set; }
        public virtual DbSet<tblPerfis> tblPerfis { get; set; }
        public virtual DbSet<tblTipoTreinamento> tblTipoTreinamento { get; set; }
        public virtual DbSet<tblTreinamento> tblTreinamento { get; set; }
        public virtual DbSet<tblTreinamentoStatus> tblTreinamentoStatus { get; set; }
        public virtual DbSet<tblWorkzone> tblWorkzone { get; set; }
        public virtual DbSet<tblWorkzoneXAtividades> tblWorkzoneXAtividades { get; set; }
        public virtual DbSet<tblWorkzoneXFuncionario> tblWorkzoneXFuncionario { get; set; }
    }
}