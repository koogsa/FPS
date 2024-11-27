using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Rigidbody prefabShell;
    public Transform fireTransform;

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Fire();
        }
    }

    void Fire()
    {
        // 포탄 인스턴스 생성
        Rigidbody shellInstance = Instantiate(prefabShell, fireTransform.position, fireTransform.rotation) as Rigidbody;

        // 포탄 인스턴스에 속도 부여
        shellInstance.velocity = 20.0f * fireTransform.forward;
    }
}
