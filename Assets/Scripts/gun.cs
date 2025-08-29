using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletTransform;
    public AudioSource gunshotSound;

    public float bulletVelocity;
    public float bulletLifetime;

    public int measureSyncDelay;
    private int totalBeats;
    public int[] beats;

    private void Start()
    {
        bulletVelocity = 100;
        bulletLifetime = 2f;
        totalBeats = 0 - measureSyncDelay;
    }
    public void BeatReached()
    {
        print(bulletVelocity);
        totalBeats++;
        int currentBeat = totalBeats % 8;
        foreach (int beat in beats)
        {
            if ((beat - 1) == currentBeat)
            {
                FireWeapon();
            }
        }
    }

    public void FireWeapon()
    {
        gunshotSound.Play();

        GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody>().AddForce(bulletTransform.forward.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifetime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletLifetime)
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(bullet);
    }
}
