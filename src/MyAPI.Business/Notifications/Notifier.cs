using MyAPI.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAPI.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool Any()
        {
            return _notifications.Any();
        }
    }
}
