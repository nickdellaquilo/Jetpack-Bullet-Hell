using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public EnemySpawner es;
    private Rigidbody2D rb;

    //Used for movement:
    public float speed;
    public float launchSpeed;
    public float maxUpwardsVelocity;
    public float maxDownwardsVelocity;
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

    public CountdownUI timer;

    //Used for getting hit by an enemy
    public float knockbackForce = 5f; // Adjust the knockback force.
    public float invulnerableDuration = 1f; // Duration of invulnerability in seconds.
    public Color invulnerableColor = Color.red; // Color to indicate invulnerability.

    private bool isInvulnerable = false;

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
        if(currentFuel > maxFuel) currentFuel = maxFuel;

        fuelBar.UpdateFuel(currentFuel, maxFuel);//check current fuel every frame and update slider

        //Intiate Dash Coroutine and lose chunk of fuel
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && currentFuel > 0f){
            currentFuel -= 2;
            StartCoroutine(Dash());
        }

        //Flip asset based on direction you're pointing
        if((shoot.rotation.x > 0 && sr.flipX) || (shoot.rotation.x < 0 && !sr.flipX)){
            sr.flipX = !sr.flipX;
        }

        //Change color to blue when hit by enemy
        if (isInvulnerable)
        {
            sr.color = invulnerableColor;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    void FixedUpdate()
    {
        //basic movement
        if(!isInvulnerable)
        {
            horiz = Input.GetAxisRaw("Horizontal");
            transform.Translate(horiz * speed * Time.deltaTime, 0f, 0f);
            if(Input.GetKey(KeyCode.W) && currentFuel > 0f){
                currentFuel -= 0.01f * es.maxWave;
                rb.AddForce(Vector3.up * launchSpeed);
            }
        }

        // Limit to vertical speed to prevent unwinnable scenarios.
        float clampedVerticalSpeed = Mathf.Clamp(rb.velocity.y, -maxDownwardsVelocity, maxUpwardsVelocity);
        rb.velocity = new Vector2(rb.velocity.x, clampedVerticalSpeed);
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

    public void PlayerDamage(Vector2 enemyPosition)
    {
        if (!isInvulnerable)
        {
            // Calculate knockback direction away from the enemy
            Vector2 knockbackDirection = ((Vector2)transform.position - enemyPosition).normalized;

            // Apply knockback force
            rb.velocity = knockbackDirection * knockbackForce;

            // Decrease fuel
            currentFuel -= 2.5f;

            // Set invulnerable state and schedule the end of invulnerability
            isInvulnerable = true;
            Invoke("EndInvulnerability", invulnerableDuration);
        }
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;
        rb.velocity = new Vector3(0f,0f,0f);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag =="Hazard_Ground"){
            PlayerPrefs.SetFloat("Time", timer.survivalTime);
            Destroy(gameObject);
            gunShoot.Stop();
            SceneManager.LoadScene(2);
            //**EDIT NEEDED Also trigger game over screen, high score, etc. THERE IS NO HEALTH IN THE GAME, hitting the ground is an immediate game over
        }
    }

}
