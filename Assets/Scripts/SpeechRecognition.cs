using UnityEngine.Windows.Speech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public AudioSource audioData;
	public AudioSource audioDataOther;
	public AudioClip otherClip;
    public AudioClip OriginalClip;
	public int round = 0;

	private void Start()
    {
        GetComponent<HandlePlayers>().orderPlayersByRank();
		vh = gameObject.GetComponent<VisualsHandler>();

		HandlePlayers hp = GetComponent<HandlePlayers>();
		round = hp.round;
		int friendlyRound = round + 1;
		roundText.text = "ROUND: " + friendlyRound.ToString();

        audioData = GetComponent<AudioSource>();
	}

    void generateNewRound(){

		HandlePlayers hp = GetComponent<HandlePlayers>();
		round = hp.round;
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
			if (round == 3)
			{
				HandlePlayers hp = GetComponent<HandlePlayers>();
				hp.orderPlayersByRank();
				if (hp.playersPositions.ElementAtOrDefault(0) != null){
					GameObject winner = hp.playersPositions[0];
					PlayerPrefs.SetString("Winner", winner.name);
				}
				if (hp.playersPositions.ElementAtOrDefault(1) != null){
					GameObject runnerup = hp.playersPositions[1];
					PlayerPrefs.SetString("RunnerUp", runnerup.name);
				}
				if (hp.playersPositions.ElementAtOrDefault(2) != null){
					GameObject finalist = hp.playersPositions[2];
					PlayerPrefs.SetString("Finalist", finalist.name);
				}

				SceneManager.LoadScene("Score", LoadSceneMode.Single);
			}

			bool answer = sb.validateWord(word, counter);
			print(answer);
            previousWord = word;
            counter++;
            if(!answer){
                // Handle Here Wrong Sentence
                print("You fucking Donkey!");
				audioDataOther.Play(0);
				wrongTracker =true;
                generateNewRound();
				loadKeywords();
                
			}
			else
			{
				vh.colorWord(word);
                audioData.Play(0);
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
