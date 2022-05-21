using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store the distance zoomed in or out
    private float zoomDistance =-25;
    private bool lockRotation=false;
    void Start()
    {
        //set the inital position 
        transform.position = new Vector3(0, 5, 0);
        //rotate then scale outwards
        transform.Rotate(new Vector3(1, 0, 0), 5);
        transform.Rotate(new Vector3(0, 1, 0), 5, Space.World);
        transform.Translate(new Vector3(0, 0, -25));
    }

    void Update()
    {
       //if right click occours
        if (Input.GetMouseButtonDown(1))
        {
            //save the position of the mouse
            previousMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            
        }
        //if right click sill held
        else if (Input.GetMouseButton(1))
        {
           
            Vector3 currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //get the direction vector from the previous mouse  pos to new mouse pos
            Vector3 directionVector = previousMousePosition - currentMousePosition;
           
           
            
            //position the camera at the centre of the base
            transform.position = new Vector3(0, 10, 0);
            //rotate around x axis
            if (lockRotation==false)
            {
                transform.Rotate(new Vector3(1, 0, 0), directionVector.y * 180);
            }
            //rotate around y axis 
          
            transform.Rotate(new Vector3(0, 1, 0), -directionVector.x * 180, Space.World);
            
           
           //Check for  scroll wheel input
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                //modify zoom distance
                zoomDistance += (Input.GetAxis("Mouse ScrollWheel") * 10);
                //clamp the values
                if (zoomDistance < -20){
                    zoomDistance = -20;
                  
                }
                if (zoomDistance > -10)
                {
                    zoomDistance = -10;
                }
                Camera.main.nearClipPlane = -zoomDistance/3;
            }
            //translate in the z direction(outward)
            transform.Translate(new Vector3(0, 0, zoomDistance));
            //clamp y axis
            if (transform.position.y < 3)
            {
                lockRotation = true;
                
                transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            }
            if (directionVector.y > 0)
            {
                lockRotation = false;
                Debug.Log("rotation unlocked");
            }

            Debug.Log(directionVector.y);
            //update the mouse position
            previousMousePosition = currentMousePosition;

            
        }
        
    }
}
