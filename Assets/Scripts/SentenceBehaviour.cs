using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private List<string> sentencesUsed = new List<string>();

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

    public void getNewSentence(int round){
        Sentence = lines[Random.Range(0, lines.Length)];
        splitSentence(Sentence);

        // Check if Sentence equals
        if(keywords.Length != round && sentencesUsed.Contains(Sentence)){
            getNewSentence(round);
        }

        sentencesUsed.Add(Sentence);

        keywordsShuffled.Clear();

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

	void shuffleKeywords()
	{
		// Shuffle Words to show to User
		for (int i = 0; i < keywordsShuffled.Count; i++)
		{
			int rnd = Random.Range(0, keywordsShuffled.Count);
			string temp = keywordsShuffled[i];
			keywordsShuffled[i] = keywordsShuffled[rnd];
			keywordsShuffled[rnd] = temp;
		}

		// Verify it is Randomized
		for (int i = 0; i < keywords.Length; i++)
		{
			if (keywords[i] == keywordsShuffled[i])
			{
				shuffleKeywords();
				break;
			}
		}
	}

	public bool validateWord(string word, int counter){
		if (counter == keywords.Length - 1)
		{
			return true;
		}
		return keywords[counter] == word;
	}


}
