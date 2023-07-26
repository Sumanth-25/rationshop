using System;  
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;  
using System.Net.Mail;  
using System.Web;  
using System.Web.Mvc;
using DomesticShop.Models;
using System.Configuration;
using System.Data;

namespace DomesticShop.Controllers
{
    public class SendMailerController : Controller
    {
        DAL DL = new DAL();
        // GET: SendMailer

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(DomesticModel ml)
        {
            string i = DL.Login(ml);
            Session["UserAvailable"] = i;
            if (Session["UserAvailable"] != "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Failedcount = i;

            }
            return View();
        }
        //--------------------------------------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Insert()
        {
                return View();

        }
        [HttpPost]
        public ActionResult Insert(Datamodel DAM)
        {
                DL.Insert(DAM);
                return RedirectToAction("PeopleList");


        }
        
        public ActionResult PeopleList()
        {
                return View(DL.GetAllPeople());
        }
        
        public ActionResult ConsumerStockList()
        {
            return View(DL.GetAllPeople());
        }
        
        public ActionResult MailRecordList()
        {
                return View(DL.GetMailRecord());
        }
        
        public ActionResult Update(int ID)
        {
                return View(DL.GetAllPeople().Find(SM => SM.Id == ID));


        }
        [HttpPost]
        public ActionResult Update(Datamodel DAM)
        { 
                DL.Update(DAM);
                return RedirectToAction("PeopleList");

        }
        
        public ActionResult ConsumerStockUpdate(int ID)
        {
            return View(DL.GetAllPeople().Find(SM => SM.Id == ID));

        }
        
        [HttpPost]
        public ActionResult ConsumerStockUpdate(Datamodel DAM)
        {
            DL.Update(DAM);
            return RedirectToAction("ConsumerStockList");

        }
        
        public ActionResult Delete(int ID)
        {
                DL.Delete(ID);
                return RedirectToAction("PeopleList");
        }
        
        [HttpPost]
        public ViewResult Index(DomesticModel _objModelMail)
        {
                string Email = DL.GetAllEmail();
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);
                mail.From = new MailAddress("captionmarvel25@gmail.com");
                mail.Subject = "Ration Shop";
                string BodyTemplate2 = DL.PopulateBody("C:\\Users\\mrmar\\Desktop\\RationShop\\DomesticShop\\genericBody.html");
                string Body = BodyTemplate2.Replace("{Body}", _objModelMail.Message);
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("captionmarvel25@gmail.com", "pnejenspnbwcjpxj");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                DL.InsertOverAllMsg(Email, _objModelMail.Message);

                return View();   
        }
        //--------------------------------------------------------------------------------
        public ActionResult SendIndividualMail()
        {
            List<Datamodel> list = new List<Datamodel>();
            list = DL.GetAllPeople();


            foreach (Datamodel item in list)
            {
                
                string Email = DL.GetAllEmail();
                MailMessage mail = new MailMessage();
                mail.To.Add(item.Email);
                mail.From = new MailAddress("captionmarvel25@gmail.com");
                mail.Subject = "Ration Shop";
                string BodyTemplate2 = DL.PopulateBody("C:\\Users\\mrmar\\Desktop\\RationShop\\DomesticShop\\genericBody.html");
                string Body = BodyTemplate2.Replace("{Body}", item.Things.ToString());
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("captionmarvel25@gmail.com", "pnejenspnbwcjpxj");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                DL.InsertOverAllMsg(item.Email, item.Things.ToString());
            }
            return RedirectToAction("MailRecordList");
        }
    }
}