using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlayAudioOnTracked : DefaultObserverEventHandler
{
    [SerializeField]
    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    protected override void OnTrackingFound()
    {
        _audio.Play();
        base.OnTrackingFound();
    }
    protected override void OnTrackingLost()
    {
        _audio.Stop();
        base.OnTrackingLost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
