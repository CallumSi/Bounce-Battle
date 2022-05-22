using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    private float previousPinchDistance;
    //store the distance zoomed in or out
    private float zoomDistance =-25;
    private bool lockNegativeRotation=false;
    private bool lockPositiveRotation = false;
    void Start()
    {
     
 
        //set the inital position 
        transform.position = new Vector3(0, 5, 0);
        //rotate then rotate and scale outwards
        transform.Rotate(new Vector3(1, 0, 0), 5);
        transform.Rotate(new Vector3(0, 1, 0), 5, Space.World);
        transform.Translate(new Vector3(0, 0, -25));
    }

    void Update()
    {
       //if right click occours
        if (Input.GetMouseButtonDown(1)|Input.touchCount>2)
        {
            //save the position of the mouse
            previousMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (Input.touchCount > 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                previousPinchDistance = Vector2.Distance(touch1.position, touch2.position) / 100;
            }
           

        }
        //if right click sill held
        else if (Input.GetMouseButton(1)| Input.touchCount > 2)
        {
           
            Vector3 currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //get the direction vector from the previous mouse  pos to new mouse pos
            Vector3 directionVector = previousMousePosition - currentMousePosition;
           
           
            
            //position the camera at the centre of the base
            transform.position = new Vector3(0, 10, 0);
            //rotate around x axis
            if (lockNegativeRotation==false && lockPositiveRotation==false)
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
                
               
                //Camera.main.nearClipPlane = -zoomDistance/3;
            }
            //Check for two finger input
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                float newPinchDistance = Vector2.Distance(touch1.position, touch2.position)/100;
                Debug.Log(newPinchDistance);
                if (newPinchDistance > previousPinchDistance && newPinchDistance>1)
                {
                    zoomDistance += newPinchDistance;
                }
                if (newPinchDistance < previousPinchDistance && newPinchDistance > 1)
                {
                    zoomDistance -= newPinchDistance;
                }

            }
            //clamp the values
            if (zoomDistance < -20)
            {
                zoomDistance = -20;

            }
            if (zoomDistance > -10)
            {
                zoomDistance = -10;
            }

        


            //translate in the z direction(outward)
            transform.Translate(new Vector3(0, 0, zoomDistance));
            //clamp negative rotation
            if (transform.position.y < 3)
            {
                lockNegativeRotation = true;
                Debug.Log("rotation y locked");
                transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            }
            if (directionVector.y > 0)
            {
                lockNegativeRotation = false;
                Debug.Log("rotation y unlocked");
            }

            //clamp negative rotation
            if (transform.position.y > 28)
            {
                lockPositiveRotation = true;
                Debug.Log("rotation y locked");
                transform.position = new Vector3(transform.position.x, 28, transform.position.z);
            }
            if (directionVector.y < 0)
            {
                lockPositiveRotation = false;
                Debug.Log("rotation y unlocked");
            }


            Debug.Log(directionVector.y);
            //update the mouse position
            previousMousePosition = currentMousePosition;

            
        }
        
    }
}
