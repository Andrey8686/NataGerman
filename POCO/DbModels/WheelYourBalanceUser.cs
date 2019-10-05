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
    public partial class WheelYourBalanceUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<WheelYourBalanceAnswer> WheelYourBalanceAnswers { get; set; }

        public WheelYourBalanceUser()
        {
            Id = System.Guid.NewGuid();
            Name = "NULL";
            Email = "NULL";
            CreatedDate = System.DateTime.Now;
            WheelYourBalanceAnswers = new List<WheelYourBalanceAnswer>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
