using Syncfusion.Maui.Themes;

namespace ThemeSample
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            
        }

        protected override void OnAppearing()
        {
            if (Application.Current != null)
            {
                this.ApplyTheme(Application.Current.RequestedTheme);
                Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            }
            base.OnAppearing();
        }

        private void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            this.ApplyTheme(e.RequestedTheme);
        }

        public void ApplyTheme(AppTheme appTheme)
        {
            if (Application.Current != null)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    var theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();
                    if (theme != null)
                    {
                        if (appTheme is AppTheme.Light)
                        {
                            theme.VisualTheme = SfVisuals.MaterialLight;
                        }
                        else
                        {
                            theme.VisualTheme = SfVisuals.MaterialDark;
                        }
                    }
                }
            }
        }
    }

}
