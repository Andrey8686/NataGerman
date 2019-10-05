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
    public partial class DataContext : DbContext, IDataContext
    {
        public IDbSet<sysdiagram> sysdiagrams { get; set; }
        public IDbSet<WheelYourBalanceAnswer> WheelYourBalanceAnswers { get; set; }
        public IDbSet<WheelYourBalanceQuestion> WheelYourBalanceQuestions { get; set; }
        public IDbSet<WheelYourBalanceType> WheelYourBalanceTypes { get; set; }
        public IDbSet<WheelYourBalanceUser> WheelYourBalanceUsers { get; set; }

        static DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext()
            : base("Name=DefaultConnection")
        {
            InitializePartial();
        }

        public DataContext(string connectionString) : base(connectionString)
        {
            InitializePartial();
        }

        public DataContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
            InitializePartial();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new WheelYourBalanceAnswerMap());
            modelBuilder.Configurations.Add(new WheelYourBalanceQuestionMap());
            modelBuilder.Configurations.Add(new WheelYourBalanceTypeMap());
            modelBuilder.Configurations.Add(new WheelYourBalanceUserMap());

            OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new sysdiagramMap(schema));
            modelBuilder.Configurations.Add(new WheelYourBalanceAnswerMap(schema));
            modelBuilder.Configurations.Add(new WheelYourBalanceQuestionMap(schema));
            modelBuilder.Configurations.Add(new WheelYourBalanceTypeMap(schema));
            modelBuilder.Configurations.Add(new WheelYourBalanceUserMap(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
        
        // Stored Procedures
    }
}
