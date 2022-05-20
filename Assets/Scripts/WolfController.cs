using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfController : MonoBehaviour
{
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store the previous velocity
    private Vector3 previousVelocity;
    //store if the player has initated a movement
    private bool playerDragging = false;
    //store if the player is currently moving on the x or y axis
    private bool playerMoving = false;
    //store the push force
    private float pushForce = 4000;
    //store the selected force
    private float selectedForce;
    //store the max health
    public float maxHealth;
    //store the health
    public float health;
    //store the max stamina
    public float maxStamina;
    //store the stamina
    private float stamina;
    //store the health regeneration per second
    private float healthRegeneration = 0.2f;
    //store the stamina regeneration per second
    private float staminaRegeneration = 1;
    //store the healthbar
    public Slider healthBar;
    //store the stamina bar
    public Slider staminaBar;
    void Start()
    {
        maxHealth = Random.Range(6, 8);
        health = maxHealth;
        maxStamina = Random.Range(3, 4);
        stamina = maxStamina;
        healthBar.maxValue = (float)maxHealth;
        staminaBar.maxValue = (float)maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
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

        if (stamina < maxStamina)
        {
            stamina += 1f * Time.deltaTime;
        }
        if (health < maxHealth)
        {
            health += 0.1f * Time.deltaTime;
        }


        healthBar.value = (float)health;
        staminaBar.value = (float)stamina;
        previousVelocity = GetComponent<Rigidbody>().velocity;




    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            var speed = previousVelocity.magnitude;
            var direction = Vector3.Reflect(previousVelocity.normalized, collision.contacts[0].normal);
            direction.y = 0;
            GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 3f);

        }
        

        previousVelocity = GetComponent<Rigidbody>().velocity;
        if (collision.collider.tag == "Player")
        {
            PlayerController playercontroller = collision.collider.gameObject.GetComponent<PlayerController>();
            playercontroller.TakeDamage(-2);
        }
        if (collision.collider.tag == "Pig")
        {
            PigController pigcontroller = collision.collider.gameObject.GetComponent<PigController>();
            pigcontroller.ApplyWolfBuff(this);
        }

    }
    public void TakeDamage(int damageValue)
    {
        health += damageValue*10;
    }
}
