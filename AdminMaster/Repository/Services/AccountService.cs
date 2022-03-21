using AdminMaster.Models;
using AdminMaster.Repository.Interface;
using AdminMaster.Utils.Enums;
using AdminMaster.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AdminMaster.Repository.Services
{
    public class AccountService : IUsers
    {
        private MSDBContext dbContext;
        public AccountService()
        {
            dbContext = new MSDBContext();
        }   
        public SignInEnum SingIn(SignInModel model)
        {
            var user = dbContext.Tbl_Users.SingleOrDefault(e => e.Email == model.Email && e.Password == model.Password);
            if(user != null)
            {
                if (user.isVerified)
                { 
                    if (user.isActive)
                    {
                        return SignInEnum.Success;
                    }
                    else
                    {
                        return SignInEnum.InActive;
                    }
                }
                else
                {
                    return SignInEnum.NotVerified;
                }
            }
            else
            {
                return SignInEnum.WrongCredentials;
            }
        }

        public SignUpEnum SingUp(SignUpModel model)
        {
            if (dbContext.Tbl_Users.Any(e => e.Email == model.Email))
            {
                return SignUpEnum.EmailExist;
            }
            else
            {
                var user = new Tbl_Users()
                {
                    F_Name = model.Fname,
                    L_Name = model.Lname,
                    Password = model.ComfirmPassword,
                    Gender = model.Gender,
                    Email = model.Email
                };
                dbContext.Tbl_Users.Add(user);
                string otp = GenerateOTP();
                SendMail(model.Email,otp);
                var V_account = new Verified_Account()
                {
                    OTP = otp,
                    UserId = model.Email,
                    SendTime = DateTime.Now
                };
                dbContext.Verified_Account.Add(V_account);
                dbContext.SaveChanges();
                return SignUpEnum.Success;
            }
            return SignUpEnum.Failure;
        }

        private void SendMail(string to, string OTP)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("ankitdemo12345@gmail.com");
            mail.Subject = "Varify Yout Account";
            string Body = $"Your OTP is <br> {OTP}<br> <br/> thanks for choosing us.";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("ankitdemo12345@gmail.com", "Ankit123456");  // enter senders user name and password
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        private string GenerateOTP()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var list = Enumerable.Repeat(0, 6).Select(x => chars[random.Next(chars.Length)]);
            var otp_code = string.Join("", list);
            return otp_code;
        }

        public bool VerifyAccount(string Otp)
        {
            if (dbContext.Verified_Account.Any(e => e.OTP == Otp))
            {
                var Acc = dbContext.Verified_Account.SingleOrDefault(e => e.OTP == Otp);
                var User = dbContext.Tbl_Users.SingleOrDefault(e => e.Email == Acc.UserId);
                User.isVerified = true;
                User.isActive = true;
                dbContext.Verified_Account.Remove(Acc);
                dbContext.Tbl_Users.Update(User);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
