using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PigController : MonoBehaviour
{

    //store the player
    private GameObject player;
    //store the pig hit sound effect
    [SerializeField]
    private AudioSource pigHit;
    //store the pig icon image
    private Image pigImage;
    //store the healthbar
    public Slider healthBar;
    //store the direction vector to the player
    private Vector3 directionVectorToPlayer;
    //store the previous position of the mouse
    private Vector3 previousMousePosition;
    //store the previous velocity
    private Vector3 previousVelocity;
    //store the push force
    private float pushForce = 300;
    //store the selected force
    private float selectedForce;
    //store the max health
    private float maxHealth = 10;
    //store the health
    private float health = 10;
    //store the max stamina
    private float maxStamina = 5;
    //store the stamina
    private float stamina = 5;
    //store the stamina regeneration per second
    private float staminaRegeneration = 1;
    //store the max distance of the raycast
    private float maxDistance = 1000f;
    //store the cooldown timer for attacks
    private float cooldown;
    //timer used in combination with cooldown
    private float timer;
    //store the attack power 
    private int attackPower;
    //store if the game is started (grace period)
    private bool gameStarting = true;
    
    void Start()
    {
        //decide the maximum stamina randomly
        maxStamina = Random.Range(3, 4);
        //increase the stamina amount to the max stamina
        stamina = maxStamina;
        //find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        //find the pig icon  image
        GameObject imageObject = GameObject.Find("Pighud");
        //if found 
        if (imageObject != null)
        {
            //get the image component of the object
            pigImage = imageObject.GetComponent<Image>();
            //disable it for now 
            pigImage.enabled = false;
        }
    }
    void Update()
    {

        //increase the timer
        timer += 1 * Time.deltaTime;
        //wait 5 seconds grace period
        if (gameStarting == true && timer > 5)
        {   
            //once grace period is over set game starting to false
            gameStarting = false;
        }

        //if the grace period is over
        if (gameStarting == false)
        {
            //update the current direction vector to the player
            UpdateDirectionVectorToPlayer();
            //if the cooldown time on attack is less than the timer
            if (cooldown < timer)
            {
                //do the run away attack
                 RunAway();
            }
            //store the velocity
            previousVelocity = GetComponent<Rigidbody>().velocity;

        }
        //regenerate the stamina stats
        RegenerateStats();

    }

    //when a collision occours 
    private void OnCollisionEnter(Collision collision)
    {
        //if collided with a wall 
        if (collision.collider.tag == "Wall")
        {
            
            //get the speed 
            var speed = previousVelocity.magnitude;
            //get the directionby reflecting the velocity causing a bounce off the wall
            var direction = Vector3.Reflect(previousVelocity.normalized, collision.contacts[0].normal);
            //clamp the y direction
            direction.y = 0;
            //apply the force 
            GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 3f);
        }
    }

    //called externally on collision with player, take controller as parameter
    public void ApplyPlayerBuff(PlayerController controller)
    {
        //increase the max health
        controller.maxHealth += 1;
        //increase the max stamina
        controller.maxStamina += 1;
        //heal the player full health
        controller.health = controller.maxHealth;
        //update the health bar to display the new max health
        controller.healthBar.maxValue = (float)maxHealth;
        //update the stamina  bar to display the new max health
        controller.staminaBar.maxValue = (float)maxStamina;
        //update player  emote system
        controller.neutral.enabled = false;
        controller.happy.enabled = true;
        controller.sad.enabled = false;
        //display the pig emote using show hud method
        StartCoroutine(ShowHud());
    }
    //called externally on collision with wolf, take controller as parameter
    public void ApplyWolfBuff(WolfController controller)
    {
        //increase the max health by 1
        controller.maxHealth += 1;
        //increase the max staminma by 1
        controller.maxStamina += 1;
        //heal the wolf to full health
        controller.health = controller.maxHealth;
        //update the stamina bar tto display the new max health
        controller.healthBar.maxValue = (float)maxHealth;
        //update the stamina bar tto display the new stamina
        controller.staminaBar.maxValue = (float)maxStamina;
        //display the pig emote using show hud method
        pigHit.Play();
    }

    //used to ge the current direction vector from this object to the player
    public void UpdateDirectionVectorToPlayer()
    {
        //get the direction vector based on the transform positions
        directionVectorToPlayer = player.transform.position - transform.position;

    }

    //used to have the pig run away from the player
    public void RunAway()
    {
        //randomy decide how much strength
        attackPower = Random.Range(-5, 0);
        //if you have enough stamina
        if ((int)stamina >= -attackPower)
        {
            //apply force to move the pig
            GetComponent<Rigidbody>().AddForce(-directionVectorToPlayer.normalized.x * pushForce * -attackPower, 0, -directionVectorToPlayer.normalized.z * pushForce * -attackPower);
            //pay the stamina cost
            stamina += attackPower;
            //create a new cooldown randomly
            cooldown = Random.Range(1, 10);
            //reset the timer
            timer = 0;
        }
    }

    //used to regenereate the stats of the pig over time
    private void RegenerateStats()
    {
        //increase the stamina 
        stamina += 1f * Time.deltaTime;
        //ensure the stamina doesnt exceed the max stamina value 
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }

    //display the hud
    IEnumerator ShowHud()
    {
        //play the ig hit sound 
        pigHit.Play();
        //show the pig emote
        pigImage.enabled = true;
        //wait 2 seconds
        yield return new WaitForSeconds(2);
        //hide the emote
        pigImage.enabled = false;
    }
}
