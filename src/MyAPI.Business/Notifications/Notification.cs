using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Business.Notifications
{
    public class Notification
    {
        public string Message { get; set; }
        public Notification(string message)
        {
            Message = message;
        }
    }
}
