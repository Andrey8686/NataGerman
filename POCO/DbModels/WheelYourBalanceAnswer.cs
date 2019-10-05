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
    public partial class WheelYourBalanceAnswer
    {
        public Guid UserId { get; set; }
        public int TypeId { get; set; }
        public int QuestionId { get; set; }
        public int Mark { get; set; }

        public virtual WheelYourBalanceQuestion WheelYourBalanceQuestion { get; set; }
        public virtual WheelYourBalanceType WheelYourBalanceType { get; set; }
        public virtual WheelYourBalanceUser WheelYourBalanceUser { get; set; }

        public WheelYourBalanceAnswer()
        {
            Mark = 0;
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
