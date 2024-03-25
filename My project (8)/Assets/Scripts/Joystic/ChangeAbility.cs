using UnityEngine;

public class ChangeAbility : MonoBehaviour, IAbility
{
    public float runDelay;

    public Material dissolveMaterial;
    public float dissolve = 0;
    public bool isDissolve;
    public bool isDissolving;
    public float _changeMaterialInput;

    private float _runTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _runTime + runDelay) return; // ete jamanaky poqr e _runTime + shootDelay-ic apa noric (return)

        _runTime = Time.time;

        isDissolve = true;

        if (dissolve <= 0.9f)
        {
            dissolve += 1f * Time.deltaTime;
            dissolveMaterial.SetFloat("_Change", dissolve);
        }
        if (dissolve >= 0.8f && dissolve <= 1f)
        {
            isDissolve = false;
        }
    }
    private void Update()
    {
        //Debug.Log(_changeMaterialInput);
        if (_changeMaterialInput == 0)
        {
            if (dissolve > 0.15f && dissolve <= 1)
            {
                dissolve -= 1f * Time.deltaTime;
                dissolveMaterial.SetFloat("_Change", dissolve);
            }
        }
    }
}

