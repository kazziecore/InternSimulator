using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;



//game status data structure
[Serializable]
public struct SaveData
{
	public string playerName; // gameplay data
	public string playerPosition; //gameplay data
	public string npcPosition; //enviroment data
	public int score; //gameplay data
	public int highScore; //statistical data
	public int totalRequestsComplete; //gameplay data
    public int totalRequestsFailed; //gameplay data
    public int activeRequests; //gameplay data
}

public class GameData : MonoBehaviour
{

    [SerializeField] TMP_Text gameStatusUI;
    public Transform player;
    public NPCRequest npc;
	public SaveData gameStatus;
	string filePath;
	const string FILE_NAME = "SaveStatus.json";

	//building ui woww
	void ShowStatus ()
	{
        //updates live positions of da npc amd player
        gameStatus.playerPosition = player.position.ToString();
        gameStatus.npcPosition = npc.transform.position.ToString();

		// this formats the ui on screen to show data!
		string message = "";
		message += "Player Name: " + gameStatus.playerName + "\n";
		message += "Player Pos: " + gameStatus.playerPosition + "\n";
		message += "NPC Pos: " + gameStatus.npcPosition + "\n";
		message += "score: " + gameStatus.score + "\n";
		message += "High Score: " + gameStatus.highScore + "\n";
		message += "Completed: " + gameStatus.totalRequestsComplete + "\n";
        message += "Failed: " + gameStatus.totalRequestsFailed + "\n";
        message += "Total Requests: " + gameStatus.activeRequests + "\n";
        
        // this actually is what shows it lol
		gameStatusUI.text = message;
	}

	
	//this overrides the saving file
	public void SaveGameStatus ()
	{
        //this defaults the name of the player (meeeee)
       	gameStatus.playerName = "Kazzie";
       	

		// serialise the GameStatus struct into a Json string
		string gameStatusJson = JsonUtility.ToJson (gameStatus);

		//write a text file containing the string value as simple text
		File.WriteAllText (filePath + "/" + FILE_NAME, gameStatusJson);

		// Debug.Log ("File created and saved");
		// commented out due to console flooding but it works
	}

	//this function loads a saving file if found in the system
	public void LoadGameStatus ()
	{
		//always check the file exists
		if (File.Exists (filePath + "/" + FILE_NAME)) {

			

			//load the file content as string
			string loadedJson = File.ReadAllText (filePath + "/" + FILE_NAME);

			//deserialise the loaded string into a GameStatus struct
			gameStatus = JsonUtility.FromJson<SaveData> (loadedJson);

			Debug.Log ("File loaded successfully");
		} else {
			//if theres no file, it starts a new game status
			gameStatus.playerName = "Kazzie";
		
       		gameStatus.playerPosition = player.position.ToString();
       		gameStatus.npcPosition = npc.transform.position.ToString();
        
			gameStatus.score = 0;
			gameStatus.highScore = 0;
			gameStatus.totalRequestsComplete = 0;
			gameStatus.totalRequestsFailed = 0;
			gameStatus.activeRequests = 1;
			

			Debug.Log ("File not found");

			//saves it if theres nothing there 
			SaveGameStatus();

			
		}
	}

	// resetting the game for new scores, and to display high score
	// game lifecycle event
	public void ResetGame()
	{
		Debug.Log("Resetting");

	// keep high score as saved data in the game
		int savedHighScore = gameStatus.highScore;

		gameStatus = new SaveData();
		gameStatus.playerName = "Kazzie";
		gameStatus.highScore = savedHighScore;

		SaveGameStatus();

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// lifecycle application event, this retreives and initialises all the data from the file
	void Start ()
	{
		//retrieving saving location
		filePath = Application.persistentDataPath;
		gameStatus = new SaveData ();
		Debug.Log (filePath);
		//startup initialisation
		LoadGameStatus ();
	}

	// another application lifecycle ! this is so the json file updates consistently. it also saves unless the game is reset.
	void Update ()
	{
		ShowStatus ();

		SaveGameStatus(); // auto saves 

		if (Input.GetKeyDown(KeyCode.R))
		{
			ResetGame();
		}
	}

	//pauses when minimised/tabbed out woaow LIFESTYLE APPLICATION!
	void OnApplicationPause(bool pauseStatus)
{
    if (pauseStatus)
    {
        SaveGameStatus();
		Debug.Log("Game paused");
    }
    else
    {
        LoadGameStatus();
		Debug.Log("Game resumed");
    }
}

}
