﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockScreenerLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BhavCopiesDbEntities : DbContext
    {
        public BhavCopiesDbEntities()
            : base("name=BhavCopiesDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BhavCopy> BhavCopies { get; set; }
        public virtual DbSet<BhavCopyUploadLog> BhavCopyUploadLogs { get; set; }
        public virtual DbSet<Housebreak> Housebreaks { get; set; }
        public virtual DbSet<Ticker> Tickers { get; set; }
    }
}