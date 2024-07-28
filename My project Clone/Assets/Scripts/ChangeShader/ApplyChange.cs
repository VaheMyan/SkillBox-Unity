using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyChange : MonoBehaviour
{
    public Material dissolveMaterial;
    public float noiseScale = 0f;

    public float dissolveSpeed = 1.0f;
    public float dissolveDuration = 1.0f; // Длительность эффекта растворения в секундах
    public float dissolveWidth = 1.0f; // Ширина растворения

    public bool isDissolving = false;
    private float dissolveAmount = 0f;
    private float dissolveTimer = 0f;

    public float dissolve = 0;
    public bool isDissolve;

    void Update()
    {
        if (isDissolving)
        {
            isDissolving = true;
            dissolveTimer += Time.deltaTime;

            if (isDissolve == true)
            {
                dissolve -= 1f * Time.deltaTime;
                dissolveMaterial.SetFloat("_Change", dissolve);
                if (dissolve <= 0f)
                {
                    isDissolve = false;
                    isDissolving = false;
                }
            }
            if (isDissolve == false)
            {
                dissolveAmount += dissolveSpeed * Time.deltaTime;
                dissolveAmount = Mathf.Clamp01(dissolveAmount);
                if (dissolve <= 0.9f)
                {
                    dissolve += 1f * Time.deltaTime;
                    dissolveMaterial.SetFloat("_Change", dissolve);
                }
                if (dissolve >= 0.8f)
                {
                    isDissolving = false;
                    isDissolve = true;
                }
            }
        }
    }

    public void TriggerDissolve()
    {
        isDissolving = true;
        dissolveAmount = 0f;
    }
}
