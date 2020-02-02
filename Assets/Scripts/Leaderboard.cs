using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
	public Text winnerText;
	public Text runnerupText;
	public Text finalistText;
	// Start is called before the first frame update
	void Start()
    {
		string winner = PlayerPrefs.GetString("Winner", "Nobody");
		string runnerup = PlayerPrefs.GetString("RunnerUp", "Nobody");
		string finalist = PlayerPrefs.GetString("Finalist", "Nobody");
		string friendlyWinner = "";
		string friendlyRunnerUp = "";
		string friendlyFinalist = "";
		if (winner != "Nobody")
		{
			double number = Int32.Parse(winner.Substring(winner.Length - 1));
			number++;
			friendlyWinner = "Player " + number.ToString();
		}
		else
		{
			friendlyWinner = "Nobody";
		}

		if (runnerup != "Nobody")
		{
			double number = Int32.Parse(runnerup.Substring(runnerup.Length - 1));
			number++;
			friendlyRunnerUp = "Player " + number.ToString();
		}
		else
		{
			friendlyRunnerUp = "Nobody";
		}

		if (finalist != "Nobody")
		{
			double number = Int32.Parse(finalist.Substring(finalist.Length - 1));
			number++;
			friendlyFinalist = "Player " + number.ToString();
		}
		else
		{
			friendlyFinalist = "Nobody";
		}
		winnerText.text = "#1 " + friendlyWinner;
		runnerupText.text = "#2 " + friendlyRunnerUp;
		finalistText.text = "#3 " + friendlyFinalist;

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
