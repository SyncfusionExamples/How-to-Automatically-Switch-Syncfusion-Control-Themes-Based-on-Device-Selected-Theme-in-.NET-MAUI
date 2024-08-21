This article explains how to automatically switch [.NET MAUI Syncfusion control](https://www.syncfusion.com/maui-controls) themes based on the device-selected theme. This can be achieved by using [SyncfusionThemeResourceDictionary](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Themes.SyncfusionThemeResourceDictionary.html).

To enable automatic theme switching for Syncfusion controls based on the device's selected theme in a .NET MAUI application, you can utilize the `OnAppearing` method to assign the Syncfusion [VisualTheme](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Themes.SyncfusionThemeResourceDictionary.html#Syncfusion_Maui_Themes_SyncfusionThemeResourceDictionary_VisualTheme). Additionally, handling the `RequestedThemeChanged` event allows for dynamic updates to the Syncfusion controls' theme when the device's theme changes at runtime.

**App.xaml Configuration**

Ensure that your App.xaml includes the `SyncfusionThemeResourceDictionary`:

```
<Application ...             
             xmlns:themes="clr-namespace:Syncfusion.Maui.Themes;assembly=Syncfusion.Maui.Core">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <themes:SyncfusionThemeResourceDictionary />
                ...
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

**XAML**

```
//Mainpage.xaml

<VerticalStackLayout Padding="30,0" Spacing="25">         
    <buttons:SfButton x:Name="CounterBtn" Text="Click me" Clicked="OnCounterClicked"
                      HorizontalOptions="Fill" />
</VerticalStackLayout>
```

**C#**

1. Override the `OnAppearing` method to apply the current theme and set up an event handler for theme changes.
2. Implement the `OnRequestedThemeChanged` event handler to respond to theme changes during runtime.
3. Define the `ApplyTheme` method to update the Syncfusion theme based on the current application theme.

```csharp
//Mainpage.xaml.cs

protected override void OnAppearing()
{
    if (Application.Current != null)
    {
        this.ApplyTheme(Application.Current.RequestedTheme);
        Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
    }
    base.OnAppearing();
}

private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
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
            var syncTheme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();
            if (syncTheme != null)
            {
                if (appTheme is AppTheme.Light)
                {
                    syncTheme.VisualTheme = SfVisuals.MaterialLight;
                }
                else
                {
                    syncTheme.VisualTheme = SfVisuals.MaterialDark;
                }
            }
        }
    }
}
```

**Output**

![Theme_Demo.gif](https://support.bolddesk.com/kb/agent/attachment/article/17196/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI4NDcxIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5ib2xkZGVzay5jb20ifQ.YNDjr4zoII7A6wHES18wM0xTiidGYFkQ_t7WmRjcdWg)