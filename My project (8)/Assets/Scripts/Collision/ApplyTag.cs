using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTag : MonoBehaviour, IAbilityTarget
{
    public string otherTag;
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        foreach (var other in Targets)
        {
            if(other.CompareTag(otherTag))
            {
                //Debug.Log(other.tag);
            }

        }
    }
}
