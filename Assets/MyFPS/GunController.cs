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
        // ��ź �ν��Ͻ� ����
        Rigidbody shellInstance = Instantiate(prefabShell, fireTransform.position, fireTransform.rotation) as Rigidbody;

        // ��ź �ν��Ͻ��� �ӵ� �ο�
        shellInstance.velocity = 20.0f * fireTransform.forward;
    }
}
