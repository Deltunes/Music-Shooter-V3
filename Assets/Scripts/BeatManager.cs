using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private float delayInMS;
    [SerializeField] private AudioSource audioThing;
    [SerializeField] private Intervals[] intervals;

    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            float elapsedSeconds = ((float)audioThing.timeSamples / (float)audioThing.clip.frequency) + (delayInMS/1000f);
            float elapsedBeats = elapsedSeconds / interval.GetIntervalLength(bpm);
            interval.CheckForNewInterval(elapsedBeats);
        }
    }
}

[System.Serializable]
public class Intervals
{
    [SerializeField] private float noteLength;
    [SerializeField] private UnityEvent trigger;
    private float lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return (60f * noteLength) / bpm;
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            trigger.Invoke();
        }
    }
}