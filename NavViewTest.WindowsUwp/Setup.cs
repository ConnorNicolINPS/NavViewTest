namespace NavViewTest.WindowsUwp
{
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Platform;
    using MvvmCross.WindowsUWP.Platform;
    using MvvmCross.WindowsUWP.Views;
    using NavViewTest.Core.Services.Interfaces;
    using NavViewTest.WindowsUwp.Services;
    using Windows.UI.Xaml.Controls;

    class Setup : MvxWindowsSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        /// <summary>
        /// Creates the application.
        /// </summary>
        /// <returns></returns>
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        /// <summary>
        /// Initializes the first chance.
        /// </summary>
        protected override void InitializeFirstChance()
        {
            Mvx.ConstructAndRegisterSingleton<ISettings, WinSettings>();
            Mvx.ConstructAndRegisterSingleton<IDevice, WinDevice>();
        }

        /// <summary>
        /// Initializes the platform services.
        /// </summary>
        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();
        }

        /// <summary>
        /// Creates the view presenter.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        /// <returns>mvx view presenter</returns>
        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            return new MvxWindowsMultiRegionViewPresenter(rootFrame);
        }
    }
}