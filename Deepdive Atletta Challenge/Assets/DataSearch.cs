using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DataSearch : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        string url = "https://apideepdive.24sdb.nl/average"; // voorbeeld-API
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
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}
