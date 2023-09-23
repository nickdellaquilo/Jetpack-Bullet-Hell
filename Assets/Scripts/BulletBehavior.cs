using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //Used for shooting where mouse is pointing:
    private Vector3 mousePos;
    public float speed = 30f;
    private Camera main;
    private Rigidbody2D rb;

    //Used to update "current fuel" when the bullet kills an enemy:
    public CharacterController player;

    void Start()
    {
        //Shooting script:
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }


    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Edges"){ //If bullet collides with walls/roof, destroy bullet
            Destroy(gameObject);
        }
        if(col.tag == "Enemy"){ //if bullet hits enemy, Kill enemy, give player more fuel, destroy bullet. EDIT NEEDED**

            //Destroy(col.gameObject);//Currently enemies are immediately killed when hit. Let's develop a health system.
            //only after we've checked that the enemy health is 0 do we destroy it and add fuel to the player.
            //player.currentFuel += 2;
            Destroy(gameObject);
        }
    }
}
