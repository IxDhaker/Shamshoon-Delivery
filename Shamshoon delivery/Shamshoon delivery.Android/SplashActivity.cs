using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;

namespace Shamshoon_delivery.Droid
{
    // MainLauncher = true ya3ni hedhi awel page tban ki t7el l'application
    [Activity(Theme = "@style/MyTheme.Splash",
              MainLauncher = true,
              NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            // T3addi direct lel MainActivity elli feha l'application Xamarin
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}