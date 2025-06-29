using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkLoader
{
    public async Task<string> GetData(string url)
    {
        try
        {
            HttpClient client = new HttpClient();
            string responseData = await client.GetStringAsync(url);
            return responseData;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Ошибка подключения!", true, true);
            Console.WriteLine($"Message : {e.Message}");
            return null;
        }
    }
}
