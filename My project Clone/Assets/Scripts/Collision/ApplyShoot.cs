using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplyShoot : MonoBehaviour, IAbilityTarget
{
    public bool startForward = false;
    public int Speed = 5;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        //
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
            this.transform.position += Vector3.forward * Time.deltaTime * Speed;
        }

        //Treeger();

    }
    private void Start()
    {
        DestroyBullet();
    }

    private async void DestroyBullet()
    {
        await Task.Delay(2000);
        Destroy(this.gameObject);
    }

}

