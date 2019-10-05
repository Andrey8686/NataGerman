using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class WheelYourBalanceViewModel
    {
        public List<SphereTypeViewModel> Spheres { get; set; }
    }
    

    public class SphereTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<SphereQuestionViewModel> Questions { get; set; }
    }



    public class SphereQuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Mark { get; set; }
    }



    public class WheelYourBalanceSendMailModel
    {
        [Display(Name = "Ваше имя")]
        [Required(ErrorMessage = "Заполните пожалуйста поле '{0}'")]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Заполните пожалуйста поле '{0}'")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Не корректно заполнено поле '{0}'")]
        public string Email { get; set; }
    }



    public class ExportToExcelModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}