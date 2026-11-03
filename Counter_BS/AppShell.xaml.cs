namespace Counter_BS;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.NewPage1), typeof(Views.NewPage1));

    }
}