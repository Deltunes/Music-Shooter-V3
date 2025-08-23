using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float bpm;
    [SerializeField] private AudioSource audioThing;
    [SerializeField] private Intervals[] intervals;

    private void Update()
    {
        foreach (Intervals interval in intervals)
        {
            float sampledTime = (audioThing.timeSamples / (audioThing.clip.frequency * interval.GetIntervalLength(bpm)));
            print(sampledTime);
            interval.CheckForNewInterval(sampledTime);
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
        return 60f / (bpm * noteLength);
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