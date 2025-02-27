using NBRBCurrencyTestTask.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class FileService
{
    private readonly string _filePath;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public void SaveRatesToJson(List<Rate> rates)
    {
        string json = JsonConvert.SerializeObject(rates, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public List<Rate> LoadRatesFromJson()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Rate>>(json);
        }
        return new List<Rate>();
    }
}