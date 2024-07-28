using UnityEngine;

public class RunAbility : MonoBehaviour, IAbility
{
    public int Speed;

    public float runDelay;

    private float _runTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _runTime + runDelay) return; // ete jamanaky poqr e _runTime + shootDelay-ic apa noric (return)

        _runTime = Time.time;
        //
    }
}
