using MyAPI.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
