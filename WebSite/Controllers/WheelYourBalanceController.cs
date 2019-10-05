using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OfficeOpenXml;
using POCO.DbModels;
using Svg;
using WebGrease.Activities;
using WebSite.Extensions;
using WebSite.Models;
using WebSite.Utils;

namespace WebSite.Controllers
{
    public class WheelYourBalanceController : BaseController
    {
        public ActionResult Index()
        {

            WheelYourBalanceViewModel model;

            if (Session["WheelYourBalance"] != null)
            {
                model = (WheelYourBalanceViewModel)Session["WheelYourBalance"];
            }
            else
            {
                model = InitWheelYourBalance();
            }
            
            return View(model);
        }


        private WheelYourBalanceViewModel InitWheelYourBalance()
        {
            var model = new WheelYourBalanceViewModel
            {
                Spheres = db.WheelYourBalanceTypes.AsNoTracking().Select(x => new SphereTypeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color,
                    Questions = db.WheelYourBalanceQuestions.Select(q => new SphereQuestionViewModel
                    {
                        Id = q.Id,
                        Text = q.Text,
                        Mark = null
                    }).ToList()
                }).ToList()
            };

            Session["WheelYourBalance"] = model;

            return model;
        }



        public ActionResult Load(string id)
        {
            if (!Guid.TryParse(id, out var rId)) return HttpNotFound();

            ViewBag.LinkToWheel = $"{Request.Url.Scheme}://{Request.Url.Host}/Content/WheelYourBalance/{rId}.png";


            var model = new WheelYourBalanceViewModel
            {
                Spheres = db.WheelYourBalanceTypes.AsNoTracking().Select(x => new SphereTypeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color,
                    Questions = db.WheelYourBalanceQuestions.Select(q => new SphereQuestionViewModel
                    {
                        Id = q.Id,
                        Text = q.Text,
                        Mark = q.WheelYourBalanceAnswers.FirstOrDefault(a => a.UserId == rId && a.TypeId== x.Id && a.QuestionId == q.Id).Mark
                    }).ToList()
                }).ToList()
            };

            Session["WheelYourBalance"] = model;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetMark(SphereQuestionViewModel question)
        {
            var model = (WheelYourBalanceViewModel)(Session["WheelYourBalance"] ?? new WheelYourBalanceViewModel());

            if (model.Spheres.Any(a => a.Questions.Any(b => b.Mark == null)))
            {
                foreach (var s in model.Spheres)
                {
                    foreach (var q in s.Questions)
                    {
                        if (q.Mark == null)
                        {
                            q.Mark = question.Mark;
                            goto exit;
                        }
                    }
                }

                exit:

                Session["WheelYourBalance"] = model;
            }
            
            return RedirectToAction("Index");
        }



