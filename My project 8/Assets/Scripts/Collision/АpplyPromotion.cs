using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class АpplyPromotion : MonoBehaviour, IAbilityTarget
{
    public int Damage = 10;
    public List<GameObject> Targets { get; set; }
    public void Execute()
    {
        foreach (var other in Targets)
        {
            var health = other.GetComponent<CharacterHealth>();
            if (other.tag == "Player") health.Health += Damage;
        }
    }


}