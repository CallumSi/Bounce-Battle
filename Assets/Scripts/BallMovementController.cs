using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store if the player has initated a movement
    private bool playerDragging = false;
    //store if the player is currently moving
    private bool playerMoving = false;
    //store the push force
    [SerializeField]
    private float pushForce = 1000;
    //store the selected force
    [SerializeField]
    private float selectedForce;
    //store the max health
    [SerializeField]
    private double maxHealth = 10;
    //store the health
    [SerializeField]
    private double health = 10;
    //store the max stamina
    [SerializeField]
    private double maxStamina= 5;
    //store the stamina
    [SerializeField]
    private double stamina = 5;
    //store the health regeneration per second
    [SerializeField]
    private double healthRegeneration = 0.2;
    //store the stamina regeneration per second
    [SerializeField]
    private double staminaRegeneration = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 0.1 || Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > 0.1){
            playerMoving = true;
        }
        else
        {
            playerMoving = false;
        }

        Debug.Log(playerMoving);
        //if left click occours
        if (Input.GetMouseButtonUp(0) && playerDragging==true && playerMoving == false )
        {
            
            //get the current mouse position
            Vector3 currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //get the direction vector from the previous mouse  pos to new mouse pos (normalised)
            Vector3 directionVector = previousMousePosition - currentMousePosition;
            Debug.Log("Calculating " + "DV:" + directionVector);
            if ((Mathf.Abs(directionVector.x) >= 0.4 || Mathf.Abs(directionVector.y) >= 0.4) && stamina==5)
            {
                selectedForce = pushForce * (int)stamina;
                //stamina -= 5;
                GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);
                Debug.Log("moved 5, stamina is now:" + stamina);
            }
            else if ((Mathf.Abs(directionVector.x) >= 0.3 || Mathf.Abs(directionVector.y) >= 0.3) && stamina >= 4)
            {
                selectedForce = pushForce * (int)stamina;
                //stamina -= 4;
                GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);
                Debug.Log("moved 4, stamina is now:" + stamina);
            }
            else if ((Mathf.Abs(directionVector.x) >= 0.2 || Mathf.Abs(directionVector.y) >= 0.2 )&& stamina >= 3)
            {
                selectedForce = pushForce * (int)stamina;
                //stamina -= 3;
                GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);
                Debug.Log("moved 3, stamina is now:" + stamina);
            }
            else if((Mathf.Abs(directionVector.x) >= 0.1 || Mathf.Abs(directionVector.y) >= 0.1 )&& stamina >= 2)
            {
                selectedForce = pushForce * (int)stamina;
                //stamina -= 2;
                GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);
                Debug.Log("moved 2, stamina is now:" + stamina);
            }
           else if((Mathf.Abs(directionVector.x) >= 0.0 || Mathf.Abs(directionVector.y) >= 0.0) && stamina >= 1)
            {
                selectedForce = pushForce * (int)stamina;
                //stamina -= 1;

                GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);
                Debug.Log("moved 1, stamina is now:" + stamina);
            }
            else
            {
                Debug.Log("Diddnt move, stamina is now:" + stamina + " DV:"+ directionVector);
            }

           

        }

        
        //if(stamina< maxStamina)
        //{
        //    stamina += 0.0001;
        //}
        
       
    }

    void OnMouseDown()
    {
        if (playerMoving == false)
        {
            //save the position of the mouse
            previousMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //inidcate player wants to move the ball
            playerDragging = true;
            Debug.Log("Storing point");
        }
      


    }
}
