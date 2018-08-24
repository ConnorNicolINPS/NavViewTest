namespace NavViewTest.Core.ViewModels
{
    using System;
    using MvvmCross.Core.ViewModels;

    /// <summary>
    /// the view model for the settings view 
    /// </summary>
    /// <seealso cref="MvvmCross.Core.ViewModels.MvxViewModel" />
    public class SettingsViewModel : MvxViewModel
    {

        /// <summary>
        /// The personal settings command
        /// </summary>
        private MvxCommand personalSettingsCommand;

        /// <summary>
        /// The general settings command
        /// </summary>
        private MvxCommand generalSettingsCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel()
        {
        }

        /// <summary>
        /// Gets the personal settings command.
        /// </summary>
        /// <value>
        /// The personal settings command.
        /// </value>
        public MvxCommand PersonalSettingsCommand
        {
            get { return this.personalSettingsCommand ?? (this.personalSettingsCommand = new MvxCommand(PersonalSettingsAction)); }
        }

        /// <summary>
        /// Gets the general settings command.
        /// </summary>
        /// <value>
        /// The general settings command.
        /// </value>
        public MvxCommand GeneralSettingsCommand
        {
            get { return this.generalSettingsCommand ?? (this.generalSettingsCommand = new MvxCommand(GeneralSettingsAction)); }
        }

        /// <summary>
        /// Personals the settings action.
        /// </summary>
        private void PersonalSettingsAction()
        {
            this.ShowViewModel<PersonalSettingsViewModel>();
        }

        /// <summary>
        /// Generals the settings action.
        /// </summary>
        private void GeneralSettingsAction()
        {
            this.ShowViewModel<GeneralSettingsViewModel>();
        }
    }
}
