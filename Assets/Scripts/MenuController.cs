using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //audio
    [SerializeField]
    private AudioSource winMusic;
    //audio
    [SerializeField]
    private AudioSource gameoverMusic;

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
                winMusic.Play();

            }
            else if (PlayerController.gameWon == false)
            {

                WinText.SetActive(false);
                GameOverText.SetActive(true);
                gameoverMusic.Play();
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
