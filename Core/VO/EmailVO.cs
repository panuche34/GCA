using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Core.Entity;

namespace Core.VO
{
    public class EmailVO
    {
 
            public string To { get; set; }
            public string Subject { get; set; }
            public string BodyMessage { get; set; }



        public EmailVO ToModel(string to, string subject, string bodymessage)
        {

            To = to;
            Subject = subject;
            BodyMessage = bodymessage;
           
            
            return this;

        }
           
    }
}
