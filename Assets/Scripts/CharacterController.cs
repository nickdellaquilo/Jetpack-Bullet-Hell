using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    //Used for movement:
    public float speed;
    public float launchSpeed;
    private float horiz;
    
    //Used for dashing:
    public bool canDash = true;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;

    [SerializeField] private TrailRenderer tr;

    //Used for fuel slider:
    public float maxFuel = 10f;
    public float currentFuel = 10f;
    [SerializeField] FuelBarControl fuelBar;


    //Used for flipping asset based on where you're shooting:
    private bool right = true;
    private ShootingScript shoot;
    private SpriteRenderer sr;

    //Used for sound effects:
    public AudioSource gunShoot;
    
    public TMP_Text dashDisplay;

    void Start()
    {//Initialize values
        rb = GetComponent<Rigidbody2D>();
        fuelBar = GetComponentInChildren<FuelBarControl>();
        fuelBar.UpdateFuel(currentFuel, maxFuel);
        sr = GetComponent<SpriteRenderer>();
        shoot = GetComponentInChildren<ShootingScript>();
    }

    void Update()
    {
        if(canDash){
            dashDisplay.color = Color.green;
            dashDisplay.text = "Dash Available";
            
        }
        else{
            dashDisplay.color = Color.red;
            dashDisplay.text = "Recharging";
            
        }
        if(currentFuel < 0f) currentFuel = 0f;//if the player dashed right before running out of fuel, fuel would be at a large-sih negative value. This stops that


        fuelBar.UpdateFuel(currentFuel, maxFuel);//check current fuel every frame and update slider

        //basic movement
        horiz = Input.GetAxisRaw("Horizontal");
        transform.Translate(horiz * speed * Time.deltaTime, 0f, 0f);
        if(Input.GetKey(KeyCode.W) && currentFuel > 0f){
            currentFuel -= 0.0024f;
            rb.AddForce(Vector3.up * launchSpeed);
        }


        //Intiate Dash Coroutine and lose chunk of fuel
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && currentFuel > 0f){
            currentFuel -= 2;
            StartCoroutine(Dash());
        }

        //Flip asset based on direction you're pointing
        if((shoot.rotation.x > 0 && sr.flipX) || (shoot.rotation.x < 0 && !sr.flipX)){
            sr.flipX = !sr.flipX;
        }
    }


    private IEnumerator Dash(){ //Dash Coroutine
        canDash = false;
        float grav = rb.gravityScale;
        rb.gravityScale = 0f;
        float oldSpeed = speed;
        speed = dashSpeed;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = grav;
        speed = oldSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag =="Hazard_Ground"){
            Destroy(gameObject);
            gunShoot.Stop();
            SceneManager.LoadScene(2);
            //**EDIT NEEDED Also trigger game over screen, high score, etc. THERE IS NO HEALTH IN THE GAME, hitting the ground is an immediate game over
        }
    }

}
