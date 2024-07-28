using UnityEngine;

public class ChangeAbility : MonoBehaviour, IAbility
{
    public float changeDelay;

    public Material dissolveMaterial;

    public float dissolve = 0.01f;
    public float _changeMaterialInput;
    [HideInInspector] public bool isDissolved = false;

    private float _runTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _runTime + changeDelay) return; // ete jamanaky poqr e _runTime + shootDelay-ic apa noric (return)

        _runTime = Time.time;

        if (_changeMaterialInput == 0 && !isDissolved)
        {
            if (dissolve > 0.03f && dissolve <= 1)
            {
                dissolve -= 1f * Time.deltaTime;
                dissolveMaterial.SetFloat("_Change", dissolve);
            }
        }
        if (_changeMaterialInput == 1 || isDissolved)
        {
            if (!(dissolve >= 0.9f && dissolve <= 1f))
            {
                dissolve += 1f * Time.deltaTime;
                dissolveMaterial.SetFloat("_Change", dissolve);
            }
        }
    }
}

