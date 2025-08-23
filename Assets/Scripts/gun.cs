using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletTransform;
    public AudioSource gunshotSound;
    public float bulletVelocity = 30;
    public float bulletLifetime = 3f;

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }
    */

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
