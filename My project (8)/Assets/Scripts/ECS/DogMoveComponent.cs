using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DogMoveComponent : MonoBehaviour
{
    [HideInInspector] public float2 Move;
    [HideInInspector] public bool isTest = false;

    private void Awake()
    {
        Move = new float2(0.5f, 0.5f);
    }
}
