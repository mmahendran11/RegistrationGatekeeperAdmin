﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegistrationGatekeeperAdmin.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MHCC_RegistrationEntities : DbContext
    {
        public MHCC_RegistrationEntities()
            : base("name=MHCC_RegistrationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CernerPosition> CernerPositions { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<GateKeeperStatu> GateKeeperStatus { get; set; }
        public virtual DbSet<NursingAncillaryCredential> NursingAncillaryCredentials { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<SystemAccess> SystemAccesses { get; set; }
        public virtual DbSet<SystemIdentity> SystemIdentitys { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<LogGatekeeper> LogGatekeepers { get; set; }
        public virtual DbSet<LogUserInformation> LogUserInformations { get; set; }
        public virtual DbSet<LogUpdateType> LogUpdateTypes { get; set; }
    }
}