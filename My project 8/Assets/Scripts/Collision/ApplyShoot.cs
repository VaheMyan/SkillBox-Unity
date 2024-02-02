using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyShoot : MonoBehaviour, IAbilityTarget
{
    public bool startForward = false;
    public int Speed = 5;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        //foreach (var other in Targets)

        startForward = true;

    }
    public void Treeger()
    {
        foreach (var other in Targets)
        {
            if (other.CompareTag("Wall"))
            {
                Speed = -Speed;
            }
            
        }
        
    }

    private void Update()
    {
        if (startForward == true)
        {
            transform.position += Vector3.back * Time.deltaTime * Speed;

        }

        //Treeger();
        
    }

}

