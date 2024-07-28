using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    public int Damage;
    public List<GameObject> Targets { get; set; }

    private CharacterHealth characterHealth;
    [HideInInspector] public bool isTest = false;
    public void Execute()
    {
        if (!isTest)
        {
            foreach (var target in Targets)
            {
                //if (health != null) health.Health -= Damage;
                if (target.tag == "Player" && characterHealth.Health > 0)
                {
                    characterHealth.Health -= Damage;
                    characterHealth.isGetHitAnim = true;
                }

            }
        }
        if (isTest)
        {
            characterHealth.Health -= Damage;
            characterHealth.isGetHitAnim = true;
        }
    }

    private void Start()
    {
        characterHealth = GameObject.FindObjectOfType<CharacterHealth>();
    }


}
