using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PaintCube
{
    public sealed partial class ExamplePage : Page
    {
        private NavigationHelper navigationHelper;

        public ExamplePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                this.DataContext = new ExampleDefinition("An Example", null);
            }

            if (this.navigationHelper.HasHardwareButtons)
            {
                this.backButton.Visibility = Visibility.Collapsed;
            }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            if (e.Parameter is ExampleDefinition example)
            {
                this.DataContext = example;
                if (example.Control != null)
                {
                    var control = Activator.CreateInstance(example.Control);
                    this.exampleContent.Children.Add((UIElement)control);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
    }
}
