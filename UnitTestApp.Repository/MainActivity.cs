using System.Reflection;
using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using DataRepository.Library;

namespace UnitTestApp.Repository
{
    [Activity(Label = "UnitTestApp.Repository", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var repository = new Respository();
            repository.ReadPsi();

            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}

