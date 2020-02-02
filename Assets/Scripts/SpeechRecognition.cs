using UnityEngine.Windows.Speech;
using UnityEngine;
using UnityEngine.UI;

public class SpeechRecognition : MonoBehaviour
{
	private SentenceBehaviour sb;
	private VisualsHandler vh;
	public string[] keywords = new string[] { };
	public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    protected PhraseRecognizer recognizer;
    protected string word;
    private string previousWord;
    private int counter=0;
    private int correctTracker = 0;
    private bool wrongTracker = false;
	public Text roundText;


	private void Start()
    {
        GetComponent<HandlePlayers>().orderPlayersByRank();
		vh = gameObject.GetComponent<VisualsHandler>();

		HandlePlayers hp = GetComponent<HandlePlayers>();
		int round = hp.round;
		int friendlyRound = round + 1;
		roundText.text = "ROUND: " + friendlyRound.ToString();
	}

    void generateNewRound(){

		HandlePlayers hp = GetComponent<HandlePlayers>();
		int round = hp.round;
		int friendlyRound = round + 1;
		roundText.text = "ROUND: " + friendlyRound.ToString();


		GameObject player = GetComponent<HandlePlayers>().getCurrentPlayer();
		//print("Player" + hp.currentPlayerId.ToString());
		player.GetComponent<Player>().updatePlayerData(wrongTracker);
        wrongTracker = false;
        counter = 0;

        // Generate New Sentence
        GetComponent<SentenceBehaviour>().getNewSentence(round+3);
		GetComponent<HandlePlayers>().switchPlayer();

    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        if(!string.IsNullOrEmpty(word) && word != previousWord){
            bool answer = sb.validateWord(word, counter);
			print(answer);
            previousWord = word;
            counter++;
            if(!answer){
                // Handle Here Wrong Sentence
                print("You fucking Donkey!");
                wrongTracker=true;
                generateNewRound();
				loadKeywords();
			}
			else
			{
				vh.colorWord(word);
			}
        }
        if(counter == keywords.Length){
            generateNewRound();
			loadKeywords();
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
