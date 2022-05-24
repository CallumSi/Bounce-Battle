using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarController : MonoBehaviour
{
    //the ball object the health bar is attatched to
    [SerializeField]
    private GameObject ball;
    //the x and y offset used to position the element
    private float xOffset = 0;
    private float yOffset = 0;

    void Update()
    {
        //if stamina bar
        if (this.tag == "Stamina")
        {
            yOffset = 1.2f;
        }
        //if health bar
        if (this.tag == "Health")
        {
            yOffset = 1;
        }
        //if direction indicator
        if (this.tag == "Direction")
        {
            yOffset = 0;
            xOffset = -1;
        }
        //update the position to the balls position
        transform.position = new Vector3(ball.transform.position.x + xOffset, ball.transform.position.y + yOffset, ball.transform.position.z) ;
        //face the camera using rotation
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
