using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Service.Models
{
    public partial class RoosterLotteryContext : DbContext
    {
        public RoosterLotteryContext()
        {
        }

        public RoosterLotteryContext(DbContextOptions<RoosterLotteryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bet> Bets { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<PlayerBet> PlayerBets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.ToTable("BET");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DrawTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("PLAYER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            modelBuilder.Entity<PlayerBet>(entity =>
            {
                entity.ToTable("PLAYER_BET");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BetId).HasColumnName("BET_ID");

                entity.Property(e => e.IsWinner)
                    .HasColumnName("isWinner")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PlayerId).HasColumnName("PLAYER_ID");

                entity.HasOne(d => d.Bet)
                    .WithMany(p => p.PlayerBets)
                    .HasForeignKey(d => d.BetId)
                    .HasConstraintName("FK__PLAYER_BE__BET_I__173876EA");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerBets)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK__PLAYER_BE__PLAYE__182C9B23");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
