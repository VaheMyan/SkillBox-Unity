using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartItemAbility : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; } = new List<GameObject>();

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var character = target.GetComponent<CharacterHealth>();
            if (character == null) return;
            character.Health += 5;
        }
        PhotonNetwork.Destroy(this.gameObject);
    }
}
