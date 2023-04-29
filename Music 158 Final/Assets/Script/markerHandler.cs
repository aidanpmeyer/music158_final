using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class markerHandler : MonoBehaviour, INotificationReceiver
{
    public List<audioTimer> loopTimers;
    //add an audio source for each marker
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        //print the audio marker
        audioMarker marker = notification as audioMarker;
        Debug.Log(notification);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = marker.clip;
        source.Play();
        // destroy and delay by length
        if (!marker.loop)
        {
            Destroy(source, marker.clip.length);
        }
        else
        {
            loopTimers.Add(new audioTimer
            {
                // adds a loop timer
                timer = marker.timebetweenLoops + marker.clip.length,
                startTime = marker.timebetweenLoops + marker.clip.length,
                source = source
            });
        }
    }
    public void Update()
    {
        for(int i=0; i< loopTimers.Count; i++)
        {
            // subtracts the timer varaible by 1
            loopTimers[i].timer -= Time.deltaTime;
            if (loopTimers[i].timer <= 0)
            {
                loopTimers[i].source.Play();
                loopTimers[i].timer = loopTimers[i].startTime;
            }
        }

    }
}
[System.Serializable]
public class audioTimer
{
    //a timer that is used for every clip that should loop
    public float timer;
    public float startTime;
    public AudioSource source;
}