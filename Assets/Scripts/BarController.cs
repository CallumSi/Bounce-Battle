using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
  
    private float yOffset = 0;
    private float xOffset = 0;

    void Update()
    {
        if (this.tag == "Stamina")
        {
            yOffset = 1.2f;
        }
        if (this.tag == "Health")
        {
            yOffset = 1;
        }
      
        if (this.tag == "Direction")
        {
            yOffset = 0;
            xOffset = -1;
        }

        //update the position to the balls position
        transform.position = new Vector3(ball.transform.position.x + xOffset, ball.transform.position.y + yOffset, ball.transform.position.z) ;
        //face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    
    
    }
}
