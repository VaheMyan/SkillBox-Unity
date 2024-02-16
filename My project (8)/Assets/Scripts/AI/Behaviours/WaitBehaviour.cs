using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBehaviour : MonoBehaviour, IBehaviour
{
    // sa misht veradarcnum e tiv
    public float Evaluate()
    {
        return 0.5f;
    }
    public void Behaviour()
    {
        Debug.Log("WAIT");
    }
}
