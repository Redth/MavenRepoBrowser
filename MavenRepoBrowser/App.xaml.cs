using System.Threading.Tasks;
using Xamarin.Forms;

namespace MavenRepoBrowser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoadingPage(() =>
                Device.BeginInvokeOnMainThread(LoadMain));
        }

        public bool IsDesktop { get; set; }

        void LoadMain()
        {
            if (IsDesktop)
            {
				var nav = new NavigationPage(new Page() { Title = "Maven Repo Browser"});
                var md = new MasterDetailPage();
                md.MasterBehavior = MasterBehavior.Split;
                md.Detail = nav;
                md.Master = new GroupListPage(nav.Navigation);

                MainPage = md;
            } else {
                MainPage = new NavigationPage(new GroupListPage(null));
            }
        }

        public void CloseDrawer()
        {
            var app = App.Instance;

            if (app != null && app.IsDesktop)
            {
                var mp = app.MainPage as MasterDetailPage;
                if (mp != null && mp.MasterBehavior != MasterBehavior.Split)
                    mp.IsPresented = false;
            }
        }

        public static App Instance {
            get {
                return Application.Current as App;
            }
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
