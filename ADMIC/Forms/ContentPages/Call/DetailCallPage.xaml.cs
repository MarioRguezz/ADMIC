using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIC.Data.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Call
{
    public partial class DetailCallPage : BasePage
    {
        string id_convocatoria = null;
        public DetailCallPage(Convocatoria call)
        {
            InitializeComponent();
            _image.Source = call.ruta_imagen;
            Title = call.titulo;
            _title.Text = call.titulo;
            id_convocatoria = call.id_convocatoria + "";
            _fechainicio.Text = DateTime.Parse(call.fecha_inicio).ToString("dd/MM/yyyy");
            _fechafin.Text = DateTime.Parse(call.fecha_cierre).ToString("dd/MM/yyyy");


            _flowListView.FlowItemsSource = call.documentos.ToList();

            var x = Math.Ceiling((double)call.documentos.Count / 3);
            _flowListView.HeightRequest = x * _flowListView.HeightRequest;

            _flowListView.FlowItemTapped += (sender, e) =>
            {
                var item = e.Item as Documento;
                if (item != null)
                    System.Diagnostics.Debug.WriteLine("Tapped {0}", item.titulo);
                Device.OpenUri(new Uri(item.ruta_documento));

            };
        }



        async void information(object sender, System.EventArgs e)
        {
            CheckConnection();
            ShowProgress("Validando");
            var user = PropertiesManager.GetUserInfo();
            //var response = await ClientGuanajoven.sendEmail(user.data.datos_usuario.id_usuario + "", id_convocatoria);
            var response = await ClientGuanajoven.sendEmail(user.data.api_token + "", id_convocatoria);

            if (ValidateResponseRegister(response))
            {
                await Task.Delay(600);
                await DisplayAlert("ADMIC", "Gracias por interesarte en la convocatoria, en breve te llegará un correo electrónico con más información", "Aceptar");
            }

            HideProgress();
        }

        bool ValidateResponseRegister(string response)
        {
            if (ClientGuanajoven.IsError(response))
            {
                DisplayAlert("Error", "Hubo un problema al ponerse en contacto. Intenta más tarde", "Aceptar");
                return false;
            }
            else
            {
                return true;
            }
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
