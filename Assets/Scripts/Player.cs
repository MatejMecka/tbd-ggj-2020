using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    //public List<int> acccuracy;
    public float time=0;
    public int corrects;
    public int wrongs;
    public bool guessesEnabled;

    void Start()
    {
     //print()   
    }

    // Update is called once per frame
    void Update()
    {
        if(guessesEnabled){
            time += Time.deltaTime;
        }

    }

    public void updatePlayerData(int correctTracker, int wrongAnswers){
        corrects += correctTracker;
        wrongs += wrongAnswers;
    }
}
