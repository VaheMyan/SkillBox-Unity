using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplyShoot : MonoBehaviour, IAbilityTarget
{
    public int Speed = 5;
    public List<GameObject> Targets { get; set; }

    bool isStartDestroy = false;

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
        if (this.gameObject.activeInHierarchy)
        {
            this.transform.position += Vector3.forward * Time.deltaTime * Speed;
            DestroyBullet();
        }

        //Treeger();

    }
    private async void DestroyBullet()
    {
        if (isStartDestroy == false)
        {
            isStartDestroy = true;
            await Task.Delay(2000);
            this.gameObject.SetActive(false);
            isStartDestroy = false;
        }
    }

}

