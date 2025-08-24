using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public int measureSyncDelay;
    private int totalBeats;
    private int moveMag = -5;
    private Vector3 moveDirection = new Vector3(0f, 0.001f, 0f);

    private void Start()
    {
        totalBeats = 0 - measureSyncDelay;
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveMag, Space.World);
    }
    
    public void BeatReached()
    {
        totalBeats++;
        int currentBeat = totalBeats % 8;
        if (currentBeat == 0)
        {
            moveMag *= -1;
        }
    }
    
}
