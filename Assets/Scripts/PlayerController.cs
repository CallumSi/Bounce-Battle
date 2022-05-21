using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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
    //store the max distance of the raycast
    private float maxDistance = 1000f;
    //store the layer mask
    public LayerMask player;
    //store the healthbar
    public Slider healthBar;
    //store the stamina bar
    public Slider staminaBar;
    //indicate if game won or lost
    public static bool gameWon;
    //store the push force
    private float pushForce = 500;
    //store the selected force
    [SerializeField]
    private float selectedForce;
    //store the max health
    [SerializeField]
    public float maxHealth = 10;
    //store the health
    [SerializeField]
    public float health = 10;
    //store the max stamina
    public float maxStamina= 5;
    //store the stamina
    private float stamina = 5;
    //store the health regeneration per second
    [SerializeField]
    private float healthRegeneration = 0.2f;
    //store the stamina regeneration per second
    [SerializeField]
    private float staminaRegeneration = 1;
  
    //audio
    [SerializeField]
    private AudioSource chickenSoundDie;
    [SerializeField]
    private AudioSource chickenSound1;
    [SerializeField]
    private AudioSource chickenSound2;
    void Start()
    {
        healthBar.maxValue = (float)maxHealth;
        staminaBar.maxValue = (float)maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            CheckIfPlayerClicked();
        }

        //calculate the attack power
        if (currentDirectionVector.magnitude > 9 && (int)stamina >= 5){attackPower = -5;}
        else if (currentDirectionVector.magnitude > 7 && (int)stamina >= 4){attackPower = -4;}
        else if (currentDirectionVector.magnitude > 5 && (int)stamina >= 3){attackPower = -3;}
        else if (currentDirectionVector.magnitude > 3 && (int)stamina >= 2){attackPower = -2;}
        else if (currentDirectionVector.magnitude > 1 && (int)stamina >= 1){attackPower = -1;}
        //get the current direction vector
        UpdateDirectionVector();

    
       
       
        //if left click up occours
        if (Input.GetMouseButtonUp(0) && playerDragging==true )
        {

            stamina += attackPower;
            
            //Debug.Log("stamina: " + stamina);
            //Debug.Log("magnitude: "+currentDirectionVector.magnitude);
            //Debug.Log("attack power: " + attackPower);
            GetComponent<Rigidbody>().AddForce(currentDirectionVector.normalized.x * pushForce * -attackPower, 0, currentDirectionVector.normalized.z * pushForce * -attackPower);
            previousAttackPower = attackPower;
            attackPower = 0;
            playerDragging = false;

        }

        RegenerateStats();
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
        if (collision.collider.tag == "Wolf")
        {
            WolfController wolfcontroller = collision.collider.gameObject.GetComponent<WolfController>();
            wolfcontroller.TakeDamage(previousAttackPower);
            Debug.Log("Giving Damage" + previousAttackPower);
        }
        if (collision.collider.tag == "Pig")
        {
            PigController pigcontroller = collision.collider.gameObject.GetComponent<PigController>();
            pigcontroller.ApplyPlayerBuff(this);
        }
    }

    public void TakeDamage(int damageValue)
    {
        health += damageValue;
       
        if (health <= 0)
        {
           
            chickenSoundDie.Play();
            StartCoroutine(Die());
        }
        else
        {
            if(Random.Range(0,10) < 5)
            {
                chickenSound1.Play();
            }
            else
            {
                chickenSound2.Play();
            }
        }
    }


    IEnumerator Die()
    {
        gameWon = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameEnd");
       
    }



    private void CheckIfPlayerClicked()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, player))
        {
            playerDragging = true;
            previousAttackPower = 0;
        }
       
            
        
    }

    private void UpdateDirectionVector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
           currentMousePosition = hit.point;
            
            currentDirectionVector = transform.position - currentMousePosition;
            
        }
    }

    private void RegenerateStats()
    {
        stamina += 1f * Time.deltaTime;
        health += 0.2f * Time.deltaTime;
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
    
}
