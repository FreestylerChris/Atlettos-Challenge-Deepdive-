using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataList : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField heightInput;
    public TMP_InputField weightInput;
    public TMP_InputField caloriesInput;
    public TMP_Text resultText;
    public Button submitButton;

    [Header("Data")]
    public DataSearch dataStorage; // Je DataSearch script dat de provincie selectie en actuele data beheert

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
    }

    public void OnSubmit()
    {
        // Haal de geselecteerde provincie op van DataSearch
        string provinceName = dataStorage.GetSelectedProvinceName();
        ProvinceStats stats = dataStorage.GetSelectedProvinceStats();

        if (string.IsNullOrEmpty(provinceName) || stats == null)
        {
            resultText.text = "Selecteer eerst een provincie!";
            return;
        }

        // Controleer invoer
        if (!float.TryParse(heightInput.text, out float userHeight) ||
            !float.TryParse(weightInput.text, out float userWeight) ||
            !float.TryParse(caloriesInput.text, out float userCalories))
        {
            resultText.text = "Voer geldige waarden in voor height, weight en calories!";
            return;
        }

        // Bereken procentueel verschil
        float heightPercent = ((userHeight - stats.avg_height) / stats.avg_height) * 100f;
        float weightPercent = ((userWeight - stats.avg_weight) / stats.avg_weight) * 100f;
        float caloriesPercent = ((userCalories - stats.avg_calories) / stats.avg_calories) * 100f;

        // Toon resultaat
        resultText.text =
            $"Provincie: {provinceName}\n" +
            $"Height: {heightPercent:+0.##;-0.##}% t.o.v. gemiddelde\n" +
            $"Weight: {weightPercent:+0.##;-0.##}% t.o.v. gemiddelde\n" +
            $"Calories: {caloriesPercent:+0.##;-0.##}% t.o.v. gemiddelde";
    }
}
