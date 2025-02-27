using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NBRBCurrencyTestTask.Model;
using MyToolkit.Command;


public class MainWindowVM : INotifyPropertyChanged
{
    private readonly ApiService _apiService;
    private readonly FileService _fileService;
    private ObservableCollection<Rate> _rates;
    private DateTime _selectedDate;

    public MainWindowVM()
    {
        _apiService = new ApiService();
        _fileService = new FileService("Rates.json");
        _selectedDate = DateTime.Today;
        UploadRatesCommand = new AsyncRelayCommand(async () => await UploadRates());
        SaveRatesCommand = new RelayCommand(SaveRates);
        UpdateRateCommand = new RelayCommand(UpdateRates);
        Rates = new ObservableCollection<Rate>(_fileService.LoadRatesFromJson());
    }

    public ObservableCollection<Rate> Rates
    {
        get { return _rates; }
        set { _rates = value; OnPropertyChanged(nameof(Rates)); }
    }

    public DateTime SelectedDate
    {
        get { return _selectedDate; }
        set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
    }

    public ICommand UploadRatesCommand { get; }
    public ICommand SaveRatesCommand { get; }
    public ICommand UpdateRateCommand { get; }

    private async Task UploadRates()
    {
        List<Rate> rates = await _apiService.GetRatesAsync(SelectedDate);
        if (rates != null)
        {
            Rates = new ObservableCollection<Rate>(rates);
        }
    }

    private void SaveRates()
    {
        _fileService.SaveRatesToJson(Rates.ToList());
    }

    /// <summary>
    /// "Update" doing the same save operation as "Save"
    /// No reason to temporary save changed fields and then update only them in file
    /// It will be more complex, slower, and take more memory
    /// </summary>
    private void UpdateRates()
    {
        SaveRates();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}