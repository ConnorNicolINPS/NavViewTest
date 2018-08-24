
namespace NavViewTest.WindowsUwp.Views
{
    using MvvmCross.WindowsUWP.Views;
    using NavViewTest.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.Foundation;
    using Windows.System;
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FirstView : MvxWindowsPage
    {
        /// <summary>
        /// The current
        /// </summary>
        public static FirstView Current = null;

        /// <summary>
        /// Gets the application frame.
        /// </summary>
        public Frame AppFrame { get { return (Frame)this.WrappedFrame.UnderlyingControl; } }

        /// <summary>
        /// Gets the vm.
        /// </summary>
        public FirstViewModel Vm => (FirstViewModel)ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstView"/> class.
        /// </summary>
        public FirstView()
        {
            //make full screen
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;

            ApplicationView.PreferredLaunchViewSize = new Size(bounds.Width, bounds.Height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            //init component
            this.InitializeComponent();

            //set current view
            this.Loaded += (sender, args) =>
            {
                Current = this;
            };

            //event handler for back request
            SystemNavigationManager.GetForCurrentView().BackRequested += FirstView_BackRequested;

            //method to calculate if there is a view to go back to 
            this.CalculateCanGoBack();
        }

        /// <summary>
        /// the currently avaliable view that are part of the NavViews navigation 
        /// does not include the settings as this is handled by default
        /// </summary>
        private readonly IList<(string Tag, Type Page)> pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(SecondView)),
            ("apps", typeof(AppsView)), 
            ("games", typeof(GamesView))
        };

        /// <summary>
        /// Handles the BackRequested event of the FirstView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BackRequestedEventArgs"/> instance containing the event data.</param>
        private void FirstView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // if the event is not already handled and there is a view  to go back too
            if (!e.Handled && this.AppFrame.CanGoBack)
            {
                e.Handled = true; // handle the event
                this.AppFrame.GoBack(); // defualt go back method
            }
        }

        /// <summary>
        /// invoked when a user makes a selction from the nav view menu
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NavigationViewItemInvokedEventArgs"/> instance containing the event data.</param>
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // get the tag of the view being navigated to
            var tag = string.Empty;

            // the settings is set by default and does not have a tag therefore...
            if (args.IsSettingsInvoked)
            {
                //if it is the settings give it a tag
                tag = "settings"; 
            }
            else
            {
                // otherwise get the tag from the invoked item
                tag = NavView.MenuItems.OfType<NavigationViewItem>().First(i => args.InvokedItem.Equals(i.Content)).Tag.ToString();
            }

            // go to view model to show the navigated view
            this.Vm.ShowView(tag);
        }

        /// <summary>
        /// Handles the Loaded event of the NavView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // add another item to the list in code
            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItem
            {
                Content = "My content",
                Icon = new SymbolIcon(Symbol.Folder),
                Tag = "content"
            });
            pages.Add(("content", typeof(ContentView))); // add view to pages list too 

            // stop the pane from ebing open by default
            this.NavView.IsPaneOpen = false;

            // event handler for all navigation calls (forward and back)
            FrameContent.Navigated += On_Navigated;

            // Add keyboard accelerators for backwards navigation
            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);

            // ALT routes here
            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            if (FrameContent.BackStack.Count > 0)
            {
                FrameContent.GoBack();
                args.Handled = true;
            }
        }

        /// <summary>
        /// back button navigation 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NavigationViewBackRequestedEventArgs"/> instance containing the event data.</param>
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            // go back one view in the frames backstack 
            FrameContent.GoBack();
        }


        /// <summary>
        /// Called when [navigated].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            // ** the selected item (little blue bar next to items in list) is not set by default when going back 
            // this method fixes that 

            // check to see if there is a view for the user to go back to 
            this.CalculateCanGoBack();
            if (FrameContent.SourcePageType == typeof(SettingsView))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
            }
            else
            {
                // if a non-settigns view get the item from the list
                var item = pages.FirstOrDefault(p => p.Page == e.SourcePageType);
                if(!string.IsNullOrWhiteSpace(item.Tag))
                {
                     // set the selected item 
                    NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals(item.Tag));
                }
            }
        }

        /// <summary>
        /// Calculates if the user can go back.
        /// </summary>
        private void CalculateCanGoBack()
        {
            // if there are views in the back stack
            if (FrameContent.BackStack != null && FrameContent.BackStack.Count > 0)
            {
                this.NavView.IsBackEnabled = true; // back button enabled
                return;
            }
            this.NavView.IsBackEnabled = false; // back button is disabled 
        }
    }
}
