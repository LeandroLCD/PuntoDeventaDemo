using Android.App;
using Android.Content;
using Android.OS;

namespace PuntoDeventa.Droid
{
    [Activity(Label = "PuntoDeVenta", Name = "com.blipblipcode.puntodeventa.StartActivity")]
    public class StartActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            StartActivity(new Intent(this, typeof(MainActivity)));
            Finish();
        }

    }
}