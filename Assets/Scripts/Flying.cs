using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{

    private ParticleSystem[] flame;

    public CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        flame = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) && player.currentFuel > 0){ //Activates particle effects on jetpack
            var em1 = flame[0].emission;
            var em2 = flame[1].emission;
            em1.rateOverTime = 64;
            em2.rateOverTime = 64;
        }
        else{//Deactivates them
            var em1 = flame[0].emission;
            var em2 = flame[1].emission;
            em1.rateOverTime = 0;
            em2.rateOverTime = 0;
        }
    }
}
