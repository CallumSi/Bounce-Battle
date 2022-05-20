using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject wolf;
    [SerializeField]
    private GameObject pig;
    private GameObject amountOfPigs;
    private GameObject amountOfWolves;
    void Start()
    {

        //randomly decide the amount of entities
        int amountOfWolves = Random.Range(4,6);
        int amountOfPigs = Random.Range(2,4);

        player.transform.Translate(new Vector3(0, 10, 0));
        player.transform.Rotate(new Vector3(1, 0, 0), Random.Range(1, 360));
        player.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 360), Space.World);
            player.transform.Translate(new Vector3(0, 0, -8));

        //instantiate the wolves 
        for (int i=0; i < amountOfWolves; i++)
        {
            SpawnWolves(i);
        }
        //instantiate the pigs 
        for (int i = 0; i < amountOfPigs; i++)
        {
            SpawnPigs(i);
        }


    }

   

    void Update()
    {
        if (GameObject.Find("Wolf(Clone)") == false)
        {
            PlayerController.gameWon = true;
            SceneManager.LoadScene("GameEnd");
        }
   
    }

    void SpawnWolves(int i )
    {
        Debug.Log("Spawning wolf" + i);
        //instantiate the wolf
        GameObject newWolf = Instantiate(wolf, new Vector3(0, 15 + i * 2, 0), Quaternion.identity);
        newWolf.transform.Rotate(new Vector3(1, 0, 0), Random.Range(1,360));
        newWolf.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 360), Space.World);
        newWolf.transform.Translate(new Vector3(0, 0, -8));
    }

    void SpawnPigs(int i)
    {
        //Debug.Log("Spawning pig" + i);
        ////instantiate the pig
        //GameObject newPig = Instantiate(pig, new Vector3(0, 15 + i * 2, 0), Quaternion.identity);
        //newPig.transform.Rotate(new Vector3(1, 0, 0), Random.Range(1, 360));
        //newPig.transform.Rotate(new Vector3(0, 1, 0), Random.Range(1, 360), Space.World);
        //newPig.transform.Translate(new Vector3(0, 0, -8));
    }
}
