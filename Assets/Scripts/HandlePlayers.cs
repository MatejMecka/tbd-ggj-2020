using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayers : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentPlayerId = 0;
    public GameObject currentPlayer;
    private int numPlayers = 0;

    void Start()
    {
        // Create A Class for Each Player
        numPlayers = PlayerPrefs.GetInt("NumberOfPlayers", 0);
        print(numPlayers);
        for(var i=0; i < numPlayers; i++){
            GameObject player;
            player = new GameObject("Player"  + i.ToString());
            player.AddComponent<Player>();
        }

        currentPlayer = GameObject.Find("Player"  + currentPlayerId.ToString());

    }

    public void switchPlayer(){
        if(currentPlayerId > numPlayers){
            currentPlayerId = 0;
        } else{
            currentPlayerId++;
        }
        currentPlayer = GameObject.Find("Player"  + currentPlayerId.ToString());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
