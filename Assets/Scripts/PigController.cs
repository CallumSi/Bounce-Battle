using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PigController : MonoBehaviour
{
    //store the directoin vector to the player
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
    //store the healthbar
    public Slider healthBar;
    //store the attack power 
    private int attackPower;
    private float cooldown;
    private float timer;
    //store if the game isstarted 
    private bool gameStarting = true;
    //store the player
    private GameObject player;
    [SerializeField]
    private AudioSource pigHit;

    void Start()
    {
       
        maxStamina = Random.Range(3, 4);
        stamina = maxStamina;
    
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {


        //timer
        timer += 1 * Time.deltaTime;
        //wait 5 seconds grace period
        if (gameStarting == true && timer > 5)
        {
            gameStarting = false;
        }

        if (gameStarting == false)
        {

        
            UpdateDirectionVectorToPlayer();
           
            if (cooldown < timer)
            {
                    RunAway();
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
    }

    public void ApplyPlayerBuff(PlayerController controller)
    {
        controller.maxHealth += 1;
        controller.health = controller.maxHealth;
        controller.maxStamina += 1;
        pigHit.Play();

    }
    public void ApplyWolfBuff(WolfController controller)
    {
        controller.maxHealth += 1;
        controller.health = controller.maxHealth;
        pigHit.Play();
    }


    public void UpdateDirectionVectorToPlayer()
    {
        directionVectorToPlayer = player.transform.position - transform.position;

    }

    public void RunAway()
    {

        attackPower = Random.Range(-5, 0);
        if ((int)stamina >= -attackPower)
        {
            GetComponent<Rigidbody>().AddForce(-directionVectorToPlayer.normalized.x * pushForce * -attackPower, 0, -directionVectorToPlayer.normalized.z * pushForce * -attackPower);
            stamina += attackPower;
            cooldown = Random.Range(1, 10);
            timer = 0;
        }

    }

    private void RegenerateStats()
    {
        stamina += 1f * Time.deltaTime;
       
        if (stamina > maxStamina)
        {
            stamina = maxStamina;

        }
       
        
    }
}
