using Counter_BS.Models;

namespace Counter_BS;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = AllCounters.Instance;
    }

    async void AddCounterClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(Views.NewPage1));

    void OnPlusClicked(object sender, EventArgs e)
        => ((Counter)((Button)sender).BindingContext).Value++;

    void OnMinusClicked(object sender, EventArgs e)
        => ((Counter)((Button)sender).BindingContext).Value--;
    async void OnDeleteClicked(object sender, EventArgs e)
    {
        var counter = (Counter)((ImageButton)sender).BindingContext;

        bool confirmed = await DisplayAlert("Delete", $"Delete counter \"{counter.CounterName}\"?", "Yes", "No");
        if (confirmed)
            AllCounters.Instance.Counters.Remove(counter);
    }
}