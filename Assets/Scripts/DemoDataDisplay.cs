using UnityEngine;
using TMPro;

public class DemoDataDisplay : MonoBehaviour
{
    public DemoData data;
    public TMP_Text text;

    void Update()
    {
        text.text =
            "Player Name: " + data.playerName + "\n" +
            "Score: " + data.score + "\n\n" +
            "Completed: " + data.totalRequestsComplete + "\n" +
            "Failed: " + data.totalRequestsFailed + "\n" +
            "Total Requests: " + data.activeRequests;
            

    }
}
