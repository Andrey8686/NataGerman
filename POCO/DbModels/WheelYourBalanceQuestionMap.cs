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
    internal partial class WheelYourBalanceQuestionMap : EntityTypeConfiguration<WheelYourBalanceQuestion>
    {
        public WheelYourBalanceQuestionMap(string schema = "dbo")
        {
            ToTable(schema + ".WheelYourBalanceQuestions");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Text).HasColumnName("Text").IsOptional().HasMaxLength(500);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
