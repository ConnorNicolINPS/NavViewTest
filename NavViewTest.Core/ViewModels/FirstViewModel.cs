namespace NavViewTest.Core.ViewModels
{
    using System.Windows.Input;
    using MvvmCross.Core.ViewModels;

    /// <summary>
    /// The Vm for first view
    /// </summary>
    /// <seealso cref="MvvmCross.Core.ViewModels.MvxViewModel" />
    public class FirstViewModel : MvxViewModel
    {
        /// <summary>
        /// The loaded command
        /// </summary>
        private MvxCommand frameLoadedCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstViewModel"/> class.
        /// </summary>
        public FirstViewModel()
        {
        }

        /// <summary>
        /// Gets the loaded command.
        /// </summary>
        /// <value>
        /// The loaded command.
        /// </value>
        public ICommand FrameLoadedCommand
        {
            get { return this.frameLoadedCommand ?? (this.frameLoadedCommand = new MvxCommand(LoadedAction)); }
        }

        /// <summary>
        /// Loadeds the action.
        /// </summary>
        private void LoadedAction()
        {
            this.ShowViewModel<SecondViewModel>();
        }

        /// <summary>
        /// Shows the view based on the provided tag
        /// </summary>
        /// <param name="tag">The tag.</param>
        public void ShowView(string tag)
        {
            tag = tag.ToLower();
            switch (tag)
            {
                case "settings":
                    {
                        this.ShowViewModel<SettingsViewModel>();
                        break;
                    }
                case "apps":
                    {
                        this.ShowViewModel<AppsViewModel>();
                        break;
                    }
                case "games":
                    {
                        this.ShowViewModel<GamesViewModel>();
                        break;
                    }
                case "home":
                    {
                        this.ShowViewModel<SecondViewModel>();
                        break;
                    }
                case "content":
                    {
                        this.ShowViewModel<ContentViewModel>();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
