using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VisualsHandler : MonoBehaviour
{

	public string[] shuffledKeywords;
	public Text tmpword;
	public Text[] words; 
	public Canvas canvas;
	
	private SentenceBehaviour sb;

    // Start is called before the first frame update
    void Start()
    {

	}

	// Update is called once per frame
	void Update()
    {
	}

	public void loadKeywords()
	{
		sb = gameObject.GetComponent<SentenceBehaviour>();
		shuffledKeywords = sb.keywordsShuffled.ToArray();
	}

	public void addText()
	{
		words = new Text[shuffledKeywords.Length];
		for (int i = 0; i < words.Length; i++)
		{
			words[i] = (Text)Instantiate(tmpword);
			words[i].transform.SetParent(canvas.transform.Find("Panel"), false);
			words[i].text = shuffledKeywords[i];
		}
		
	}

	public void clearText() {
		GameObject[] words = GameObject.FindGameObjectsWithTag("word");
		foreach(GameObject word in words)
		{
			GameObject.Destroy(word);
		}
	}
	
}
