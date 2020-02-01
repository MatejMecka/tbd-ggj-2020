using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RedirectToGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startButton;
    public InputField input;
    public int numberOfPlayers;

    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame(){
        if(input.text.Length > 0){
            numberOfPlayers = int.Parse(input.text);
        } else { 
            numberOfPlayers = 1;
        }
        PlayerPrefs.SetInt("NumberOfPlayers", numberOfPlayers);
        print(numberOfPlayers);

        SceneManager.LoadScene("game", LoadSceneMode.Single);
    }

    public void quitGame(){
        Application.Quit();
    }
}
