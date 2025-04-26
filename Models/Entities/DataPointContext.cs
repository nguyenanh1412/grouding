﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WEB_SHOW_WRIST_STRAP.Models.Entities
{
    public partial class DataPointContext : DbContext
    {
        public DataPointContext()
        {
        }

        public DataPointContext(DbContextOptions<DataPointContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActProtocolType> ActProtocolTypes { get; set; } = null!;
        public virtual DbSet<ActUnitType> ActUnitTypes { get; set; } = null!;
        public virtual DbSet<DataNow> DataNows { get; set; } = null!;
        public virtual DbSet<DataNow2> DataNows2 { get; set; } = null!;
        public virtual DbSet<Dmalarm> Dmalarms { get; set; } = null!;
        public virtual DbSet<HisAlarmLed> HisAlarmLeds { get; set; } = null!;
        public virtual DbSet<HisBuzzeroff> HisBuzzeroffs { get; set; } = null!;
        public virtual DbSet<ListFloor> ListFloors { get; set; } = null!;
        public virtual DbSet<ListLine> ListLines { get; set; } = null!;
        public virtual DbSet<ListPlc> ListPlcs { get; set; } = null!;
        public virtual DbSet<ListPoint> ListPoints { get; set; } = null!;
        public virtual DbSet<ListPoint2> ListPoints2 { get; set; } = null!;
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<StatusPlc> StatusPlcs { get; set; } = null!;
        public virtual DbSet<TotalDatum> TotalData { get; set; } = null!;
        public virtual DbSet<TotalDatum2> TotalData2 { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DRIVERDELL-NHIE\\MAY1;Initial Catalog=DataPoint;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActProtocolType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ActProtocolType");

                entity.Property(e => e.ConnectionSystem).HasMaxLength(128);

                entity.Property(e => e.ProtocolType).HasMaxLength(128);

                entity.Property(e => e.Value).HasMaxLength(128);
            });

            modelBuilder.Entity<ActUnitType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ActUnitType");

                entity.Property(e => e.ConnectionSystem).HasMaxLength(512);

                entity.Property(e => e.ModuleType).HasMaxLength(128);

                entity.Property(e => e.Value).HasMaxLength(128);
            });

            modelBuilder.Entity<DataNow>(entity =>
            {
                entity.HasKey(e => new { e.IdPoint, e.IdLine });

                entity.ToTable("Data_Now");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.IdLine).HasColumnName("id_line");

                entity.Property(e => e.Alarm).HasColumnName("alarm");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.NamePoint)
                    .HasMaxLength(100)
                    .HasColumnName("name_point");

                entity.Property(e => e.TimeCheck)
                    .HasColumnType("datetime")
                    .HasColumnName("time_check");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");

                entity.Property(e => e.Value).HasColumnName("value");
            });
            modelBuilder.Entity<DataNow2>(entity =>
            {
                entity.HasKey(e => new { e.IdPoint, e.IdLine });

                entity.ToTable("Data_Now2");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.IdLine).HasColumnName("id_line");

                entity.Property(e => e.Alarm).HasColumnName("alarm");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.NamePoint)
                    .HasMaxLength(100)
                    .HasColumnName("name_point");

                entity.Property(e => e.TimeCheck)
                    .HasColumnType("datetime")
                    .HasColumnName("time_check");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");

                entity.Property(e => e.Value).HasColumnName("value");
            });
            modelBuilder.Entity<Dmalarm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dmalarm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ten)
                    .HasMaxLength(512)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<HisAlarmLed>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("His_alarm_Led");

                entity.Property(e => e.DeltaValue).HasColumnName("delta_value");

                entity.Property(e => e.DeltaValueN).HasColumnName("delta_value_n");

                entity.Property(e => e.Enstatus).HasColumnName("enstatus");

                entity.Property(e => e.EnstatusN).HasColumnName("enstatus_n");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MaxSpectN).HasColumnName("max_spect_n");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.MinSpectN).HasColumnName("min_spect_n");

                entity.Property(e => e.OffsetValue).HasColumnName("offset_value");

                entity.Property(e => e.OffsetValueN).HasColumnName("offset_value_n");

                entity.Property(e => e.TimeCheck)
                    .HasColumnType("datetime")
                    .HasColumnName("time_check");

                entity.Property(e => e.Timeoff).HasColumnName("timeoff");

                entity.Property(e => e.TimeoffN).HasColumnName("timeoff_n");
            });

            modelBuilder.Entity<HisBuzzeroff>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_His_Buzzeroff_1");

                entity.ToTable("His_Buzzeroff");

                entity.Property(e => e.Username)
                    .HasMaxLength(256)
                    .HasColumnName("username");

                entity.Property(e => e.Dateclick)
                    .HasColumnType("datetime")
                    .HasColumnName("dateclick");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");
            });

            modelBuilder.Entity<ListFloor>(entity =>
            {
                entity.HasKey(e => e.IdFloor);

                entity.ToTable("List_Floor");

                entity.Property(e => e.IdFloor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_floor");

                entity.Property(e => e.NameFloor)
                    .HasMaxLength(100)
                    .HasColumnName("name_floor");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");
            });

            modelBuilder.Entity<ListLine>(entity =>
            {
                entity.HasKey(e => e.IdLine)
                    .HasName("PK_List_Room");

                entity.ToTable("List_Line");

                entity.Property(e => e.IdLine)
                    .ValueGeneratedNever()
                    .HasColumnName("id_line");

                entity.Property(e => e.Cssheight).HasColumnName("cssheight");

                entity.Property(e => e.Cssleft).HasColumnName("cssleft");

                entity.Property(e => e.Csstop).HasColumnName("csstop");

                entity.Property(e => e.Csswidth).HasColumnName("csswidth");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.IdIviLine)
                    .HasMaxLength(10)
                    .HasColumnName("id_ivi_line");

                entity.Property(e => e.ListUser)
                    .HasMaxLength(512)
                    .HasColumnName("list_user");

                entity.Property(e => e.NameLine)
                    .HasMaxLength(512)
                    .HasColumnName("name_line");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.TotalPointAlarm).HasColumnName("total_point_alarm");
            });

            modelBuilder.Entity<ListPlc>(entity =>
            {
                entity.HasKey(e => e.IpPlc);

                entity.ToTable("List_Plc");

                entity.Property(e => e.IpPlc)
                    .HasMaxLength(50)
                    .HasColumnName("ip_plc");

                entity.Property(e => e.ActPassword).HasMaxLength(128);

                entity.Property(e => e.ActProtocolType).HasMaxLength(128);

                entity.Property(e => e.ActUnitType).HasMaxLength(128);

                entity.Property(e => e.AddRead).HasMaxLength(128);

                entity.Property(e => e.AddReadV)
                    .HasMaxLength(128)
                    .HasColumnName("AddRead_V");

                entity.Property(e => e.IdLine).HasColumnName("Id_line");

                entity.Property(e => e.IdPlc).HasColumnName("id_plc");

                entity.Property(e => e.NumberReads)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NumberReadsV)
                    .HasMaxLength(10)
                    .HasColumnName("NumberReads_V")
                    .IsFixedLength();

                entity.Property(e => e.StatusUse).HasColumnName("status_use");
            });

            modelBuilder.Entity<ListPoint>(entity =>
            {
                entity.HasKey(e => new { e.IdPoint, e.IdLine })
                    .HasName("PK_List_Led");

                entity.ToTable("List_Point");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.IdLine).HasColumnName("id_line");

                entity.Property(e => e.Addsread)
                    .HasMaxLength(50)
                    .HasColumnName("addsread");

                entity.Property(e => e.Addswrite)
                    .HasMaxLength(50)
                    .HasColumnName("addswrite");

                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.Cssleft).HasColumnName("cssleft");

                entity.Property(e => e.Csstop).HasColumnName("csstop");
                entity.Property(e => e.Width).HasColumnName("width");

                entity.Property(e => e.Height).HasColumnName("height");
                entity.Property(e => e.TopDetail).HasColumnName("topdetail");

                entity.Property(e => e.LeftDetail).HasColumnName("leftdetail");

                entity.Property(e => e.DeltaValue).HasColumnName("delta_value");

                entity.Property(e => e.Enstatus).HasColumnName("enstatus");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.NamePoint)
                    .HasMaxLength(100)
                    .HasColumnName("name_point");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.OffsetValue).HasColumnName("offset_value");

                entity.Property(e => e.Plc).HasColumnName("plc");

                entity.Property(e => e.TimeChange)
                    .HasColumnType("datetime")
                    .HasColumnName("time_change");

                entity.Property(e => e.Timeoff).HasColumnName("timeoff");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");

                entity.Property(e => e.UserChange)
                    .HasMaxLength(100)
                    .HasColumnName("user_change")
                    .IsFixedLength();
            });
            modelBuilder.Entity<ListPoint2>(entity =>
            {
                entity.HasKey(e => new { e.IdPoint, e.IdLine })
                    .HasName("PK_List_Led2");

                entity.ToTable("List_Point2");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.IdLine).HasColumnName("id_line");

                entity.Property(e => e.Addsread)
                    .HasMaxLength(50)
                    .HasColumnName("addsread");

                entity.Property(e => e.Addswrite)
                    .HasMaxLength(50)
                    .HasColumnName("addswrite");

                entity.Property(e => e.Change).HasColumnName("change");

                entity.Property(e => e.Cssleft).HasColumnName("cssleft");

                entity.Property(e => e.Csstop).HasColumnName("csstop");

                entity.Property(e => e.DeltaValue).HasColumnName("delta_value");

                entity.Property(e => e.Enstatus).HasColumnName("enstatus");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.NamePoint)
                    .HasMaxLength(100)
                    .HasColumnName("name_point");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.OffsetValue).HasColumnName("offset_value");

                entity.Property(e => e.Plc).HasColumnName("plc");

                entity.Property(e => e.TimeChange)
                    .HasColumnType("datetime")
                    .HasColumnName("time_change");

                entity.Property(e => e.Timeoff).HasColumnName("timeoff");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");

                entity.Property(e => e.UserChange)
                    .HasMaxLength(100)
                    .HasColumnName("user_change")
                    .IsFixedLength();
            });
            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("options");

                entity.Property(e => e.Cname)
                    .HasMaxLength(50)
                    .HasColumnName("cname");

                entity.Property(e => e.ValueOtp)
                    .HasMaxLength(50)
                    .HasColumnName("value_otp");
                entity.Property(e => e.DelayTime).HasColumnName("delay_time");
            });

            modelBuilder.Entity<StatusPlc>(entity =>
            {
                entity.HasKey(e => e.IdPlc);

                entity.ToTable("Status_plc");

                entity.Property(e => e.IdPlc)
                    .ValueGeneratedNever()
                    .HasColumnName("id_plc");

                entity.Property(e => e.Alarm).HasColumnName("alarm");

                entity.Property(e => e.NamePlc)
                    .HasMaxLength(50)
                    .HasColumnName("name_plc");

                entity.Property(e => e.Sl).HasColumnName("sl");

                entity.Property(e => e.StatusNow).HasColumnName("status_now");
            });

            modelBuilder.Entity<TotalDatum>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK_DATASLEDDB");

                entity.Property(e => e.IdLog).HasColumnName("id_log");

                entity.Property(e => e.Alarm)
                    .HasMaxLength(10)
                    .HasColumnName("alarm");

                entity.Property(e => e.IdLine).HasColumnName("id_line");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeCheck)
                    .HasColumnType("datetime")
                    .HasColumnName("time_check");

                entity.Property(e => e.TimeStop)
                    .HasColumnType("datetime")
                    .HasColumnName("time_stop");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Value).HasColumnName("value");
                entity.Property(e => e.Status).HasColumnName("status");
            });
            modelBuilder.Entity<TotalDatum2>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK_DATASLEDDB2");

                entity.Property(e => e.IdLog).HasColumnName("id_log");

                entity.Property(e => e.Alarm)
                    .HasMaxLength(10)
                    .HasColumnName("alarm");

                entity.Property(e => e.IdLine).HasColumnName("id_line");

                entity.Property(e => e.IdPoint).HasColumnName("id_point");

                entity.Property(e => e.MaxSpect).HasColumnName("max_spect");

                entity.Property(e => e.MinSpect).HasColumnName("min_spect");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeCheck)
                    .HasColumnType("datetime")
                    .HasColumnName("time_check");

                entity.Property(e => e.TimeStop)
                    .HasColumnType("datetime")
                    .HasColumnName("time_stop");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Value).HasColumnName("value");
                entity.Property(e => e.Status).HasColumnName("status");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
