using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfController : MonoBehaviour
{
    //store the directoin vector to the player
    private Vector3 directionVectorToPlayer;
    //store the previous velocity
    private Vector3 previousVelocity;
    //store the player
    private GameObject player;
    //store if the player has initated a movement
    private bool playerDragging = false;
    //store if the player is currently moving on the x or y axis
    private bool playerMoving = false;
    //store the push force
    private float pushForce = 500;
    //store the selected force
    private float selectedForce;
    //store the previous attack power
    public int previousAttackPower;
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
    //store the attack power 
    private int attackPower;
    private float cooldown;
    private float timer;
    private Image wolfImage;
    private Image wolfdieImage;
    //audio
    [SerializeField]
    private AudioSource wolfHit;
    [SerializeField]
    private AudioSource wolfPoof;
    //is the wolf dead
    private bool wolfDead = false;
    //store if the game isstarted 
    private bool gameStarting = true;

    void Start()
    {
        cooldown = Random.Range(1, 5);
        maxHealth = Random.Range(6, 8);
        health = maxHealth;
        maxStamina = Random.Range(3, 4);
        stamina = maxStamina;
        healthBar.maxValue = (float)maxHealth;
        staminaBar.maxValue = (float)maxStamina;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject imageObject = GameObject.Find("Wolfhud");
        if (imageObject != null)
        {
            wolfImage = imageObject.GetComponent<Image>();
            wolfImage.enabled = false;
        }
        GameObject imageObject2 = GameObject.Find("Wolfdiehud");
        if (imageObject2 != null)
        {
            wolfdieImage = imageObject2.GetComponent<Image>();
            wolfdieImage.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //timer
        timer += 1 * Time.deltaTime;
        //wait 5 seconds grace period
        if(gameStarting==true && timer > 5)
        {
            gameStarting = false;
        }

        if (gameStarting == false)
        {
            
            if (health <= 0 && wolfDead == false)
            {
                //wolfHit.Play();
                wolfPoof.Play();
                wolfDead = true;
                StartCoroutine(Die());

            }

            UpdateDirectionVectorToPlayer();
            if ((int)health >= 3 && cooldown<timer)
            {
                Attack();
            }
            else
            {   
                if(cooldown < timer) {
                    RunAway();
                }
                
              
            }

            
            previousVelocity = GetComponent<Rigidbody>().velocity;

        }
        RegenerateStats();
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
            playercontroller.TakeDamage(previousAttackPower);
            previousAttackPower = 0;
            StartCoroutine(ShowHud());
        }
        if (collision.collider.tag == "Pig")
        {
            PigController pigcontroller = collision.collider.gameObject.GetComponent<PigController>();
            pigcontroller.ApplyWolfBuff(this);
        }
        if (collision.collider.tag == "OutOfBounds")
        {
            wolfDead = true;
            StartCoroutine(Die());
        }

    }
    public void TakeDamage(int damageValue)
    {
        health += damageValue;

    }
    private void RegenerateStats()
    {
        stamina += 1f * Time.deltaTime;
        health += 0.1f * Time.deltaTime;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;

        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.value = (float)health;
        staminaBar.value = (float)stamina;
    }
    public void UpdateDirectionVectorToPlayer()
    {
        directionVectorToPlayer =  player.transform.position - transform.position;

    }

    public void Attack()
    {
       
        attackPower = Random.Range(-5, 0);
        if((int)stamina>= -attackPower)
        {
            GetComponent<Rigidbody>().AddForce(directionVectorToPlayer.normalized.x * pushForce * -attackPower, 0, directionVectorToPlayer.normalized.z * pushForce * -attackPower);
            stamina += attackPower;
            previousAttackPower = attackPower;
            cooldown = Random.Range(5, 15);
            timer = 0;
        }
        
    }

    public void RunAway()
    {

        attackPower = Random.Range(-5, 0);
        if ((int)stamina >= -attackPower)
        {
            GetComponent<Rigidbody>().AddForce(-directionVectorToPlayer.normalized.x * pushForce * -attackPower, 0, -directionVectorToPlayer.normalized.z * pushForce * -attackPower);
            stamina += attackPower;
            previousAttackPower = attackPower;
            cooldown = Random.Range(1, 10);
            timer = 0;
        }

    }
    IEnumerator Die()
    {
        
        wolfdieImage.enabled = true;
        yield return new WaitForSeconds(1);
        wolfdieImage.enabled = false;
        Destroy(this.gameObject);
    }

    IEnumerator ShowHud()
    {
        wolfHit.Play();
        wolfImage.enabled = true;
        yield return new WaitForSeconds(2);
        wolfImage.enabled = false;

    }
}
