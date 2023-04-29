using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


// monobehavior: you can attch to game object
// marker: you can place on the timeline
// iNotification: detect when the cursor hits the marker
public class audioMarker : Marker, INotification
{
    public AudioClip clip;
    public PropertyName id => new PropertyName();
    public bool loop;
    public float timebetweenLoops;
}
