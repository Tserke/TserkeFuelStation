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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(transaction => transaction.Id);
            builder.Property(transaction => transaction.Id).ValueGeneratedOnAdd();
            //TODO: Set foreign keys

            builder.HasOne(transaction => transaction.Customer).WithOne(customer => customer.Transaction).HasForeignKey<Transaction>(transaction => transaction.CustomerId);
            builder.HasOne(transaction => transaction.Employee).WithOne(employee => employee.Transaction).HasForeignKey<Transaction>(transaction => transaction.EmployeeId);

        }
    }
}
