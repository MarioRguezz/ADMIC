using System;
namespace ADMIC.Providers
{
    public interface IPushNotifications
    {
        void Register();
        void Unregister();
        string IdUnique();
    }
}