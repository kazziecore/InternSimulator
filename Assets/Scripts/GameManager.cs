using UnityEngine;
using System.Collections.Generic;

// Game Status Struct

    

// Create asset menu item called GameManager
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManager_SO", order = 1)]

public class GameDataSO : ScriptableObject
{
	public string playerName;
	public string playerPosition;
	public string npcPosition;
	public int score;
	public int highScore;
	public int totalRequestsComplete;
    public int totalRequestsFailed;
    public int activeRequests;

    public void ResetGame()
    {
        
        int savedHighScore = highScore;

        playerName = "Kazzie";
        score = 0;
        totalRequestsComplete = 0;
        totalRequestsFailed = 0;
        activeRequests = 1;

        highScore = savedHighScore;
    }
}

