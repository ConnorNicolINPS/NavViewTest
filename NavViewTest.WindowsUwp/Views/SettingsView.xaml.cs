namespace NavViewTest.WindowsUwp.Views
{
    using MvvmCross.WindowsUWP.Views;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegion("FrameContent")]
    public sealed partial class SettingsView : MvxWindowsPage
    {
        public SettingsView()
        {
            this.InitializeComponent();
        }
    }
}
