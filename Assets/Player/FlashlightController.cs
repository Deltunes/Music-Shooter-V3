using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Flashlight;
    public AudioSource FlashlightSound;
    private bool FlashlightActive = false;
    void Start()
    {
        Flashlight.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (!FlashlightActive)
            {
                Flashlight.gameObject.SetActive(true);
                FlashlightActive = true;
                FlashlightSound.Play();
            }
            else if (FlashlightActive)
            {
                Flashlight.gameObject.SetActive(false);
                FlashlightActive = false;
                FlashlightSound.Play();
            }
        }
    }
}
