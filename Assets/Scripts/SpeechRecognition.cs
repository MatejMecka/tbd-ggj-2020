﻿using UnityEngine.Windows.Speech;
using UnityEngine;
public class SpeechRecognition : MonoBehaviour
{
	private SentenceBehaviour sb;
	public string[] keywords = new string[] { };
	public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    protected PhraseRecognizer recognizer;
    protected string word;

    private void Start()
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

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
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
		print(word);
	}
}