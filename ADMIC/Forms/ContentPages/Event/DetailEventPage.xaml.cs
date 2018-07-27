using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIC.Data.Models;
using ADMIC.Helpers;
using ADMIC.Providers;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ADMIC.Forms.ContentPages.Event
{
    public partial class DetailEventPage : BasePage
    {


        public static string street = null, streetNumber, colony = null, city = null, postalCode = null, state = null, country = null;

        Position START_POINT;
        Distance START_DISTANCE = Distance.FromKilometers(0.2);
        static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
        DateTime now = DateTime.Parse(timeString);
        int time = 0;
        string id_evento = "";
        Evento eventodetail = null;

        bool chooseAddress;

        public DetailEventPage(Evento evento)
        {
            InitializeComponent();
            eventodetail = evento;
            START_POINT = new Position(evento.latitud, evento.longitud);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(START_POINT, START_DISTANCE));
            Title = evento.titulo;
            _title.Text = evento.titulo;
            _direccion.Text = evento.direccion;
            _evento.Text = evento.descripcion;
            var fechainiciox = DateTime.Parse(evento.fecha_inicio).ToString("dd/MM/yyyy");
            var fechafinx = DateTime.Parse(evento.fecha_fin).ToString("dd/MM/yyyy");
            _fecha.Text = fechainiciox + " - " + fechafinx;
            DateTime fechafin = DateTime.Parse(evento.fecha_fin);
            DateTime fechainicio = DateTime.Parse(evento.fecha_inicio);
            id_evento = evento.id_evento + "";
            if (now < fechainicio)
            {
                _assistance.Text = "Me interesa";
                time = 0;
            }
            if (now <= fechafin && now >= fechainicio)
            {
                _assistance.Text = "Estoy en el evento";
                time = 1;
            }
            if (now > fechafin)
            {
                _assistance.Text = "Este evento ha caducado";
                time = 2;
            }

            _flowListView.FlowItemsSource = evento.documentos.ToList();

            var x = Math.Ceiling((double)evento.documentos.Count / 3);

            _flowListView.HeightRequest = x * _flowListView.HeightRequest;

            _flowListView.FlowItemTapped += (sender, e) =>
            {
                var item = e.Item as Documentos;
                if (item != null)
                    System.Diagnostics.Debug.WriteLine("Tapped {0}", item.titulo);
                Device.OpenUri(new Uri(item.ruta_documento));

            };

            if (!evento.documentos.Any())
            {
                _document.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetLocation();
            //var x = LocationHelper.Instance.CurrentPosition.Latitude;
        }


        async void EventoClicked(object sender, EventArgs args)
        {
            if (time == 0)
            {

                var posicion = await ClientGuanajoven.InteresaEvento(PropertiesManager.GetUserInfo().data.api_token, eventodetail.id_evento + "");
                ShowProgress(IProgressType.LogedIn);
                await Task.Delay(600);
                HideProgress();
                await DisplayAlert("ADMIC", "Gracias por estar interesado en el evento, en breve te llegará un correo electrónico con más información", "Aceptar");
            }

            else if (time == 1)
            {



                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    var res = await Application.Current.MainPage.DisplayAlert("Ubicación requerida", "¿Deseas abrir los ajustes para permitir accesar a tu ubicación?.", "Ir a ajustes", "Cancelar");
                    if (res)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return;
                }


                var user = PropertiesManager.GetUserInfo();
                var response = await ClientGuanajoven.EventNotification(user.data.api_token, id_evento, LocationHelper.Instance.CurrentPosition.Latitude + "", LocationHelper.Instance.CurrentPosition.Longitude + "");
                try
                {
                    var puntaje = JsonConvert.DeserializeObject<PuntajeResponse>(response);
                    if (puntaje.data.asistio == 0)
                    {
                        if (puntaje.errors.Count > 0)
                        {
                            await DisplayAlert("ADMIC", "No te encuentras en el rango del evento", "Aceptar");
                        }
                        else
                        {
                            await DisplayAlert("ADMIC", "Ya has sido registrado", "Aceptar");
                        }
                    }
                    else
                    {
                        user.data.puntaje = user.data.puntaje + puntaje.data.puntos_otorgados;
                        //var posicion = await ClientGuanajoven.getPosition(user.data.api_token);
                        //user.data.posicion = ClientGuanajoven.Data(posicion);
                        user.data.posicion = await getPuntaje(user.data.api_token);
                        PropertiesManager.SaveUserInfo(user);
                        await DisplayAlert("ADMIC", "Has ganado " + puntaje.data.puntos_otorgados + " puntos", "Aceptar");
                        MessagingCenter.Send<DetailEventPage>(this, "Score");
                    }
                }
                catch (Exception ex)
                {
                }
                ShowProgress(IProgressType.LogedIn);
                await Task.Delay(600);

                HideProgress();

            }
            else
            {
                _assistance.IsEnabled = false;
            }
        }


        async Task<string> getPuntaje(string apitoken)
        {
            var position = await ClientGuanajoven.getPosition(apitoken);
            return ClientGuanajoven.Data(position);
        }


        async void GetLocation()
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(START_POINT, START_DISTANCE));

        }


    }


}

