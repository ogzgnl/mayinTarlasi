﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mayinTarlasi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MineSweeper : DbContext
    {
        public MineSweeper()
            : base("name=MineSweeper")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PersonelMessage> PersonelMessages { get; set; }
        public virtual DbSet<PublicMessenger> PublicMessengers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TBLgameData> TBLgameDatas { get; set; }
        public virtual DbSet<TBLuserData> TBLuserDatas { get; set; }
    }
}
