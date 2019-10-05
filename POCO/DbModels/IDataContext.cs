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
    public interface IDataContext : IDisposable
    {
        IDbSet<sysdiagram> sysdiagrams { get; set; }
        IDbSet<WheelYourBalanceAnswer> WheelYourBalanceAnswers { get; set; }
        IDbSet<WheelYourBalanceQuestion> WheelYourBalanceQuestions { get; set; }
        IDbSet<WheelYourBalanceType> WheelYourBalanceTypes { get; set; }
        IDbSet<WheelYourBalanceUser> WheelYourBalanceUsers { get; set; }

        int SaveChanges();
        
        // Stored Procedures
    }

}
