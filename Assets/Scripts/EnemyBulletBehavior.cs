using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    private CharacterController player;
    private EnemyTest enemy;
    private Vector2 enemyPosition;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Edges")
        {
            Destroy(gameObject);
        }
        if (col.tag == "Player")
        { 
            // Get the player's PlayerController script.
            CharacterController charCon = col.gameObject.GetComponent<CharacterController>();

            if (charCon != null)
            {
                // Call the PlayerDamage method on the player, passing the enemy's position.
                charCon.PlayerDamage(transform.position);
            }
            Destroy(gameObject);
        }
    }
}
