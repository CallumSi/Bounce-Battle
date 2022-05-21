using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthIndicatorController : MonoBehaviour
{

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private Slider slider;
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
            transform.Rotate(90, -90, 0);
            
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
            indicator.SetActive(false);
        }

    }
}

