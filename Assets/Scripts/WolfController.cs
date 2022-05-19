using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store if the player has initated a movement
    private bool playerDragging = false;
    //store if the player is currently moving on the x or y axis
    private bool playerMoving = false;
    //store if the player is currently jumping 
    private bool playerJumping = false;
    //store if the player is has already jumped to indicate ready to move 
    private bool playerJumped = false;
    //store the push force
    [SerializeField]
    private float pushForce = 4000;
    //store the selected force
    [SerializeField]
    private float selectedForce;
    //store the max health
    [SerializeField]
    private float maxHealth = 10;
    //store the health
    [SerializeField]
    private float health = 10;
    //store the max stamina
    [SerializeField]
    private float maxStamina = 5;
    //store the stamina
    [SerializeField]
    private float stamina = 5;
    //store the health regeneration per second
    [SerializeField]
    private float healthRegeneration = 0.2f;
    //store the stamina regeneration per second
    [SerializeField]
    private float staminaRegeneration = 1;
    //store the max distance of the raycast
    private float maxDistance = 1000f;
    //store the layer mask
    public LayerMask player;
    //store the healthbar
    public Slider healthBar;
    //store the stamina bar
    public Slider staminaBar;
    void Start()
    {
        healthBar.maxValue = (float)maxHealth;
        staminaBar.maxValue = (float)maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
    //    //if (Input.GetMouseButtonDown(0))
    //    //{
    //    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    //    RaycastHit hit;
    //    //    if (Physics.Raycast(ray, out hit, maxDistance, player))
    //    //    {
    //    //        GetMousePos();
    //    //    }
    //    }

    //    if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 0.1 || Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > 0.1 || Mathf.Abs(GetComponent<Rigidbody>().velocity.z) > 0.1)
    //    {

    //        playerMoving = true;


    //    }
    //    else
    //    {
    //        playerMoving = false;



    //    }

    //    //if left click occours
    //    if (Input.GetMouseButtonUp(0) && playerDragging == true && playerMoving == false)
    //    {

    //        //get the current mouse position
    //        Vector3 currentMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //        //get the direction vector from the previous mouse  pos to new mouse pos (normalised)
    //        Vector3 directionVector = previousMousePosition - currentMousePosition;
    //        Debug.Log("stamina" + stamina);
    //        if ((Mathf.Abs(directionVector.x) >= 0.4 || Mathf.Abs(directionVector.y) >= 0.4) && stamina == 5)
    //        {
    //            selectedForce = pushForce * (int)stamina;
    //            stamina -= 5;
    //            GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);

    //        }
    //        else if ((Mathf.Abs(directionVector.x) >= 0.3 || Mathf.Abs(directionVector.y) >= 0.3) && stamina >= 4)
    //        {
    //            selectedForce = pushForce * (int)stamina;
    //            stamina -= 4;
    //            GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);

    //        }
    //        else if ((Mathf.Abs(directionVector.x) >= 0.2 || Mathf.Abs(directionVector.y) >= 0.2) && stamina >= 3)
    //        {
    //            selectedForce = pushForce * (int)stamina;
    //            stamina -= 3;
    //            GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);

    //        }
    //        else if ((Mathf.Abs(directionVector.x) >= 0.1 || Mathf.Abs(directionVector.y) >= 0.1) && stamina >= 2)
    //        {
    //            selectedForce = pushForce * (int)stamina;
    //            stamina -= 2;
    //            GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);

    //        }
    //        else if ((Mathf.Abs(directionVector.x) >= 0.0 || Mathf.Abs(directionVector.y) >= 0.0) && stamina >= 1)
    //        {
    //            selectedForce = pushForce * (int)stamina;
    //            stamina -= 1;

    //            GetComponent<Rigidbody>().AddForce(directionVector.x * pushForce, 0, directionVector.y * pushForce);

    //        }
    //        else
    //        {

    //        }

    //        playerJumped = false;

    //    }


    //    if (stamina < maxStamina)
    //    {
    //        stamina += 0.001f;
    //    }
    //    healthBar.value = (float)health;
    //    staminaBar.value = (float)stamina;


    //}

    //void GetMousePos()
    //{
    //    if (playerMoving == false)
    //    {
    //        //save the position of the mouse
    //        previousMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //        //inidcate player wants to move the ball
    //        playerDragging = true;

    //    }



    }
}
