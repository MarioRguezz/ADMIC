using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using ADMIC.Data.Models;
using ADMIC.Forms.ContentPages.Menu;

namespace ADMIC.Forms.ContentPages.Profile
{
    public partial class ProfilePage : BasePage
    {
        string url = null;

        HomeDrawerPage RootPage;
        public ProfilePage(HomeDrawerPage _rootPage)
        {
            InitializeComponent();
            RootPage = _rootPage;

            NavigationPage.SetHasNavigationBar(this, false);
            Background.BackgroundColor = Color.FromHex("#A5FFFFFF");

            ImageSourceChanged = () =>
            {
                if (LastView is FFImageLoading.Forms.CachedImage)
                    (LastView as FFImageLoading.Forms.CachedImage).Source = Source;

                _imageView.Source = Source;
            };

        }

       
        bool firstTime = true;

        protected override void OnAppearing()
        {

            if (firstTime)
            {
                pickerSetStudies();
                getProfile();
            }


            firstTime = false;
        }


        #region Events

        async void CloseClicked(object sender, System.EventArgs e)
        {
            var image = sender as Image;
            image.Opacity = 0.6;
            image.FadeTo(1);
            //await Navigation.PopAsync();
            //MessagingCenter.Send<ProfilePage>(this, "show_home");
            RootPage.IsPresented = true;

        }




        async void ChangePicture(object sender, EventArgs e)
        {
            TakePictureActionSheet(_imageView);
        }



        async void getProfile()
        {

            ShowProgress("Validando");
            TokenPOJO token = new TokenPOJO();
            var localuser = PropertiesManager.GetUserInfo();
            token.api_token = localuser.data.api_token;
            CheckConnection();
            var response = await ClientGuanajoven.getProfile(token.api_token);

            if (ValidateResponse(response))
            {
                var dataProfile = JsonConvert.DeserializeObject<DataUserResponse>(response);
                SetInfo(dataProfile);
                await Task.Delay(600);
            }
            HideProgress();
        }

        void SetInfo(DataUserResponse profile)
        {
            _imageView.Source = profile.data.ruta_imagen;
            url = profile.data.ruta_imagen;
            NivelEstudios(profile);
            NewFields(profile);
        }

        void NivelEstudios(DataUserResponse profile)
        {
            if (profile.data.id_nivel_estudios == null)
            {
            }
            else
            {
                pickernivelestudios.SelectedIndex = (int)profile.data.id_nivel_estudios;
            }
        }

        void NewFields(DataUserResponse profile)
        {
            inputCiudad.Text = profile.data.ciudad;
            inputColonia.Text = profile.data.colonia;
            inputCalle.Text = profile.data.calle;
            inputEmpresa.Text = profile.data.empresa;
            inputActividad.Text = profile.data.actividad;
            inputEmpleados.Text = profile.data.num_empleados;
            inputRFC.Text = profile.data.rfc;
            inputAntiguedad.Text = profile.data.antiguedad;
        }



        async void ModifyClicked(object sender, System.EventArgs e)
        {
            var answer = await DisplayAlert("ADMIC", "¿Deseas modificar tu perfil?", "Sí", "No");
            if (!answer)
            {
            }
            else
            {
                DatosUsuario user = new DatosUsuario();
                var localuser = PropertiesManager.GetUserInfo();
                user.api_token = localuser.data.api_token;
                if (pickernivelestudios.SelectedIndex != 0)
                {
                    user.id_nivel_estudios = pickernivelestudios.SelectedIndex;
                }
                if (isNull(inputCiudad.Text))
                {
                    await DisplayAlert("Error", "Falta ciudad", "Aceptar");
                    return;
                }
                if (isNull(inputColonia.Text))
                {
                    await DisplayAlert("Error", "Falta colonia", "Aceptar");
                    return;
                }
                if (isNull(inputCalle.Text))
                {
                    await DisplayAlert("Error", "Falta calle", "Aceptar");
                    return;
                }
                if (isNull(inputEmpresa.Text))
                {
                    await DisplayAlert("Error", "Falta empresa", "Aceptar");
                    return;
                }
                if (isNull(inputActividad.Text))
                {
                    await DisplayAlert("Error", "Falta actividad", "Aceptar");
                    return;
                }
                if (isNull(inputEmpleados.Text))
                {
                    await DisplayAlert("Error", "Falta número de empleados", "Aceptar");
                    return;
                }
                if (isNull(inputRFC.Text))
                {
                    await DisplayAlert("Error", "Falta número de RFC", "Aceptar");
                    return;
                }
                if (isNull(inputAntiguedad.Text))
                {
                    await DisplayAlert("Error", "Falta número de antigüedad", "Aceptar");
                    return;
                }
                user.ciudad = inputCiudad.Text;
                user.colonia = inputColonia.Text;
                user.calle = inputCalle.Text;
                user.empresa = inputEmpresa.Text;
                user.actividad = inputActividad.Text;
                user.num_empleados = inputEmpleados.Text;
                user.rfc = inputRFC.Text;
                user.antiguedad = inputAntiguedad.Text;
                user.idiomas = new List<IdiomaAdicional>();
                try
                {
                    user.ruta_imagen = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
                }
                catch (Exception ex)
                {
                    HttpClient client = new HttpClient();
                    client.MaxResponseContentBufferSize = 256000;
                    Stream stream = await client.GetStreamAsync(url);
                    var xy = stream;
                    var y = GetImageStreamAsBytes(xy);
                    var imageurl = "data:image/jpeg;base64," + Convert.ToBase64String(y);
                    user.ruta_imagen = imageurl;
                }

                CheckConnection();
                ShowProgress("Validando");

                var response = await ClientGuanajoven.updateProfile(user);
                var updateAct = await ClientGuanajoven.getProfile(user.api_token);
                var updated = JsonConvert.DeserializeObject<DataUserResponse>(updateAct);
                var pivot = PropertiesManager.GetUserInfo();
                pivot.data.datos_usuario.ruta_imagen = updated.data.ruta_imagen;
                PropertiesManager.SaveUserInfo(pivot);

                if (ValidateResponseRegister(response))
                {
                    await Task.Delay(600);
                    await DisplayAlert("ADMIC", "Datos Guardados con exito", "Aceptar");
                }

                HideProgress();
            }
        }



        bool isNull(string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                return true;
            }
            return false;
        }

        public byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        bool ValidateResponseRegister(string response)
        {
            if (ClientGuanajoven.IsError(response))
            {
                DisplayAlert("Error", "No pudimos guardar tu información", "Aceptar");
                return false;
            }
            else
            {
                return true;
            }
        }


        #endregion



        #region Pickers
        public void pickerSetStudies()
        {
            pickernivelestudios.Items.Add("Seleccione una opción");
            pickernivelestudios.Items.Add("Primaria");
            pickernivelestudios.Items.Add("Secundaria");
            pickernivelestudios.Items.Add("Preparatoria");
            pickernivelestudios.Items.Add("TSU");
            pickernivelestudios.Items.Add("Universidad");
            pickernivelestudios.Items.Add("Maestría");
            pickernivelestudios.Items.Add("Doctorado");
            pickernivelestudios.Items.Add("Otro");
            pickernivelestudios.SelectedIndex = 0;
            pickernivelestudios.SelectedIndexChanged += (sender, e) =>
            {
                if (pickernivelestudios.SelectedIndex == 0)
                    pickernivelestudios.SelectedIndex = 1;
            };

        }


        #endregion

        bool ValidateResponse(string response)
        {
            if (ClientGuanajoven.IsError(response))
            {
                DisplayAlert("Error", "Hubo un problema al iniciar sesión", "Aceptar");
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