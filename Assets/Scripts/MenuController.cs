using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerController.gameWon == true && this.name=="End Menu")
        {
            GameObject WinBackground = GameObject.Find("WinBackground");
            GameObject GameOverBackground = GameObject.Find("GameOverBackground");
            WinBackground.SetActive(true);
            GameOverBackground.SetActive(false);    
        }
        else if (PlayerController.gameWon == false && this.name == "End Menu")
        {
            GameObject WinBackground = GameObject.Find("WinBackground");
            GameObject GameOverBackground = GameObject.Find("GameOverBackground");
            WinBackground.SetActive(false);
            GameOverBackground.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
