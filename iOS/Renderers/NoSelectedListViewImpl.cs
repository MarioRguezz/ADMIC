using System;
using System;
using ADMIC.Forms.CustomViews;
using ADMIC.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NoSelectedListView), typeof(NoSelectedListViewImpl))]
namespace ADMIC.iOS.Renderers
{
    public class NoSelectedListViewImpl : ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
            }

            if (e.NewElement != null)
            {
                // Configure the native control and subscribe to event handlers
                Control.AllowsSelection = false;
            }
        }

    } // public class MyAppListViewNonSelectableRenderer : ListViewRenderer
}


