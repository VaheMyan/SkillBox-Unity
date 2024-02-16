using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour, IBehaviour
{
    // sa veradarcnum e tiv, vory 1-y bajanac persanaji ev ir heravorutyuan vra
    // ete persanajy motik e uremn tivy maksimal e 
    // ete persanajy heru e uremn tivy minimal e
    public CharacterHealth character;
    private void Start()
    {
        character = FindObjectOfType<CharacterHealth>();
    }
    public float Evaluate()
    {
        if (character == null) return 0;
        return 1 / (this.gameObject.transform.position - character.transform.position).magnitude;
    }
    public void Behaviour()
    {
        transform.Rotate(Vector3.up, 10);
    }
}
