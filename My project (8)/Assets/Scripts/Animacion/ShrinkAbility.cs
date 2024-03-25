using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAbility : MonoBehaviour, IAbility
{
    public float scaleFactor = 0.2f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }
    public void Execute()
    {
        transform.localScale = startScale * scaleFactor;
    }


}
