using System;
using System.Collections.Generic;
using ADMIC.Forms.ContentPages.Menu;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.About
{
    public partial class AboutPage : BasePage
    {
        HomeDrawerPage RootPage;
        public AboutPage(HomeDrawerPage _rootPage)
        {
            InitializeComponent();
            RootPage = _rootPage;
            NavigationPage.SetHasNavigationBar(this, false);
            _queHacemos.Text = "Somos una organización del sector privado, nuestro principal objetivo es ofrecer un PROGRAMA INTEGRAL DE ASESORÍA, CAPACITACIÓN Y CRÉDITO" +
                " A LA MICRO Y LA PEQUEÑAS EMPRESA para que de esta forma se FORTALEZCA Y LOGRE CRECIMIENTO Y DESARROLLO";
            _queHacemos.HorizontalTextAlignment = TextAlignment.Start;
            _logros.Text = "Ofrecemos soluciones prácticas y concretas para mejorar y/o fortalecer las principales áreas de oportunidad en tu empresa. Como pueden ser:" +
                "\n* Controles administrativos." +
                " \n* Diseño de imagen.\n * Control de costos." +
                " \n* Sondeo de mercado, etc.";
            _logros.HorizontalTextAlignment = TextAlignment.Start;
            _capacitacion.Text = "Capacitación básica con la oportunidad de conocer y aplicar de manera sencilla y práctica en temas relacionados con el negocio, la persona y la familia." +
                "\n* Trabajo en equipo." +
                " \n* Atención a clientes." +
                " \n* Desarrollo humano."+
                " \n* Liderazgo, etc.";
            _capacitacion.HorizontalTextAlignment = TextAlignment.Start;
            _cap.Text = "Después de analizar juntos sus necesidades y definir sus proyectos de crecimiento y" +
                "expansión, evaluamos la posibilidad de otorgarle un crédito el cual puede ser utilizado para: \n* Surtir mercancía." +
                " \n*Compra de herramienta." +
                " \n* Compra de maquinaria." +
                " \n* Compra de equipo. \n* Ampliar o mejorar las instalaciones.";
            _cap.HorizontalTextAlignment = TextAlignment.Start;
        }

        async void CloseClicked(object sender, System.EventArgs e)
        {
            var image = sender as Image;
            image.Opacity = 0.6;
            image.FadeTo(1);
            RootPage.IsPresented = true;
            //await Navigation.PopAsync()       }
        }

        async void lineas(object sender, EventArgs args)

        {
            Device.OpenUri(new Uri("http://jovenes.guanajuato.gob.mx/index.php/702-2/"));
        }

        async void encuestas(object sender, EventArgs args)

        {
            Device.OpenUri(new Uri("http://jovenes.guanajuato.gob.mx/index.php/encuesta-de-juventud/"));
        }


        async void diagnosticos(object sender, EventArgs args)

        {
            Device.OpenUri(new Uri("http://jovenes.guanajuato.gob.mx/index.php/diagnostico-juvenil/"));
        }


        async void directorios(object sender, EventArgs args)

        {
            Device.OpenUri(new Uri("http://transparencia.guanajuato.gob.mx/transparencia/informacion_publica_directorio.php"));
        }
    }
}
