using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //store the win music audio
    [SerializeField]
    private AudioSource winMusic;
    //store the gameover music audio
    [SerializeField]
    private AudioSource gameoverMusic;
    void Start()
    {
        //if this is the end menu
        if(this.gameObject.name=="End Menu")
        {
            //find the win text
            GameObject WinText = GameObject.Find("WinText");
            //find the game over text text
            GameObject GameOverText = GameObject.Find("GameOverText");

            //if the player has won 
            if (PlayerController.gameWon == true)
            {
                //display the win text 
                WinText.SetActive(true);
                //disable the game over text 
                GameOverText.SetActive(false);
                //play the victory music 
                winMusic.Play();

            }
            //if the player has lost
            else if (PlayerController.gameWon == false)
            {
                //hide the win text 
                WinText.SetActive(false);
                //display the game over text 
                GameOverText.SetActive(true);
                //play the gamer over music 
                gameoverMusic.Play();
            }
            else
            {
                //hide both the texts 
                WinText.SetActive(false);
                GameOverText.SetActive(false);
            }
        }
    }

    //used to load the game scene
    public void StartGame()
    {
        //load the game 
        SceneManager.LoadScene("Game");
    }

    //used to load the menu scene
    public void StartMainMenu()
    {
        //load the menu
        SceneManager.LoadScene("Menu");
    }
    //used to quit the game scene
    public void QuitGame()
    {
        Debug.Log("quit");
        //quit the application 
        Application.Quit();
    }
}
