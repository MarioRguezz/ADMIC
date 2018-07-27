using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ADMIC.Forms.ContentPages.Menu;
using Xamarin.Forms;

namespace ADMIC.Forms.ContentPages.Promotion
{
    public partial class PromotionPage : BasePage
    {
        HomeDrawerPage RootPage;
        public static bool Changed = false;
        public static List<ADMIC.Data.Models.Promotion> _promociones = null;
        ObservableCollection<ADMIC.Data.Models.Promotion> _itemsList = new ObservableCollection<ADMIC.Data.Models.Promotion>();
        static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
        static DateTime time = DateTime.Parse(timeString);
        static DateTime dateToSend = time;
        string empresa_imagen = null;
        public PromotionPage(ADMIC.Data.Models.Empresa empresa)
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);

            Title = empresa.nombre_comercial;

            _image.Source = empresa.logo;
            empresa_imagen = empresa.logo;

            _itemsList.Clear();
            _promociones = empresa.promociones.OrderBy(x => x.fecha_inicio).ToList();

            foreach (var item in _promociones)
            {
                _itemsList.Add(item);
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                if (ListView.ItemsSource != _itemsList)
                    ListView.ItemsSource = _itemsList;
            });

            ListView.ItemsSource = _itemsList;
         
        }


        async void TapItem(object sender, System.EventArgs e)
        {
            var stack = (StackLayout)sender;
            await stack.ScaleTo(0.9, 100, Easing.SinIn);
            await stack.ScaleTo(1, 100, Easing.SinIn);
            if (stack.BindingContext is ADMIC.Data.Models.Promotion)
            {
                var promotion = stack.BindingContext as ADMIC.Data.Models.Promotion;

                await Navigation.PushAsync(new DetailPromotionPage(promotion, empresa_imagen));
            }
        }

        async void CloseClicked(object sender, System.EventArgs e)
        {
            var image = sender as Image;
            image.Opacity = 0.6;
            image.FadeTo(1);
            RootPage.IsPresented = true;
            //await Navigation.PopAsync();
        }

    }
}