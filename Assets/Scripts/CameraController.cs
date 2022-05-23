
   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store the previous distance between the last two touches
    private float previousPinchDistance;
    //store the current mouse position
    private Vector3 currentMousePosition;
    //store the direction vector between last two mouse/touch inputs
    private Vector3 directionVector;
    //store the distance zoomed in or out
    private float zoomDistance = -25;
    //store the sensitivity
    private float sensitivity = 3.5f;

    void Start()
    {
        //set the inital position 
        transform.position = new Vector3(0, 4, 0);
        //rotate then rotate and scale outwards
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
            //if two finger input
            if (Input.touchCount == 2)
            {
                //modify sensitivity
                sensitivity = 1f;
                //get both the touches
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                //store the distance between the pinches 
                previousPinchDistance = Vector2.Distance(touch1.position, touch2.position) / 100;
            }
        }
        //if right click sill held
        else if (Input.GetMouseButton(1))
        {
            //Get the direction vector
            UpdateDirectionVector();
            //update the zoom distance
            UpdateZoomDistance();
            //clamp input        
            if (directionVector.y > 0.05f)
            {
                directionVector.y = 0.05f;
            }
            if (directionVector.y < -0.05f)
            {
                directionVector.y = -0.05f;
            }
            //clamp rotation
            //if the angle is above 70 and you are trying to move up
            if (transform.rotation.eulerAngles.x > 70f && directionVector.y > 0)
            {
                //set the rotation amount to 0
                directionVector.y = 0;
            }
            //if the angle is below 10 and you are trying to move down
            else if (transform.rotation.eulerAngles.x < 10f && directionVector.y < 0)
            {
                //set the rotation amount to 0
                directionVector.y = 0;
            }
            Debug.Log(directionVector.y);
            //position the camera at the centre of the base
            transform.position = new Vector3(0, 4, 0);
            //rotate around x axis
            transform.Rotate(new Vector3(1, 0, 0), (directionVector.y * 180)/sensitivity);
            //rotate around y axis 
            transform.Rotate(new Vector3(0, 1, 0), -directionVector.x * 180/sensitivity, Space.World);
            //translate in the z direction(outward)
            transform.Translate(new Vector3(0, 0, zoomDistance));
            //update the mouse position
            previousMousePosition = currentMousePosition;

        }
      
    }
    private void UpdateDirectionVector()
    {
        currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //get the direction vector from the previous mouse  pos to new mouse pos
        directionVector = previousMousePosition - currentMousePosition;
    }

    private void UpdateZoomDistance()
    {
        //Check for  scroll wheel input
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            //modify zoom distance
            zoomDistance += (Input.GetAxis("Mouse ScrollWheel") * 10);
            //modify the near clipping plane of the camera based on the zoom amount 
            //Camera.main.nearClipPlane = -zoomDistance/3;

        }
        //Check for two finger input
        if (Input.touchCount == 2)
        {
            //get the first and second touch 
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            //get this ditance between the two touches
            float newPinchDistance = Vector2.Distance(touch1.position, touch2.position) / 100;
            //if the distance between pinces is increasing
            if (newPinchDistance > previousPinchDistance && newPinchDistance > 5)
            {
                zoomDistance += 0.1f;
            }
            //if the distance between pinces is decreasing 
            if (newPinchDistance < previousPinchDistance && newPinchDistance > 5)
            {
                zoomDistance -= 0.1f;
            }

        }
        //clamp the values
        if (zoomDistance < -40)
        {
            zoomDistance = -40;

        }
        if (zoomDistance > -10)
        {
            zoomDistance = -10;
        }
    }
}
