using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json;

namespace Counter_BS.Models;

public class AllCounters
{
    static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "counters.json");

    public static AllCounters Instance { get; } = new();

    public ObservableCollection<Counter> Counters { get; } = new();

    public string AddCounterButton => "Add";

    AllCounters()
    {
        Load();
        foreach (var c in Counters)
            c.PropertyChanged += OnCounterChanged;

        Counters.CollectionChanged += OnCollectionChanged;
    }

    void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (Counter c in e.NewItems)
                c.PropertyChanged += OnCounterChanged;

        if (e.OldItems != null)
            foreach (Counter c in e.OldItems)
                c.PropertyChanged -= OnCounterChanged;

        Save();
    }

    void OnCounterChanged(object? sender, PropertyChangedEventArgs e) => Save();

    void Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return;

            var saved = JsonSerializer.Deserialize<List<Counter>>(File.ReadAllText(FilePath));
            if (saved == null) return;

            foreach (var c in saved)
                Counters.Add(c);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Load failed: {ex.Message}");
        }
    }

    void Save()
    {
        try
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(Counters));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Save failed: {ex.Message}");
        }
    }
}