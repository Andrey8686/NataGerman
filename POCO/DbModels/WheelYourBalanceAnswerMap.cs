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
    internal partial class WheelYourBalanceAnswerMap : EntityTypeConfiguration<WheelYourBalanceAnswer>
    {
        public WheelYourBalanceAnswerMap(string schema = "dbo")
        {
            ToTable(schema + ".WheelYourBalanceAnswers");
            HasKey(x => new { x.UserId, x.TypeId, x.QuestionId });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.TypeId).HasColumnName("TypeId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.QuestionId).HasColumnName("QuestionId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Mark).HasColumnName("Mark").IsRequired();

            HasRequired(a => a.WheelYourBalanceUser).WithMany(b => b.WheelYourBalanceAnswers).HasForeignKey(c => c.UserId);
            HasRequired(a => a.WheelYourBalanceType).WithMany(b => b.WheelYourBalanceAnswers).HasForeignKey(c => c.TypeId);
            HasRequired(a => a.WheelYourBalanceQuestion).WithMany(b => b.WheelYourBalanceAnswers).HasForeignKey(c => c.QuestionId);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
