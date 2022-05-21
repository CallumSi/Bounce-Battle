    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicatorController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject indicator;

    private float zOffset = 0;
    private float yOffset = 0;
    private float xOffset = 0;

    void Update()
    {


        transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);

        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController.playerDragging == true)
        {
            
            indicator.SetActive(true);
            transform.LookAt(new Vector3(playerController.currentMousePosition.x, transform.position.y, playerController.currentMousePosition.z));
            transform.Rotate(90, 90, 0);
        }
        else
        {
            indicator.SetActive(false);
        }
       
    }

 

}