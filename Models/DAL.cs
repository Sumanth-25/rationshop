using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DomesticShop.Models
{
    public class DAL
    {

        public string Con = ConfigurationManager.ConnectionStrings["constring"].ToString();

        //-------------------------------------------------LOGIN-----------------------------------

        public string Login(DomesticModel ml)
        {
            using (SqlConnection sqlcon = new SqlConnection(Con))
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("Sp_Employeelogin", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Username", ml.Username);
                sqlcmd.Parameters.AddWithValue("@Password", ml.Password);
                string i = sqlcmd.ExecuteScalar().ToString();
                sqlcon.Close();
                return i;
            }
        }

        //-----------------------------MAIL BODY---------------------------------------------------
        public string PopulateBody(string path)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }

        //-------------------------------ADD CONSUMER DETAILS--------------------------------------

        public int Insert(Datamodel DAM)
        {
            List<Datamodel> lst = new List<Datamodel>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_People", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", DAM.Name);
            cmd.Parameters.AddWithValue("@DOB", DAM.DOB);
            cmd.Parameters.AddWithValue("@PhoneNumber", DAM.PhoneNumber);
            cmd.Parameters.AddWithValue("@AadhaarNumber", DAM.AadhaarNumber);
            cmd.Parameters.AddWithValue("@RationCardNumber", DAM.RationCardNumber);
            cmd.Parameters.AddWithValue("@Email", DAM.Email);
            cmd.Parameters.AddWithValue("@Things", DAM.Things);
            cmd.Parameters.AddWithValue("@Action", 1);
            con.Open(); 
            int K = (int)cmd.ExecuteNonQuery();
            con.Close();
            return K;
        }
        //--------------------------------CONSUMER LIST---------------------------------------------
        public List<Datamodel> GetAllPeople()
        {
            List<Datamodel> lst = new List<Datamodel>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_People", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", 4);
            con.Open();
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
                if (Sdr.FieldCount > 0)
                {
                    Datamodel DM = new Datamodel();
                    DM.Id = Convert.ToInt32(Sdr["Id"]);
                    DM.Name = Convert.ToString(Sdr["Name"]);
                    DM.DOB = Convert.ToDateTime(Sdr["DOB"]);
                    DM.PhoneNumber = Convert.ToInt64(Sdr["PhoneNumber"]);
                    DM.AadhaarNumber = Convert.ToInt64(Sdr["AadhaarNumber"]);
                    DM.RationCardNumber = Convert.ToInt64(Sdr["RationCardNumber"]);
                    DM.Email = Convert.ToString(Sdr["Email"]);
                    DM.Things = Convert.ToString(Sdr["Things"]);
                    lst.Add(DM);
                }
            return lst;
        }
        //----------------------------------UPDATE CONSUMER DETAILS-----------------------------------
        public int Update(Datamodel DAM)
        {
            List<Datamodel> lst = new List<Datamodel>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_People", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", DAM.Name);
            cmd.Parameters.AddWithValue("@DOB", DAM.DOB);
            cmd.Parameters.AddWithValue("@PhoneNumber", DAM.PhoneNumber);
            cmd.Parameters.AddWithValue("@AadhaarNumber", DAM.AadhaarNumber);
            cmd.Parameters.AddWithValue("@RationCardNumber", DAM.RationCardNumber);
            cmd.Parameters.AddWithValue("@Email", DAM.Email);
            cmd.Parameters.AddWithValue("@Things", DAM.Things);
            cmd.Parameters.AddWithValue("@Action", 2);
            cmd.Parameters.AddWithValue("@ID", DAM.Id);
            con.Open();
            int J = cmd.ExecuteNonQuery();
            con.Close();
            return J;

        }
        //----------------------------------DELETE CONSUMER----------------------------------------
        public int Delete(int ID)
        {
            List<Datamodel> lst = new List<Datamodel>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_People", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Action", 3);
            con.Open();
            int L = cmd.ExecuteNonQuery();
            con.Close();
            return L;
        }

        //---------------------------GET ALL MAILS---------------------------------------------------
        public string GetAllEmail()
        {
            string Email = string.Empty;
            
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("sp_SelectAll", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
                if (Sdr.FieldCount > 0)
                {
                    Datamodel DM = new Datamodel();
                    Email = Convert.ToString(Sdr["Email"]);
                    
                }
            return Email;
        }
        //--------------------------GET SEPERATE MAIL--------------------------------------------------
        
        public int InsertOverAllMsg(string Email,string message)
        {
            List<Datamodel> lst = new List<Datamodel>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_InsertMailSentRecord", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@SentDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Action", 1);
            con.Open();
            int K = (int)cmd.ExecuteNonQuery();
            con.Close();
            return K;
        }

        //------------------------------E-MAIL LIST-----------------------------------------------------
        public List<MailRecord> GetMailRecord()
        {
            List<MailRecord> lst = new List<MailRecord>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_InsertMailSentRecord", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", 2);
            con.Open();
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
                if (Sdr.FieldCount > 0)
                {
                    MailRecord DM = new MailRecord();
                    DM.Id = Convert.ToInt32(Sdr["Id"]);
                    DM.Email = Convert.ToString(Sdr["Email"]);
                    DM.message = Convert.ToString(Sdr["message"]);
                    DM.SentDate = Convert.ToString(Sdr["SentDate"]);
                    lst.Add(DM);
                }
            return lst;
        }
    }
}