﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;

using ADMIC.Data.Models;
using ADMIC.Providers;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Login
{
    public partial class ForgotPassword : BasePage
    {
        public ForgotPassword()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }


        async void Start(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new ForgotPassword());
        }

        async void Recuperar(object sender, System.EventArgs e)
        {
            if (ValidateUI())
            {
                CheckConnection();
                ShowProgress("Validando");
                var response = await ClientGuanajoven.recoverpass(_email.Text);
                if (ValidateResponse(response))
                {
                    ShowProgress(IProgressType.LogedIn);
                    await Task.Delay(600);
                    HideProgress();
                    await DisplayAlert("Guanajoven", "Se ha mandado un correo para recuperar su contraseña", "Aceptar");

                }
                HideProgress();
            }
        }


        async void CloseClicked(object sender, System.EventArgs e)
        {
            var image = sender as Image;
            image.Opacity = 0.6;
            image.FadeTo(1);
            await Navigation.PopAsync();
        }

        bool ValidateResponse(string response)
        {
            if (ClientGuanajoven.IsError(response))
            {
                DisplayAlert("Error", "Usuario no existe", "Aceptar");
                return false;
            }
            else
            {
                return true;
            }
        }

        bool ValidateUI()
        {
            if (string.IsNullOrEmpty(_email.Text) || !Regex.IsMatch(_email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                DisplayAlert("Error", "Verifique su correo", "Aceptar");

                return false;
            }
            return true;

        }

        #region  Connection
        async void CheckConnection()
        {
            try
            {
                //var res = await CrossConnectivity.Current.IsReachable("http://www.google.com") ? "Reachable" : "Not reachable";
                var res = await CrossConnectivity.Current.IsReachable("http://www.google.com") ? true : false;

                if (res)
                {
                    System.Diagnostics.Debug.WriteLine("conexion");
                }
                else
                {
                    //await DisplayAlert("Error", "Verifique su conexión a internet", "Aceptar");
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion


    }
}
