using Xamarin.Forms;

namespace lembrete
{
	public partial class App : Application
	{

		public static IAlertService _alertService;
		public static INavigationService _navigationService;

		public App()
		{
			InitializeComponent();

			DependencyService.Register<INavigationService, NavigationService>();
			DependencyService.Register<IAlertService, AlertService>();

			_alertService = DependencyService.Get<IAlertService>();
			_navigationService = DependencyService.Get<INavigationService>();


			MainPage = new NavigationPage(new ListaLembretesView());

			var navigationPage = Application.Current.MainPage as NavigationPage;
			navigationPage.BarBackgroundColor = Color.FromHex("#E0E0E0");
			navigationPage.BarTextColor = Color.FromHex("#000");
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
