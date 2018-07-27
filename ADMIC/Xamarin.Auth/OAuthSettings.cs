using System;
namespace ADMIC.Xamarin.Auth
{

    public class OAuthSettingsFacebook
    {
        public static string ClientId = "203591490120675";
        public static string Scope = "email+public_profile";
        public static string RedirectUrl = "https://www.facebook.com/connect/login_success.html";
        public static string AuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
    }

    public class OAuthSettingsGoogle
    {
        public static string ClientId = "541564627298-7l7dsbs4ckrbs3nc7muqdc8mk7bslspj.apps.googleusercontent.com";

        public static string ClientSecret = null;
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string RedirectUrl = "com.googleusercontent.apps.541564627298-7l7dsbs4ckrbs3nc7muqdc8mk7bslspj:/oauth2redirect";
        public static string Scope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.login";

    }
}