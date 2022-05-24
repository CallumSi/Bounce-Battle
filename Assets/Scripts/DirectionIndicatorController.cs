    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicatorController : MonoBehaviour
{

    //get the player we want to attach the inidcator to
    [SerializeField]
    private GameObject player;
    //get the UI inidctor element 
    [SerializeField]
    private GameObject indicator;

    void Update()
    {
        //update the position to the players position 
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //get the player controller element of the player
        PlayerController playerController = player.GetComponent<PlayerController>();
        //check if the player is dragging their input
        if (playerController.playerDragging == true)
        {
            //show the indicator
            indicator.SetActive(true);
            //rotate accordingly 
            transform.LookAt(new Vector3(playerController.currentMousePosition.x, transform.position.y, playerController.currentMousePosition.z));
            transform.Rotate(90, 90, 0);
        }
        else
        {
            //dont display the indicator
            indicator.SetActive(false);
        }
       
    }

 

}
