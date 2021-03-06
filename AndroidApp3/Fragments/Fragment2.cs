using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Runtime;
using Android.Widget;
using System;
using DataRepository.Library;
using System.Linq;
using DataRepository.Library.Model;
using System.Threading.Tasks;

namespace AndroidApp3.Fragments
{
    public class Fragment2 : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_share1, menu);

            var shareItem = menu.FindItem(Resource.Id.action_share);

            //actionProvider = MenuItemCompat.GetActionProvider(shareItem).JavaCast<Android.Widget.ShareActionProvider>();
            actionProvider = MenuItemCompat.GetActionProvider(shareItem).JavaCast<Android.Support.V7.Widget.ShareActionProvider>();

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, "Time to share some text!");

            actionProvider.SetShareIntent(intent);

            base.OnCreateOptionsMenu(menu, inflater);
        }

        Android.Support.V7.Widget.ShareActionProvider actionProvider;

        public static Fragment2 NewInstance()
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            return frag2;
        }

        //public override void OnActivityCreated(Bundle savedInstanceState)
        //{
        //    base.OnActivityCreated(savedInstanceState);
        //    DoWork();
        //}

        private async void DoWork()
        {
            var pr = new Android.App.ProgressDialog(globalContext);
            pr.SetMessage("Loading data");
            pr.SetCancelable(false);
            pr.Show();

            var repository = new Respository();
            //var channel = await await Task.Factory.StartNew(async() =>await repository.ReadPsi());//repository.ReadPsi();
            //var channel = await await await Task.Factory.StartNew(() =>repository.ReadPsi().ContinueWith<Task<Channel>>(t=>t,TaskContinuationOptions.OnlyOnFaulted));
            await Task.Factory.StartNew(() => BigLongImportantMethodAsync());

            var channel = _channelResult;
            var psiInfo = PsiInformation.Parse(channel);
            var view = globalView;
            var foundTextbox = view.FindViewById<TextView>(Resource.Id.textViewUpdateTime);
            foundTextbox.Text = psiInfo.DisplayUpdateTime;
            foundTextbox = view.FindViewById<TextView>(Resource.Id.textView3hrPsi);
            foundTextbox.Text = psiInfo.Psi3Hour;
            foundTextbox = view.FindViewById<TextView>(Resource.Id.textView24hrPsi);
            foundTextbox.Text = psiInfo.Psi24Hour;

            var root=view.FindViewById<LinearLayout>(Resource.Id.linearLayoutRoot);
            root.Visibility = ViewStates.Visible;

            pr.Hide();
        }

        private Channel _channelResult;
        private async void BigLongImportantMethodAsync()
        {
            var repository = new Respository();
            try
            {
                _channelResult = await repository.ReadPsi();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private Context globalContext = null;
        private View globalView = null;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {            
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            this.Activity.Title = Resources.GetString(Resource.String.fragment_psi_name);
            var view= inflater.Inflate(Resource.Layout.fragment2, null);
            globalContext = view.Context;
            globalView = view;
            //var repository = new Respository();
            //var psiInfo = PsiInformation.Parse(repository.ReadPsi());

            //var foundTextbox = view.FindViewById<TextView>(Resource.Id.textViewUpdateTime);
            //foundTextbox.Text = psiInfo.DisplayUpdateTime;
            //foundTextbox = view.FindViewById<TextView>(Resource.Id.textView3hrPsi);
            //foundTextbox.Text = psiInfo.Psi3Hour;
            //foundTextbox = view.FindViewById<TextView>(Resource.Id.textView24hrPsi);
            //foundTextbox.Text = psiInfo.Psi24Hour;
            //pr.Hide();

            {
                //CipherUtility.Encrypt("a");
            }

            {
                var path = Android.App.Application.Context.FilesDir.Path;
                //PreferenceManager.GetDefaultSharedPreferences(view.Context);
                var sharedPreference= Android.App.Application.Context.GetSharedPreferences("test.dat", FileCreationMode.Private);

                var value1 = sharedPreference.GetInt("number_of_times_accessed", 0);
                var value2 = sharedPreference.GetString("your_key2", null);

                var editor = sharedPreference.Edit();
                editor.PutInt("number_of_times_accessed", 3);
                editor.PutString("date_last_accessed", DateTime.Now.ToString("yyyy-MMM-dd"));
                editor.Apply();
            }

            DoWork();

            {
                var t = view.FindViewById(Resource.Id.textClock);
                var timeTexta = view.FindViewById<TextClock>(Resource.Id.textClock);
                timeTexta.Text = DateTime.Now.ToString();
            }
            return view;
        }
    }

    public class PsiInformation
    {
        private const string DefaultEmptyInformation = "N/A";
        private const string PsiDatetimeFormat = "yyyyMMddHHmmss";
        private const int RegionCount = 5;

        public DateTime LastUpdateTime { set; get; }
        public string DisplayUpdateTime { get; set; }
        public string Psi3Hour { get; set; }
        public string Psi24Hour { get; set; }

        public PsiInformation()
        {
            Initialize();
        }

        public static PsiInformation Parse(Channel info)
        {
            var result = new PsiInformation();
            if (info == null || info.Item == null || info.Item.Count < 1) return result;
            DateTime updateTime;
            if (!DateTime.TryParseExact(info.Item[0].Record.Timestamp, PsiDatetimeFormat,
                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out updateTime)) return result;
            var allRecords=(from item in info.Item from record in item.Record.PsiReadings select record);
            var psi3Hr = allRecords.Where(x => x.Type == "NPSI_PM25_3HR").Sum(x => int.Parse(x.Value)) / RegionCount;
            var psi24Hr = allRecords.Where(x => x.Type == "PM25_24HR").Sum(x => int.Parse(x.Value)) / RegionCount;

            result.LastUpdateTime = updateTime;
            result.DisplayUpdateTime = updateTime.Hour.ToString("D2") +":"+ updateTime.Minute.ToString("D2");
            result.Psi3Hour = psi3Hr.ToString();
            result.Psi24Hour = psi24Hr.ToString();
            return result;
        }

        private void Initialize()
        {
            LastUpdateTime = DateTime.MinValue;
            DisplayUpdateTime = DefaultEmptyInformation;
            Psi24Hour = DefaultEmptyInformation;
            Psi3Hour = DefaultEmptyInformation;
        }
    }
}