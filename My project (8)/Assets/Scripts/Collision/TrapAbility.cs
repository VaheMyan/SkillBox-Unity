using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAbility : CollisionAbility
{
    public int Damage = 10;

    public void Execute()
    {
        foreach (var target in collisions)
        {
            var targetHealth = target?.gameObject?.GetComponent<CharacterHealth>();
            if (targetHealth != null) // ete inch vor bani baxvum a =>
            {
                targetHealth.Health -= Damage;
            }
        }
    }
}

