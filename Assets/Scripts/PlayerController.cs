using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //store the previous position of the mouse
    public Vector3 previousMousePosition;
    //store the previous velocity
    private Vector3 previousVelocity;
    //store the previous velocity
    public Vector3 currentDirectionVector;
    //store the previous attack power
    private int attackPower;
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
    [SerializeField]
    private float pushForce = 4000;
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
    [SerializeField]
    public float maxStamina= 5;
    //store the stamina
    [SerializeField]
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


        //get the current direction vector
        UpdateDirectionVector();


        //if left click up occours
        if (Input.GetMouseButtonUp(0) && playerDragging==true )
        {     
            if ((Mathf.Abs(currentDirectionVector.x) >= 0.4 || Mathf.Abs(currentDirectionVector.z) >= 0.4) && stamina==5)
            {
                selectedForce = pushForce * (int)stamina;
                stamina -= 5;
                attackPower = -5;
                GetComponent<Rigidbody>().AddForce(currentDirectionVector.x * pushForce, 0, currentDirectionVector.z * pushForce);
              
            }
            else if ((Mathf.Abs(currentDirectionVector.x) >= 0.3 || Mathf.Abs(currentDirectionVector.z) >= 0.3) && stamina >= 4)
            {
                selectedForce = pushForce * (int)stamina;
                stamina -= 4;
                attackPower = -4;
                GetComponent<Rigidbody>().AddForce(currentDirectionVector.x * pushForce, 0, currentDirectionVector.z * pushForce);
                
            }
            else if ((Mathf.Abs(currentDirectionVector.x) >= 0.2 || Mathf.Abs(currentDirectionVector.z) >= 0.2 )&& stamina >= 3)
            {
                selectedForce = pushForce * (int)stamina;
                stamina -= 3;
                attackPower = -3;
                GetComponent<Rigidbody>().AddForce(currentDirectionVector.x * pushForce, 0, currentDirectionVector.z * pushForce);
         
            }
            else if((Mathf.Abs(currentDirectionVector.x) >= 0.1 || Mathf.Abs(currentDirectionVector.z) >= 0.1 )&& stamina >= 2)
            {
                selectedForce = pushForce * (int)stamina;
                stamina -= 2;
                attackPower = -2;
                GetComponent<Rigidbody>().AddForce(currentDirectionVector.x * pushForce, 0, currentDirectionVector.z * pushForce);
          
            }
            else if((Mathf.Abs(currentDirectionVector.x) >= 0.0 || Mathf.Abs(currentDirectionVector.z) >= 0.0) && stamina >= 1)
            {
                selectedForce = pushForce * (int)stamina;
                stamina -= 1;
                attackPower = -1;

                GetComponent<Rigidbody>().AddForce(currentDirectionVector.x * pushForce, 0, currentDirectionVector.z * pushForce);
            }
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
            wolfcontroller.TakeDamage(attackPower);
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
        }
       
            
        
    }

    private void UpdateDirectionVector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Vector3 currentMousePosition = hit.point;
            previousMousePosition = currentMousePosition;
            currentDirectionVector = transform.position - currentMousePosition;
            currentDirectionVector = currentDirectionVector.normalized;
        }
    }

    private void RegenerateStats()
    {

        if (stamina < maxStamina)
        {
            stamina += 1f * Time.deltaTime;
        }
        if (health < maxHealth)
        {
            health += 0.2f * Time.deltaTime;
        }
        healthBar.value = (float)health;
        staminaBar.value = (float)stamina;
    }
    
}
