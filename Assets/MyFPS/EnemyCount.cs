using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyCount : MonoBehaviour
{
    int eCount;
    public Text enemyCountText;
    // Start is called before the first frame update
    void Start()
    {
        eCount = 5;
    }

    // Update is called once per frame
    public void CountEnemy()
    {
        eCount--;
        Debug.Log("eCount= " + eCount);
    }
    void Update()
    {
        enemyCountText.text = "남은 적의 수: " + eCount;
    }
}

