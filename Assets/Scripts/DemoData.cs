using UnityEngine;

[CreateAssetMenu(fileName = "DemoData, meenuName = ScriptableObjects/DemoData")]

public class DemoData : ScriptableObject
{
    public string playerName;
    public int score;
    public int totalRequestsComplete;
    public int totalRequestsFailed;
    public int activeRequests;
}
