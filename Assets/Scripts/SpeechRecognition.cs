using UnityEngine.Windows.Speech;
using UnityEngine;

public class SpeechRecognition : MonoBehaviour
{
	private SentenceBehaviour sb;
	public string[] keywords = new string[] { };
	public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    protected PhraseRecognizer recognizer;
    protected string word;
    private string previousWord;
    private int counter=0;
    private int correctTracker = 0;


    private void Start()
    {
		
	}

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        if(!string.IsNullOrEmpty(word) && word != previousWord){
            bool answer = sb.validateWord(word, counter);
            previousWord = word;
            counter++;
            if(answer){
                correctTracker++;
            }
        }
        if(counter == keywords.Length){
            // Set the Player Score
            GetComponent<Player>().updatePlayerData(correctTracker, keywords.Length - correctTracker);
            
            correctTracker = 0;

            // Generate New Sentence
            GetComponent<SentenceBehaviour>().getNewSentence(2);

            // Switch to the next player
            GetComponent<HandlePlayers>().switchPlayer();

        }
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }

    }

	private void Update()
	{

	}
	
	public void loadKeywords()
	{
		sb = gameObject.GetComponent<SentenceBehaviour>();
		keywords = sb.keywords;

		if (keywords != null)
		{
			recognizer = new KeywordRecognizer(keywords, confidence);
			recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
			recognizer.Start();
		}
	}
}