        public ActionResult GoBack()
        {
            try
            {
                if (Session["WheelYourBalance"] != null)
                {
                    var model = (WheelYourBalanceViewModel)Session["WheelYourBalance"];
                    
                    model.Spheres.LastOrDefault(x => x.Questions.Any(a => a.Mark != null)).Questions.LastOrDefault(x => x.Mark != null).Mark = null;

                    Session["WheelYourBalance"] = model;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return RedirectToAction("Index");
        }



        public ActionResult ResetMark()
        {
            Session["WheelYourBalance"] = null;
            Session["WheelYourBalanceId"] = null;

            return RedirectToAction("Index");
        }

        

        public ActionResult GetWheelYourBalance()
        {
            if (Session["WheelYourBalance"] is null) return HttpNotFound();

            try
            {
                var model = (WheelYourBalanceViewModel) Session["WheelYourBalance"];
                
                var svg = SvgDocument.Open(Server.MapPath("~/Content/Images/WheelYourBalance.svg"));

                var newWidth = 424;
                var newHeight = (int)(newWidth / svg.Width * svg.Height);

                using (var svgBmp = svg.Draw())
                {
                    using (var bmp = new Bitmap(newWidth * 5, newHeight * 5))
                    {
                        bmp.MakeTransparent(Color.Black);

                        for (var i = 0; i < model.Spheres.Count; i++)
                        {
                            bmp.SetPiePiece(i + 1, (double)model.Spheres.ToList()[i].Questions.Sum(x => x.Mark ?? 0) / model.Spheres.ToList()[i].Questions.Count);
                        }

                        using (var bmpDes = new Bitmap(svgBmp.Width, svgBmp.Height))
                        {
                            using (var gDes = Graphics.FromImage(bmpDes))
                            {
                                gDes.DrawImage(bmp, bmpDes.Width / 2 - newWidth / 2, bmpDes.Height / 2 - newHeight / 2, newWidth, newHeight);
                                gDes.DrawImage(svgBmp, 0, 0, svgBmp.Width, svgBmp.Height);
                            }

                            using (var ms = new MemoryStream())
                            {
                                bmpDes.Save(ms, ImageFormat.Png);

                                return File(ms.ToArray(), "image/png");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return HttpNotFound();
        }


        public bool SaveWheel(Guid id)
        {
            try
            {
                var model = (WheelYourBalanceViewModel) Session["WheelYourBalance"];
                
                var svg = SvgDocument.Open(Server.MapPath("~/Content/Images/WheelYourBalance.svg"));

                const int newWidth = 424;
                var newHeight = (int)(newWidth / svg.Width * svg.Height);

                using (var svgBmp = svg.Draw())
                {
                    using (var bmp = new Bitmap(newWidth * 5, newHeight * 5))
                    {
                        bmp.MakeTransparent(Color.Black);

                        for (var i = 0; i < model.Spheres.Count; i++)
                        {
                            bmp.SetPiePiece(i + 1, (double)model.Spheres.ToList()[i].Questions.Sum(x => x.Mark ?? 0) / model.Spheres.ToList()[i].Questions.Count);
                        }

                        using (var bmpDes = new Bitmap(svgBmp.Width, svgBmp.Height))
                        {
                            using (var gDes = Graphics.FromImage(bmpDes))
                            {
                                gDes.DrawImage(bmp, bmpDes.Width / 2 - newWidth / 2, bmpDes.Height / 2 - newHeight / 2, newWidth, newHeight);
                                gDes.DrawImage(svgBmp, 0, 0, svgBmp.Width, svgBmp.Height);
                            }

                            bmpDes.Save(Path.Combine(Server.MapPath("~/Content/WheelYourBalance"), $"{id}.png"), ImageFormat.Png);
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
            }
            
            return false;
        }


        public ActionResult SendMail()
        {
            return PartialView(new WheelYourBalanceSendMailModel());
        }



        public ActionResult SendMailSuccess(WheelYourBalanceSendMailModel model)
        {
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMail(WheelYourBalanceSendMailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = Save(model.Name, model.Email);

                    if (id != Guid.Empty)
                    {
                        if (SaveWheel(id))
                        {
                            var template = System.IO.File.ReadAllText(Path.Combine(Server.MapPath("~/Content/EmailTemplates/WheelYourBalance"), "Index.html"));

                            template = template.Replace("{Name}", model.Name);
                            template = template.Replace("{ImagePath}", $"http://natagerman.ru/Content/WheelYourBalance/{id}.png");


                            var wheel = (WheelYourBalanceViewModel)Session["WheelYourBalance"];

                            var wData = string.Empty;
                            for (var i = 0; i < wheel.Spheres.Count; i++)
                            {
                                wData += $"<tr>";
                                    wData += $"<td style='color: {wheel.Spheres.ToList()[i].Color};'>";
                                        wData += $"{wheel.Spheres.ToList()[i].Name}:&nbsp;";
                                    wData += "</td>";
                                    wData += $"<td style='color: {wheel.Spheres.ToList()[i].Color};'>";
                                        wData += $"{(double)wheel.Spheres.ToList()[i].Questions.Sum(x => x.Mark) / wheel.Spheres.ToList()[i].Questions.Count}";
                                    wData += "</td>";
                                wData += "</tr>";
                            }
                            template = template.Replace("{wData}", wData);


                            await MessageUtil.SendEmailAsync(model.Email, "Благодарим за прохождение теста! [результат внутри]", "Ваше колесо баланса", template);
                            
                            Session["WheelYourBalance"] = null;

                            return RedirectToAction("SendMailSuccess", model);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Что то пошло не так!");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return View(model);
        }



        private Guid Save(string name, string email)
        {
            try
            {
                var m = (WheelYourBalanceViewModel)(Session["WheelYourBalance"] ?? new WheelYourBalanceViewModel());

                if (m != null)
                {
                    var modelUser = new WheelYourBalanceUser
                    {
                        Name = name,
                        Email = email
                    };

                    db.WheelYourBalanceUsers.Add(modelUser);

                    db.SaveChanges();
                    
                    foreach (var mSphere in m.Spheres)
                    {
                        foreach (var mQuestions in mSphere.Questions)
                        {
                            if (mQuestions.Mark == null) mQuestions.Mark = 0;

                            db.WheelYourBalanceAnswers.Add(new WheelYourBalanceAnswer
                            {
                                UserId = modelUser.Id,
                                TypeId = mSphere.Id,
                                QuestionId = mQuestions.Id,
                                Mark = mQuestions.Mark.Value
                            });
                        }
                    }

                    db.SaveChanges();

                    return modelUser.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Guid.Empty;
        }


        public ActionResult ExportToExcel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportToExcel(ExportToExcelModel model)
        {
            try
            {
                if (!DateTime.TryParse(model.StartDate, out var resultStartDate))
                    ModelState.AddModelError("", @"Что то не так с датой 'С'");

                if (!DateTime.TryParse(model.EndDate, out var resultEndDate))
                    ModelState.AddModelError("", @"Что то не так с датой 'ПО'");



                if (ModelState.IsValid)
                {
                    resultEndDate = resultEndDate.AddDays(1);

                    var excel = new ExcelPackage();

                    var workSheet = excel.Workbook.Worksheets.Add("contact-import");


                    workSheet.Cells[1, 1].Border(Color.Black).Bold().Value = "Фамилия";
                    workSheet.Cells[1, 2].Border(Color.Black).Bold().Value = "Имя";
                    workSheet.Cells[1, 3].Border(Color.Black).Bold().Value = "Отчество";
                    workSheet.Cells[1, 4].Border(Color.Black).Bold().Value = "Email";
                    workSheet.Cells[1, 5].Border(Color.Black).Bold().Value = "Телефон";
                    workSheet.Cells[1, 6].Border(Color.Black).Bold().Value = "Скайп";
                    workSheet.Cells[1, 7].Border(Color.Black).Bold().Value = "Страна";
                    workSheet.Cells[1, 8].Border(Color.Black).Bold().Value = "Город";
                    workSheet.Cells[1, 9].Border(Color.Black).Bold().Value = "Дата подписки";
                    workSheet.Cells[1, 10].Border(Color.Black).Bold().Value = "Поле 1";
                    workSheet.Cells[1, 11].Border(Color.Black).Bold().Value = "Поле 2";
                    workSheet.Cells[1, 12].Border(Color.Black).Bold().Value = "Поле 3";
                    workSheet.Cells[1, 13].Border(Color.Black).Bold().Value = "Код канала рекламы";


                    var users = db.WheelYourBalanceUsers.AsNoTracking().Where(x => x.CreatedDate >= resultStartDate && x.CreatedDate < resultEndDate).ToList();

                    for (var i = 0; i < users.Count; i++)
                    {
                        workSheet.Cells[i + 2, 2].Value = users[i].Name;
                        workSheet.Cells[i + 2, 4].Value = users[i].Email;
                        workSheet.Cells[i + 2, 9].Value = users[i].CreatedDate.ToString("yyyy-MM-dd hh:mm:ss");
                        workSheet.Cells[i + 2, 10].Value = "поле 1";
                        workSheet.Cells[i + 2, 11].Value = "поле 2";
                        workSheet.Cells[i + 2, 12].Value = "поле 3";
                        workSheet.Cells[i + 2, 13].Value = 0;
                    }

                    for (var i = 1; i <= 13; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }


                    return new FileContentResult(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"natagerman.ru {resultStartDate:dd.MM.yyyy}-{resultEndDate:dd.MM.yyyy}.xlsx"
                    };
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", @"Что то пошло не так...");
                Console.WriteLine(e);
            }
            
            return View(model);
        }

    }
}