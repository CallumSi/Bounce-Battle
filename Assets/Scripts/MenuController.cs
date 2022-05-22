using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name=="End Menu")
        {
            GameObject WinText = GameObject.Find("WinText");
            GameObject GameOverText = GameObject.Find("GameOverText");
            if (PlayerController.gameWon == true)
            {

                WinText.SetActive(true);
                GameOverText.SetActive(false);
            }
            else if (PlayerController.gameWon == false)
            {

                WinText.SetActive(false);
                GameOverText.SetActive(true);
            }
            else
            {
                WinText.SetActive(false);
                GameOverText.SetActive(false);
            }
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
