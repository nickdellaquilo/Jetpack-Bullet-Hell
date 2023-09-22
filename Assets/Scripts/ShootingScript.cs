using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingScript : MonoBehaviour
{
    //Used for pointing gun in the right direction:
    private Camera main;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public Vector3 rotation;

    private SpriteRenderer sr;

    //Used for gun functionality:
    public bool canShoot;
    private float timer;
    public float fireCooldown;
    private bool reloading = false;
    private int ammo = 10;
    public float reloadTime = 2f;

    //Used for Ammo display:
    public TMP_Text ammoDisplay;
    
    //Used for sound effects:
    public AudioSource gunShot;

    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rotation = mousePos - transform.position;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!reloading){//Display ammo
            ammoDisplay.text = ammo.ToString();
        }
        else{//Unless reloading
            ammoDisplay.text = "Reloading";
        }
        
        //Pointing gun the right way:
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        rotation = mousePos - transform.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);


        //Trigger reload if out of ammo or right clicking mouse:
        if(ammo <= 0 || Input.GetMouseButton(1)){
            StartCoroutine(Reloading());
            ammo = 10;
        }

        //Bullet cooldown:
        if(!canShoot){
            timer += Time.deltaTime;
            if(timer > fireCooldown){
                canShoot = true;
                timer = 0;
            }
        }

        //If left click and shooting conditions are met:
        if(Input.GetMouseButton(0) && canShoot && !reloading){
            gunShot.Play();
            canShoot = false;
            ammo--;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

        //Used to flip asset when looking a certain direction:
        if((rotation.x > 0 && sr.flipY) || (rotation.x < 0 && !sr.flipY)){
            sr.flipY = !sr.flipY;
        }
    }

    private IEnumerator Reloading(){ //Reload Coroutine
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;

    }
}
