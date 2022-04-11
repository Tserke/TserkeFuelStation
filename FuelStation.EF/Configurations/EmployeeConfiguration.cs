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
    public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(employee => employee.Id);
            builder.Property(employee => employee.Id).ValueGeneratedOnAdd();
            builder.Property(employee => employee.Name).HasMaxLength(maxLength: 100);
            builder.Property(employee =>employee.Surname).HasMaxLength(maxLength: 100);
            builder.Property(employee => employee.SallaryPerMonth).HasColumnType("decimal(10,2)");
            //TODO: set foreign keys
        }
    }
}
