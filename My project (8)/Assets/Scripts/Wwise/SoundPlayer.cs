using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AK.Wwise.Event backgroundEvent = null;

    public void StartBackgroundMusic()
    {
        backgroundEvent.Post(this.gameObject);
    }
    public void StopBackgroundMusic()
    {
        backgroundEvent.Stop(this.gameObject);
    }

}
