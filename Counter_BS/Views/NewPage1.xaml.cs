using Counter_BS.Models;

namespace Counter_BS.Views;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
    {
        InitializeComponent();
        BindingContext = new Counter();
    }

    async void SaveButtonClicked(object sender, EventArgs e)
    {
        var counter = (Counter)BindingContext;
        if (string.IsNullOrWhiteSpace(counter.CounterName)) return;

        if (!string.IsNullOrWhiteSpace(InitialValueEntry.Text))
        {
            if (!int.TryParse(InitialValueEntry.Text, out int initial))
            {
                await DisplayAlert("Error", "value must be an integer.", "OK");
                return;
            }
            counter.Value = initial;
        }

        AllCounters.Instance.Counters.Add(counter);
        await Shell.Current.GoToAsync("..");
    }

    async void CancelButtonClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}