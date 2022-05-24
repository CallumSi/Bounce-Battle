using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //get the player object 
    [SerializeField]
    private GameObject player;
    //get the wolf prefab
    [SerializeField]
    private GameObject wolf;
    //get the pig prefab
    [SerializeField]
    private GameObject pig;
    //store the amount of pigs to spawn
    private GameObject amountOfPigs;
    //store the amount of wolves to spawn
    private GameObject amountOfWolves;
    void Start()
    {

        //randomly decide the amount of pigs to spawn
        int amountOfPigs = Random.Range(2,4);
        //randomly decide the amount of wolves to spawn
        int amountOfWolves = Random.Range(4,6);
        //modify the inital players position
        //place in centre of arena 
        player.transform.Translate(new Vector3(0, 10, 0));
        //rotate around x randomly 
        player.transform.Rotate(new Vector3(1, 0, 0), Random.Range(1, 360));
        //rotate around y randomly
        player.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 360), Space.World);
        //translate outwards 
        player.transform.Translate(new Vector3(0, 0, -8));
        //for the amount of wolves to be spawned 
        for (int i=0; i < amountOfWolves; i++)
        {
            //create the wolves
            SpawnWolves(i);
        }
        //for the amount of pigs be spawned 
        for (int i = 0; i < amountOfPigs; i++)
        {
            //create the pigs
            SpawnPigs(i);
        }
    }

    void Update()
    {
        //if there is no wolf clone objects
        if (GameObject.Find("Wolf(Clone)") == false)
        {
            //all the wolves are dead so set the player won to true
            PlayerController.gameWon = true;
            //load the game end scene
            SceneManager.LoadScene("GameEnd");
        }
    }

    //used to spawn wolves taking in the amount of wolves to spawn as a parameter
    void SpawnWolves(int i )
    {
        //instantiate the wolf
        //position them above the centre at intervals
        GameObject newWolf = Instantiate(wolf, new Vector3(0, 15 + i * 2, 0), Quaternion.identity);
        //rotate around x randomly 
        newWolf.transform.Rotate(new Vector3(1, 0, 0), Random.Range(1,360));
        //rotate around y randomly
        newWolf.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 360), Space.World);
        //translate outwards
        newWolf.transform.Translate(new Vector3(0, 0, -8));
    }
    //used to spawn pigs taking in the amount of wolves to spawn as a parameter
    void SpawnPigs(int i)
    {
        //instantiate the pig
        //position them above the centre at intervals
        GameObject newPig = Instantiate(pig, new Vector3(0, 15 + i * 2, 0), Quaternion.identity);
        //rotate around x randomly 
        newPig.transform.Rotate(new Vector3(1, 0, 0), Random.Range(1, 360));
        //rotate around y randomly 
        newPig.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 360), Space.World);
        //translate outwards
        newPig.transform.Translate(new Vector3(0, 0, -8));
    }
}
