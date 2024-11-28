using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletController : MonoBehaviour
{
    public ParticleSystem bulletExplosion;
    public ParticleSystem enemyExplosion;
    public AudioClip deathSE;
    AudioSource aud;
    EnemyCount ec;
    public int bulletCount = 10;
    private Text bulletCountText;

    private void OnCollisionEnter(Collision other)
    {
        ParticleSystem fire = Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        fire.Play();

        if (other.gameObject.tag == "ENEMY")
        {
            GameObject ehp = other.gameObject.transform.GetChild(0).gameObject;
            GameObject canv = ehp.transform.GetChild(0).gameObject;
            GameObject hp = canv.transform.GetChild(1).gameObject;
            if (hp.GetComponent<Image>().fillAmount > 0f)
            {
                hp.GetComponent<Image>().fillAmount -= 0.5f;
            }

            if (hp.GetComponent<Image>().fillAmount <= 0f)
            {
                aud.PlayOneShot(deathSE);
                ParticleSystem deathEF = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
                deathEF.Play();
                ec.GetComponent<EnemyCount>().CountEnemy();
                Destroy(other.gameObject, 1.0f);
            }
        }
        Destroy(gameObject, 2.0f);
        Destroy(fire.gameObject, 2.0f);
    }

    void Start()
    {
        aud = GetComponent<AudioSource>();
        ec = GameObject.Find("EnemyCounter").GetComponent<EnemyCount>();
        UpdateBulletCountUI();
        GameObject bulletTextObject = GameObject.Find("BulletCountText");
    }
    void Update()
    {
        UpdateBulletCountUI();  // �Ѿ� ������ �ǽð����� ������Ʈ
    }
    public void Fire()
    {
        if (bulletCount > 0)
        {
            bulletCount--;
            UpdateBulletCountUI();
            // Fire ����� ������ �����ϴ� �ٸ� ��ũ��Ʈ���� �߻� ����� ȣ���մϴ�.
        }
    }
    void UpdateBulletCountUI()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Bullets: " + bulletCount;
        }
    }
}
