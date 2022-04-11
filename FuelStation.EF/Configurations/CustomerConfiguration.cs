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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(customer => customer.Id);
            builder.Property(customer => customer.Id).ValueGeneratedOnAdd();
            builder.Property(customer => customer.Name).HasMaxLength(maxLength:100).IsRequired();
            builder.Property(customer => customer.Surname).HasMaxLength(maxLength: 100).IsRequired();
            //TODO: make CardNumber to be unique

            //TODO: I need to set foreign keys if needed
        }
    }
}
