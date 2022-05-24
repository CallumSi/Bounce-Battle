using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfController : MonoBehaviour
{
    //store the player game object 
    private GameObject player;
    //store the neutral wolf icon emote
    private Image wolfImage;
    //store the dead wolf icon emote
    private Image wolfdieImage;
    //store the wold hit sound 
    [SerializeField]
    private AudioSource wolfHit;
    //store the wolf die poof sound 
    [SerializeField]
    private AudioSource wolfPoof;
    //store the healthbar
    public Slider healthBar;
    //store the stamina bar
    public Slider staminaBar;
    //store the directoin vector to the player
    private Vector3 directionVectorToPlayer;
    //store the previous velocity
    private Vector3 previousVelocity;
    //store the previous attack power
    public int previousAttackPower;
    //store the attack power 
    private int attackPower;
    //store if the wolf dead
    private bool wolfDead = false;
    //store if the game isstarted 
    private bool gameStarting = true;
    //store the push force
    private float pushForce = 500;
    //store the max health
    public float maxHealth;
    //store the health
    public float health;
    //store the max stamina
    public float maxStamina;
    //store the stamina
    private float stamina;
    //store the current attack cooldown
    private float cooldown;
    //store time timer used in combination with cooldown
    private float timer;
 
    void Start()
    {
        //initalsie the cooldown randomly 
        cooldown = Random.Range(1, 5);
        //initalise the max health of the wolf randomly
        maxHealth = Random.Range(6, 8);
        //initalise the max stamina of the wolf ramdonly
        maxStamina = Random.Range(3, 4);
        //set the health to the max health value 
        health = maxHealth;
        //set the stamina to the max stamina value 
        stamina = maxStamina;
        //update the health bar 
        healthBar.maxValue = (float)maxHealth;
        //update the stamina bar
        staminaBar.maxValue = (float)maxStamina;
        //find the player object 
        player = GameObject.FindGameObjectWithTag("Player");
        //find the  wolf hud object 
        GameObject imageObject = GameObject.Find("Wolfhud");
        //if image object has been found 
        if (imageObject != null)
        {
            //get the image component 
            wolfImage = imageObject.GetComponent<Image>();
            //hide the image
            wolfImage.enabled = false;
        }
        //if second image object has been found 
        GameObject imageObject2 = GameObject.Find("Wolfdiehud");
        if (imageObject2 != null)
        {
            //get the image component 
            wolfdieImage = imageObject2.GetComponent<Image>();
            //hide the image
            wolfdieImage.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //increment the timer 
        timer += 1 * Time.deltaTime;
        //wait 5 seconds grace period
        if(gameStarting==true && timer > 5)
        {
            gameStarting = false;
        }
        //if the game is not starting 
        if (gameStarting == false)
        {
            //if the wolfs health is 0 or below 
            if (health <= 0 && wolfDead == false)
            {
                //play the death sound 
                wolfPoof.Play();
                //indicate the wolf is dead 
                wolfDead = true;
                //call the die method 
                StartCoroutine(Die());

            }
            //update the current direction vector between this object and the player 
            UpdateDirectionVectorToPlayer();
            //if the wolfs health is above or equal to three and the wolf is not waiting to attack
            if ((int)health >= 3 && cooldown<timer)
            {
                //attack the player 
                Attack();
            }
            else
            {   
                //runaway from the player 
                if(cooldown < timer) {
                    RunAway();
                }              
            }
            //store the previous velocity 
            previousVelocity = GetComponent<Rigidbody>().velocity;

        }
        //regenerate the players stats 
        RegenerateStats();
    }

    //when a collision has occoured 
    private void OnCollisionEnter(Collision collision)
    {
        //if the collision was with the wall 
        if (collision.collider.tag == "Wall")
        { 
            //get the previous speed 
            var speed = previousVelocity.magnitude;
            //get the new direction by reflecting the old direction
            var direction = Vector3.Reflect(previousVelocity.normalized, collision.contacts[0].normal);
            //clamp the y magnitude
            direction.y = 0;
            //apply the force
            GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 3f);
        }
        //store the previous velocity 
        previousVelocity = GetComponent<Rigidbody>().velocity;
        //if the collision was with the player 
        if (collision.collider.tag == "Player")
        {
            //get the controller of the player 
            PlayerController playercontroller = collision.collider.gameObject.GetComponent<PlayerController>();
            //call the take damage method pasing in the wolfs previous attack power
            playercontroller.TakeDamage(previousAttackPower);
            //set the attack power to 0 to stop multiple collisions 
            previousAttackPower = 0;
            //display the emote using the show hud method 
            StartCoroutine(ShowHud());
        }
        //if the collision was with the pig 
        if (collision.collider.tag == "Pig")
        {
            //get the pig controller
            PigController pigcontroller = collision.collider.gameObject.GetComponent<PigController>();
            //apply the bugg by calling the apply buff method in the pig controller passing this controller as a parameter
            pigcontroller.ApplyWolfBuff(this);
        }
        //if the collision was out of bounds 
        if (collision.collider.tag == "OutOfBounds")
        {
            //set the wolf dead to true
            wolfDead = true;
            //kill the wolf 
            StartCoroutine(Die());
        }
    }
    //this method is called externally and used to decrease the hp of the wolf 
    public void TakeDamage(int damageValue)
    {
        //decrease the health by adding the attack value 
        health += damageValue;

    }
    //this method is used to regenerate the health and stamia stats 
    private void RegenerateStats()
    {
        //increase the stamina 
        stamina += 1f * Time.deltaTime;
        //incerase the health
        health += 0.1f * Time.deltaTime;
        //clamp the stamina 
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        //clamp the health
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        //update the health bar 
        healthBar.value = (float)health;
        //update the stamina bar
        staminaBar.value = (float)stamina;
    }

    //this method is used to update the current vector between this object and the player 
    public void UpdateDirectionVectorToPlayer()
    {
        //get the new direction vector 
        directionVectorToPlayer =  player.transform.position - transform.position;

    }
    //this method is used to attack towards the player 
    public void Attack()
    {
       //randomly decide the attack power
        attackPower = Random.Range(-5, 0);
        //if the wolf has enough stamina 
        if((int)stamina>= -attackPower)
        {
            //apply force in the direction of the player 
            GetComponent<Rigidbody>().AddForce(directionVectorToPlayer.normalized.x * pushForce * -attackPower, 0, directionVectorToPlayer.normalized.z * pushForce * -attackPower);
            //reduce the stamina by adding the attack power
            stamina += attackPower;
            //store the attack
            previousAttackPower = attackPower;
            //create a cooldown for the next attack randomly 
            cooldown = Random.Range(5, 15);
            //reset the timer 
            timer = 0;
        }
    }

    //this method is used to move the wolf away from the player 
    public void RunAway()
    {
        //randomly decide the attack power 
        attackPower = Random.Range(-5, 0);
        //if you have enough stamina 
        if ((int)stamina >= -attackPower)
        {
            //apply the force away from the player by using a negative direction vector 
            GetComponent<Rigidbody>().AddForce(-directionVectorToPlayer.normalized.x * pushForce * -attackPower, 0, -directionVectorToPlayer.normalized.z * pushForce * -attackPower);
            //reduce the stamina by adding the attack power
            stamina += attackPower;
            //store the attack
            previousAttackPower = attackPower;
            //create a cooldown for the next attack randomly 
            cooldown = Random.Range(1, 10);
            //reset the timer 
            timer = 0;
        }

    }

    //this method is used to kill the  wolf 
    IEnumerator Die()
    {
        //display the dead wolf emote 
        wolfdieImage.enabled = true;
        //wait for 1 second 
        yield return new WaitForSeconds(2);
        //hide the emote 
        wolfdieImage.enabled = false;
        //destory the game object 
        Destroy(this.gameObject);
    }

    //this method is used to display the wolf emote 
    IEnumerator ShowHud()
    {
        //play the wolf hit sound
        wolfHit.Play();
        //display the emote
        wolfImage.enabled = true;
        //wait for 2 seconds 
        yield return new WaitForSeconds(2);
        //hide the emote 
        wolfImage.enabled = false;

    }
}
