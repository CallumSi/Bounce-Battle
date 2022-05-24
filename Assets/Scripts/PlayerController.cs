using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //chicken sound effects 
    [SerializeField]
    private AudioSource chickenSoundDie;
    [SerializeField]
    private AudioSource chickenSound1;
    [SerializeField]
    public AudioSource chickenSound2;
    //collision sound effect
    [SerializeField]
    public AudioSource clink;
    //image fields to store the player icon emotes
    [SerializeField]
    public Image neutral;
    [SerializeField]
    public Image happy;
    [SerializeField]
    public Image sad;
    //store the game object the star particle effect is attached to
    [SerializeField]
    public GameObject starEffect;
    //store the healthbar
    public Slider healthBar;
    //store the stamina bar
    public Slider staminaBar;
    public Vector3 currentMousePosition;
    //store the previous velocity
    private Vector3 previousVelocity;
    //store the previous velocity
    public Vector3 currentDirectionVector;
    //store the current attack power
    public int attackPower;
    //store the previous attack power
    public int previousAttackPower;
    //store if the player has initated a movement
    public bool playerDragging = false;
    //indicate if game won or lost
    public static bool gameWon;
    //store the max distance of the raycast
    private float maxDistance = 1000f;
    //store the push force
    private float pushForce = 500;
    //store the selected force
    [SerializeField]
    private float selectedForce;
    //store the max health
    public float maxHealth = 10;
    //store the health
    public float health = 10;
    //store the max stamina
    public float maxStamina= 5;
    //store the stamina
    private float stamina = 5;

    void Start()
    {
        //increase the max value of the health bar
        healthBar.maxValue = (float)maxHealth;
        //increase the max value of the stamina bar
        staminaBar.maxValue = (float)maxStamina;
        //hide the particle effect
        starEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player clicks once 
        if (Input.GetMouseButtonDown(0) | Input.touchCount == 1)
        {
            //indicate the player is starting to drag there click
            playerDragging = true;

        }
        //if the touch count has increased 
        if(Input.touchCount > 1)
        {
            //indicate the player is not to drag there click
            playerDragging = false;
        }

        //calculate the attack power based on the magnitude of the vector between the drag start and stop points
        if (currentDirectionVector.magnitude > 9 && (int)stamina >= 5){attackPower = -5;}
        else if (currentDirectionVector.magnitude > 4 && (int)stamina >= 4){attackPower = -4;}
        else if (currentDirectionVector.magnitude > 3 && (int)stamina >= 3){attackPower = -3;}
        else if (currentDirectionVector.magnitude > 2 && (int)stamina >= 2){attackPower = -2;}
        else if (currentDirectionVector.magnitude > 1 && (int)stamina >= 1){attackPower = -1;}
        //get the current direction vector
        UpdateDirectionVector();
        //if left click up occours
        if (Input.GetMouseButtonUp(0) && playerDragging==true && Input.touchCount < 2)
        {
            //reduce the stamina by the attack pwer
            stamina += attackPower;
            //apply force to the players ball based on the selected attack power
            GetComponent<Rigidbody>().AddForce(currentDirectionVector.normalized.x * pushForce * -attackPower, 0, currentDirectionVector.normalized.z * pushForce * -attackPower);
            //store the power of the attack
            previousAttackPower = attackPower;
            //reset the selected attack power to 0
            attackPower = 0;
            //indicate the player isnt dragging anymore
            playerDragging = false;
            //display the neutral emote
            neutral.enabled = true;
            happy.enabled = false;
            sad.enabled = false;
        }
        //regenerate the players stats
        RegenerateStats();
        //store the velocity
        previousVelocity = GetComponent<Rigidbody>().velocity;
     
    }

    //when a collision occours 
    private void OnCollisionEnter(Collision collision)
    {
        //if the player collided with the wall 
        if (collision.collider.tag == "Wall")
        {
            //get the previous speed
            var speed = previousVelocity.magnitude;
            //reflect the direction of the player causing them to bounce of the wall
            var direction = Vector3.Reflect(previousVelocity.normalized, collision.contacts[0].normal);
            //clamp the y 
            direction.y = 0;
            //apply the force
            GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 3f);

        }
        //if the player collided with a wolf 
        previousVelocity = GetComponent<Rigidbody>().velocity;
        if (collision.collider.tag == "Wolf")
        {
            //get the controller of the wolf 
            WolfController wolfcontroller = collision.collider.gameObject.GetComponent<WolfController>();
            //play the collision sound effectt
            clink.Play();
            //give the wolf damage based on the previous attack power
            wolfcontroller.TakeDamage(previousAttackPower);
            //set the previous attack to 0 to prevent continous damage
            previousAttackPower = 0;
            //display the particle effect
            StartCoroutine(PlayParticleEffect());

        }
        //if the player collided with a pig 
        if (collision.collider.tag == "Pig")
        {
            //get the pigs controller
            PigController pigcontroller = collision.collider.gameObject.GetComponent<PigController>();
            //play the collision sound 
            clink.Play();
            //apply the buff to the player by passing it the player contoller
            pigcontroller.ApplyPlayerBuff(this);
            //display the collision particle effect 
            StartCoroutine(PlayParticleEffect());
        }
        //if the player collided with a out of bounds section
        if (collision.collider.tag == "OutOfBounds")
        {
            //kill the player
            StartCoroutine(Die());
        }
    }

    //method used to take damage called externally
    public void TakeDamage(int damageValue)
    {
        //decrease the health by adding the passed damage value 
        health += damageValue;
        //if the players health reaches 0 or below 
        if (health <= 0)
        {
           //play the death spund 
            chickenSoundDie.Play();
            //kill the chicken
            StartCoroutine(Die());
        }
        else
        {
            //change the chickens emote
            sad.enabled = true;
            neutral.enabled = false;
            happy.enabled = false;
            //randomly play one of the chicken sounds 
            if (Random.Range(0,10) < 5)
            {
                chickenSound1.Play();
            }
            else
            {
                chickenSound2.Play();
            }
        }
    }

    //used to kill the player 
    IEnumerator Die()
    {
        //set the game won to false
        gameWon = false;
        //wait for 2 seconds
        yield return new WaitForSeconds(2);
        //change the scene to the game end 
        SceneManager.LoadScene("GameEnd");
       
    }

    //used to display the particle effect for 1 second 
    IEnumerator PlayParticleEffect()
    {
        //set the particle effect to active 
        starEffect.SetActive(true);
        //wait 1 second 
        yield return new WaitForSeconds(1);
        //set the particle effect to inactive
        starEffect.SetActive(false);
    }

    //used to update the direction vector between the postion of the the player and the position of the mouse
    private void UpdateDirectionVector()
    {
        //create a ray cast 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if the ratcast hit 
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //store the point that was hit 
           currentMousePosition = hit.point;
           //update the direction vector 
           currentDirectionVector = transform.position - currentMousePosition;
        }
    }

    //used to regenerate the players stats 
    private void RegenerateStats()
    {
        //increase the players stamina 
        stamina += 1f * Time.deltaTime;
        //increase the players health 
        health += 0.2f * Time.deltaTime;
        //clamp the stamina value 
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
         
        }
        //clamp the health value
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        //update the health bar
        healthBar.value = (float)health;
        //update the stamina bar
        staminaBar.value = (float)stamina;
    }
    
}
