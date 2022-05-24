using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthIndicatorController : MonoBehaviour
{
    //store the player
    [SerializeField]
    private GameObject player;
    //store the indicator UI element 
    [SerializeField]
    private GameObject indicator;
    //store the images for the different power levels 
    [SerializeField]
    private Image power1;
    [SerializeField]
    private Image power2;
    [SerializeField]
    private Image power3;
    [SerializeField]
    private Image power4;
    [SerializeField]
    private Image power5;

    void Update()
    {
        //update the indicators position to thep layers position
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //get the player controller element of the player object
        PlayerController playerController = player.GetComponent<PlayerController>();
        //if the player is dragging their click
        if (playerController.playerDragging == true )
        {
            //display the indicator 
            indicator.SetActive(true);
            //rotate appropriatly
            transform.LookAt(new Vector3(playerController.currentMousePosition.x, transform.position.y, playerController.currentMousePosition.z));
            transform.Rotate(90, -90, 0);
            //change the image displayed based on the current attack power selected in the player controller
            if (-playerController.attackPower == 5){power5.enabled = true;}
            else{ power5.enabled = false;}
            if (-playerController.attackPower == 4) { power4.enabled = true; }
            else { power4.enabled = false; }
            if (-playerController.attackPower == 3) { power3.enabled = true; }
            else { power3.enabled = false; }
            if (-playerController.attackPower == 2) { power2.enabled = true; }
            else { power2.enabled = false; }
            if (-playerController.attackPower == 1) { power1.enabled = true; }
            else { power1.enabled = false; }
        }
        else
        {
            //hide the indicator 
            indicator.SetActive(false);
        }

    }
}

