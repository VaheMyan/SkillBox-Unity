using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ApplyTag : MonoBehaviour, IAbilityTarget
{
    public string otherTag;
    public List<GameObject> Targets { get; set; }

    public ParticleSystem fier;

    public async void Execute()
    {
        foreach (var other in Targets)
        {
            if (other.tag == ("Bullet"))
            {
                var fierClon = Instantiate(fier, other.transform.position, other.transform.rotation);
                fierClon.Play();
                DestroyImmediate(other.gameObject);
                await Task.Delay(1000);
                DestroyImmediate(fierClon);
            }
            return;
        }
    }
}
