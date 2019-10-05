// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.61

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace POCO.DbModels
{
    internal partial class WheelYourBalanceUserMap : EntityTypeConfiguration<WheelYourBalanceUser>
    {
        public WheelYourBalanceUserMap(string schema = "dbo")
        {
            ToTable(schema + ".WheelYourBalanceUsers");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(50);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(50);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
