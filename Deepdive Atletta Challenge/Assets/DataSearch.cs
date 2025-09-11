using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System;

[Serializable]
public class ProvinceStats
{
    public float avg_age;
    public float avg_bmi;
    public float avg_calories;
    public float avg_height;
    public float avg_income;
    public float avg_weight;
    public int cities_in_province;
    public int total_records;
}

[Serializable]
public class AveragesPerProvinceWrapper
{
    public Dictionary<string, ProvinceStats> averages_per_province;
}

public class DataSearch : MonoBehaviour
{
    public GameObject gr;
    public GameObject dr;
    public GameObject fr;

    public TMP_Text groningenText; // Sleep hier Groningen TMP in Inspector

    public string SelectedProvince { get; private set; }
    private ProvinceStats selectedStats;

    public string GetSelectedProvinceName()
    {
        return SelectedProvince;
    }

    public ProvinceStats GetSelectedProvinceStats()
    {
        return selectedStats;
    }
    private void Start()
    {
        StartCoroutine(UpdateDataLoop());
    }

    IEnumerator UpdateDataLoop()
    {
        while (true)
        {
            yield return GetData();
            yield return new WaitForSeconds(0.1f); 
        }
    }
    private void UpdateSelectedProvince(string provinceName, ProvinceStats stats)
    {
        SelectedProvince = provinceName;
        selectedStats = stats;
    }

    IEnumerator GetData()
    {

        string url = "https://apideepdive.24sdb.nl/average";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<AveragesPerProvinceWrapper>(json);


                if (gr.GetComponent<ClickableObject>().Switch > 0)
                {
                    SelectedProvince = "Groningen";
                    SetProvinceText("Groningen", data, groningenText);
                }

                if (dr.GetComponent<ClickableObject>().Switch > 0)
                {
                    SelectedProvince = "Drenthe";
                    SetProvinceText("Drenthe", data, groningenText);
                }

                if (fr.GetComponent<ClickableObject>().Switch > 0)
                {
                    SelectedProvince = "Fryslân";
                    SetProvinceText("Fryslân", data, groningenText);
                }


            }
        }

    }
    private void SetProvinceText(string provinceName, AveragesPerProvinceWrapper data, TMP_Text tmpText)
    {
        if (data.averages_per_province.TryGetValue(provinceName, out ProvinceStats stats))
        {
            // Update geselecteerde provincie voor DataList
            UpdateSelectedProvince(provinceName, stats);

            tmpText.text = $"{provinceName}\n" +
                           $"Avg Age: {stats.avg_age}\n" +
                           $"Avg BMI: {stats.avg_bmi}\n" +
                           $"Avg Calories: {stats.avg_calories}\n" +
                           $"Avg Height: {stats.avg_height}\n" +
                           $"Avg Income: {stats.avg_income}\n" +
                           $"Avg Weight: {stats.avg_weight}\n" +
                           $"Cities: {stats.cities_in_province}\n" +
                           $"Records: {stats.total_records}";
        }
        else
        {
            tmpText.text = $"{provinceName} data niet gevonden";
        }
    }

}
