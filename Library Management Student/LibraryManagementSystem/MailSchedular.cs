﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.MiddleLayer;
using System.Data;

namespace LibraryManagementSystem
{

    public class MailSchedular
    {

        //Intilizing middle layer
        LibraryMiddleLayer md;
        public MailSchedular()
        {
            md = new LibraryMiddleLayer();
        }
        public static void Main(string[] args)
        {
            MailSchedular md = new MailSchedular();
            md.SendMail();
        }

        public  void SendMail()
        {
            DataTable dt = new DataTable();
            dt = md.GetPenaltyData();
            if(dt.Rows.Count>0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    string userId = dt.Rows[i]["penalty"].ToString();
                    string name = dt.Rows[i]["StudentName"].ToString();
                    string email = dt.Rows[i]["Email"].ToString();
                    string subject = "Return date exceeded";

                    int penalty = Convert.ToInt32(dt.Rows[i]["penalty"].ToString());
                    if (penalty==10)
                    {
                        
                        string body = "<p>HI "+name+" </p> <p> Account locked because you have exceded the penaulty 10$ </p> <p> If you want to unlock the account please pay 10$ </p> <p> Thanks, </p> <p> Library management </p>";
                        md.sendMail(subject, body, email, userId, name);
                    }
                    else
                    {
                        subject = "Account Locked";
                        string body = "<p>HI " + name + " </p> <p> Account locked because you have exceded the penaulty 10$ </p> <p> If you want to unlock the account please pay 10$ </p> <p> Thanks, </p> <p> Library management </p>";
                        md.sendMail(subject, body, email, userId, name);
                        int result = 0;
                        result = md.UpdatePenaulty(userId);
                    }
                }
               
            }

        }

    }


   
}

    
