using FuelStation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelStation.EF.Configurations
{
    public class ItemConfiguration: IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");
            builder.HasKey(item => item.Id);
            builder.Property(item => item.Id).ValueGeneratedOnAdd();
            builder.HasAlternateKey(item => item.Code);
            builder.Property(item => item.Description).HasMaxLength(maxLength: 200);
            //TODO: Maybe ItemType needed
            builder.Property(item => item.Price).HasColumnType("decimal(10,2)");
            builder.Property(item => item.Cost).HasColumnType("decimal(10,2)");

        }
    }
}
