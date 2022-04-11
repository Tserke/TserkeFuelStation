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
    public class TransactionLineConfiguration : IEntityTypeConfiguration<TransactionLine>
    {
        public void Configure(EntityTypeBuilder<TransactionLine> builder)
        {
            builder.ToTable("TransactionLine");
            builder.HasKey(transactionLine => transactionLine.Id);
            builder.Property(transactionLine => transactionLine.Id).ValueGeneratedOnAdd();
            builder.Property(transactionLine => transactionLine.Quantity).IsRequired();
            builder.Property(transactionLine => transactionLine.ItemPrice).HasColumnType("Decimal(10,2)");
            builder.Property(transactionLine => transactionLine.NetValue).HasColumnType("Decimal(10,2)");
            builder.Property(transactionLine => transactionLine.DiscountValue).HasColumnType("Decimal(10,2)");
            builder.Property(transactionLine => transactionLine.TotalValue).HasColumnType("Decimal(10,2)");

            //TODO:set foreign keys
            builder.HasOne(transactionLine => transactionLine.Transaction).WithMany(transaction => transaction.TransactionLines).HasForeignKey(transactionLine => transactionLine.TransactionId);
            builder.HasOne(transactionLine => transactionLine.Item).WithOne(item => item.TransactionLine).HasForeignKey<TransactionLine>(transactionLine => transactionLine.ItemId);
        }
    }
}
