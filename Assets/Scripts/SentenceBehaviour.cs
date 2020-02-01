using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SentenceBehaviour : MonoBehaviour
{
	// Start is called before the first frame update
	private SpeechRecognition srec;
	public string Sentence = "";
    public string[] keywords = new string[] {};
    public List<string> keywordsShuffled = new List<string>();
    public Text sentence;
    public string fileName;
    private string[] lines = new string[]{};
    public Player player;
    private int level = 0;
    private int cycles = 0;
    private List<int> sentencesUsed = new List<int>();

    //public StreamReader reader = new StreamReader(path);

    void Start(){
        var sr = new StreamReader(Application.dataPath + "/" + fileName);
        var fileContents = sr.ReadToEnd();
        sr.Close();

        lines = fileContents.Split("\n"[0]);
        getNewSentence(level+3);

		srec = gameObject.GetComponent<SpeechRecognition>();
		srec.loadKeywords();
    }   

    // Update is called once per frame
    void Update(){
		// print("test");
    }

    void getNewSentence(int round){
        Sentence = lines[Random.Range(0, lines.Length)];
        splitSentence(Sentence);

        // Check if Sentence equals
        if(keywords.Length != round){
            getNewSentence(round);
        }

        for(int i=0; i < keywordsShuffled.Count; i++){
             keywordsShuffled[i] = null;
        }

        // Add Keywords
        for(int i=0; i < keywords.Length; i++){
            keywordsShuffled.Add(keywords[i]);
        }

        shuffleKeywords();
        sentence.text = string.Join(" ", keywordsShuffled.ToArray());
    }


    void splitSentence(string sentence){
        // Create Array of Words for Speech Recognizer
        // Nullify Existing Elements
        for(int i=0; i < keywords.Length; i++){
            keywords[i] = null;
        }
        keywords = sentence.Split(' ');
    }

    void shuffleKeywords(){
        // Shuffle Words to show to User
        for(int i=0; i < keywordsShuffled.Count; i++){
            int rnd = Random.Range(0, keywordsShuffled.Count);
            string temp = keywordsShuffled[i];
            keywordsShuffled[i] = keywordsShuffled[rnd];
            keywordsShuffled[rnd] = temp;
        }
        
        // Verify it is Randomized
        for(int i=0; i < keywords.Length; i++){
            if(keywords[i]==keywordsShuffled[i]){
                shuffleKeywords();
                break;
            }
        }

        /* 
        if(keywords == keywordsShuffled.ToArray()){
            for(int i=0; i < keywordsShuffled.Count; i++){
                keywordsShuffled[i] = null;
            }
            shuffleKeywords();
        }*/
    }

    void validateSentence(){
        ;
        /*
            GameObject player = HandlePlayers.currentPlayer
            cycles++
            if(keywords.Length == guesses.Count &6 keywords == guesses.Count){
                // Increase Scores
               // player.score++;
                player.corrects++;
            } else {
                player.wrongs++;
            }
            if(cycles == 5){
                level++;
                cycles = 0;
            }
            HandlePlayers.currentPlayerId++;
            GameObject.currentPlayer = GameObject.Find("Player"  + currentPlayerId.ToString());
            getNewSentence(level);
         */
    }


}
