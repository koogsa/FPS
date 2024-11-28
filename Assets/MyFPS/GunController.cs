using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Text UI�� ����ϱ� ���� �߰�

public class GunController : MonoBehaviour
{
    public Rigidbody prefabShell;
    public Transform fireTransform;
    public AudioClip fireSE;
    public int bulletCount = 10;  // ���� �Ѿ� ����
    public int maxBullets = 10;   // �ִ� �Ѿ� ����
    public Text bulletCountText;  // �Ѿ� ������ ǥ���ϴ� UI
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        UpdateBulletCountUI();  // �ʱ� �Ѿ� ������ UI�� ǥ��
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && bulletCount > 0)
        {
            Fire();
        }
        else if (Input.GetButtonDown("Reload"))  // ������ ��ư�� ������ ������
        {
            Reload();
        }
    }

    void Fire()
    {
        Rigidbody shellInstance = Instantiate(prefabShell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = 50.0f * fireTransform.forward;
        aud.PlayOneShot(fireSE);
        bulletCount--;
        UpdateBulletCountUI();  // �Ѿ� ������ UI�� ������Ʈ
    }

    void Reload()
    {
        bulletCount = maxBullets;  // �Ѿ� ������ �ִ�ġ�� �缳��
        UpdateBulletCountUI();  // ������ �� �Ѿ� ������ UI�� ������Ʈ
    }

    void UpdateBulletCountUI()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Bullets: " + bulletCount;
        }
    }
}
