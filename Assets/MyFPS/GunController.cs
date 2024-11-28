using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    public Rigidbody prefabShell;
    public Transform fireTransform;
    public AudioClip fireSE;
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Rigidbody shellInstance = Instantiate(prefabShell, fireTransform.position, fireTransform.rotation) as Rigidbody;
            shellInstance.velocity = 50.0f * fireTransform.forward;
            aud.PlayOneShot(fireSE);
        }
    }
}
