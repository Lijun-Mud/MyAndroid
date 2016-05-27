using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using System.Threading.Tasks;

namespace AndroidSmthApp.Fragments
{
    public class LoginFragment : Fragment
    {
        private Button _loginButton;
        private Context _context;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static LoginFragment NewInstance()
        {
            return new LoginFragment { Arguments = new Bundle() };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view= inflater.Inflate(Resource.Layout.Login, null);
            _context = view.Context;
            _loginButton =view.FindViewById<Button>(Resource.Id.login_login_button);
            _loginButton.Click += LoginButton_Click;
            return view;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            _loginButton.Enabled = false;

            var pr = new Android.App.ProgressDialog(_context);
            pr.SetMessage("Login...");
            pr.SetCancelable(false);
            pr.Show();

            await Task.Factory.StartNew(() => BigLongImportantMethodAsync());

            pr.Hide();
            _loginButton.Enabled =true;
        }

        private async void BigLongImportantMethodAsync()
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}