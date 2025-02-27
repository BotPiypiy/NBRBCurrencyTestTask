using NBRBCurrencyTestTask.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Rate>> GetRatesAsync(DateTime onDate)
    {
        string formattedDate = onDate.ToString("yyyy-MM-dd");
        string apiUrl = $"https://api.nbrb.by/exrates/rates?ondate={formattedDate}&periodicity=0";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            List<Rate> rates = JsonConvert.DeserializeObject<List<Rate>>(json);
            return rates;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network request error: {ex.Message}");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserealization error: {ex.Message}");
            return null;
        }
    }
}