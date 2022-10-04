using DoorPrize.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoorPrize.Infrastructure.Data.Config
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<ParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<ParticipantEntity> builder)
        {
            builder.ToTable("Participant");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(x => x.CPF)
                .HasColumnName("CPF")
                .IsRequired();

            builder.Property(x => x.BirthDate)
                .HasColumnName("BirthDate")
                .IsRequired();

            builder.Property(x => x.Income)
                .HasColumnName("Income")
                .IsRequired();

            builder.Property(x => x.Quota)
                .HasColumnName("Quota")
                .IsRequired();

            builder.Property(x => x.CID)
                .HasColumnName("CID");
        }
    }
}