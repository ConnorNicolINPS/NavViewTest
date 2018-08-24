namespace NavViewTest.Core
{
    using MvvmCross.Core.ViewModels;
    using NavViewTest.Core.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<FirstViewModel>();
        }
    }
}
