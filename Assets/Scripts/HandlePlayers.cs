using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayers : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentPlayerId = 0;
    public GameObject currentPlayer;

    void Start()
    {
        // Create A Class for Each Player
        int numPlayers =  PlayerPrefs.GetInt("NumberOfPlayers", 0);
        print(numPlayers);
        for(var i=0; i < numPlayers; i++){
            GameObject player;
            player = new GameObject("Player"  + i.ToString());
            player.AddComponent<Player>();
        }

        currentPlayer = GameObject.Find("Player"  + currentPlayerId.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
