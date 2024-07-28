using System.Collections.Generic;
using UnityEngine;

public class ApplyProjectile : MonoBehaviour, IAbilityTarget
{
    public int projectile = 0;
    public List<GameObject> Targets { get; set; }
    public void Execute()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa");
        foreach (var other in Targets)
        {
            if (other.tag == "Projectile")
            {
                Destroy(other.gameObject);

                projectile += 1;
            }
        }
    }


}
