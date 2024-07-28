using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityTarget : IAbility // stex npatakna a te vor obektin kpnes inch lini
{
    List<GameObject> Targets { get; set; }
}
