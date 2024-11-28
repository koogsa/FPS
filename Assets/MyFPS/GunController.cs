using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Text UI를 사용하기 위해 추가

public class GunController : MonoBehaviour
{
    public Rigidbody prefabShell;
    public Transform fireTransform;
    public AudioClip fireSE;
    public int bulletCount = 10;  // 현재 총알 개수
    public int maxBullets = 10;   // 최대 총알 개수
    public Text bulletCountText;  // 총알 개수를 표시하는 UI
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        UpdateBulletCountUI();  // 초기 총알 개수를 UI에 표시
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && bulletCount > 0)
        {
            Fire();
        }
        else if (Input.GetButtonDown("Reload"))  // 재장전 버튼을 누르면 재장전
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
        UpdateBulletCountUI();  // 총알 개수를 UI에 업데이트
    }

    void Reload()
    {
        bulletCount = maxBullets;  // 총알 개수를 최대치로 재설정
        UpdateBulletCountUI();  // 재장전 후 총알 개수를 UI에 업데이트
    }

    void UpdateBulletCountUI()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Bullets: " + bulletCount;
        }
    }
}
