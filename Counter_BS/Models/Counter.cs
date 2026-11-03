using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Counter_BS.Models;

public class Counter : INotifyPropertyChanged
{
    int value;

    public string CounterName { get; set; } = "";

    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    [JsonIgnore] public string EditorPlaceholderText => "Set name of the counter";
    [JsonIgnore] public string SaveButtonText => "Save";
    [JsonIgnore] public string CancelButtonText => "Cancel";

    public event PropertyChangedEventHandler? PropertyChanged;
}