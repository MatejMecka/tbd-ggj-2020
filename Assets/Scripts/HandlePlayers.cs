using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayers : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentPlayerId = 0;
    public GameObject currentPlayer;
    private int numPlayers = 0;
    public List<GameObject> playersPositions = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
	public int round = 0;

    void Start()
    {
        // Create A Class for Each Player
        numPlayers = PlayerPrefs.GetInt("NumberOfPlayers", 0);
        print(numPlayers);
        for(var i=0; i < numPlayers; i++){
            GameObject player;
            player = new GameObject("Player"  + i.ToString());
            player.AddComponent<Player>();
            players.Add(player);
        }

        currentPlayer = GameObject.Find("Player"  + currentPlayerId.ToString());
        playersPositions = players;
        orderPlayersByRank();
    }

    public void switchPlayer(){
        if(currentPlayerId > numPlayers){
            currentPlayerId = 0;
			round++;
        } else{
            currentPlayerId++;
        }
        currentPlayer = GameObject.Find("Player"  + currentPlayerId.ToString());
    }

    public GameObject getCurrentPlayer(){
        return GameObject.Find("Player"  + currentPlayerId.ToString());
    }

    public void orderPlayersByRank(){
        for(int i=0; i < playersPositions.Count; i++){
            // Get Player One's score

            GameObject playerFirst = playersPositions[i];
            int playerFirstScore = playerFirst.GetComponent<Player>().corrects;

            for(int j=i+1; j < playersPositions.Count-1; j++){

                // Get Player Two's score
                GameObject playerSecond = playersPositions[j];
                int playerSecondScore = playerSecond.GetComponent<Player>().corrects;

                // If you didn't get this by now it's Bubble Sort :p
                // I'm sorry I failed you Bidik
                if(playerFirstScore > playerSecondScore){
                    GameObject temp = playerFirst;
                    playersPositions[i] = playerSecond;
                    playersPositions[j] = temp;
                }
            }
        }
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
