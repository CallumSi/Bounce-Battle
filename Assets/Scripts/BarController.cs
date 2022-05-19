using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private float yOffset = 1;
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
        
        
        //update the position to the balls position
        transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + yOffset, ball.transform.position.z) ;
        //face the camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    
    
    }
}
