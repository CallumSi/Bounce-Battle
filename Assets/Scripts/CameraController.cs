using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store the distance zoomed in or out
    private float zoomDistance =-25;
   
    void Start()
    {
        //set the inital position 
        transform.position = new Vector3(0, 0, 0);
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
            //get the current mouse position
            Vector3 currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //get the direction vector from the previous mouse  pos to new mouse pos
            Vector3 directionVector = previousMousePosition - currentMousePosition;
            //position the camera at the centre of the base
            transform.position = new Vector3(0, 0, 0);
            //rotate around x axis
            transform.Rotate(new Vector3(1, 0, 0), directionVector.y * 180);
            //rotate around y axis 
            transform.Rotate(new Vector3(0, 1, 0), -directionVector.x * 180, Space.World);
           //Check for  scroll wheel input
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                //modify zoom distance
                zoomDistance += (Input.GetAxis("Mouse ScrollWheel") * 10);
                //clamp the values
                if (zoomDistance < -40){
                    zoomDistance = -40;
                }
                if (zoomDistance > -10)
                {
                    zoomDistance = -10;
                }
            }
            //translate in the z direction(outward)
            transform.Translate(new Vector3(0, 0, zoomDistance));
            //update the mouse position
            previousMousePosition = currentMousePosition;

            
        }
        
    }
}
