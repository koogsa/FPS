using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TurretSpawn : MonoBehaviour
{
    public GameObject turret;  // 생성할 터렛 프리팹
    public Terrain spawnTerrain; // 터렛이 생성될 Terrain
    public GameObject player;  // 플레이어 오브젝트

    public float spawnInterval = 2f; // 생성 간격 (초)
    public int turretCount = 1;      // 한 번에 생성할 터렛 개수
    public int maxTurretCount = 10;  // 최대 터렛 개수
    public float playerAvoidRange = 5f; // 플레이어 주변 회피 범위 (5x5)

    private float terrainPosX;
    private float terrainPosZ;
    private float spawnTimer;
    private List<Vector3> spawnedPositions = new List<Vector3>(); // 생성된 위치 리스트

    void Start()
    {
        // Terrain의 위치 저장
        terrainPosX = spawnTerrain.transform.position.x;
        terrainPosZ = spawnTerrain.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // 최대 터렛 개수에 도달하면 더 이상 생성하지 않음
        if (spawnedPositions.Count >= maxTurretCount)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        // 소환 간격이 도달하면 터렛 생성
        if (spawnTimer >= spawnInterval)
        {
            for (int i = 0; i < turretCount; i++)
            {
                Vector3 spawnPosition = GetValidRandomPosition();
                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(turret, spawnPosition, Quaternion.identity); // 터렛 생성
                    spawnedPositions.Add(spawnPosition); // 생성된 위치를 리스트에 추가
                }
            }
            spawnTimer = 0f; // 타이머 초기화
        }
    }

    // 유효한 무작위 위치를 반환
    Vector3 GetValidRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int attempt = 0; attempt < 30; attempt++) // 최대 30번 시도
        {
            float randomX = Random.Range(terrainPosX, terrainPosX + spawnTerrain.terrainData.size.x);
            float randomZ = Random.Range(terrainPosZ, terrainPosZ + spawnTerrain.terrainData.size.z);
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));
            randomPosition = new Vector3(randomX, y, randomZ);

            // 플레이어 회피 거리 체크
            if (Vector3.Distance(randomPosition, player.transform.position) > playerAvoidRange)
            {
                validPositionFound = true;
                break;
            }
        }

        return validPositionFound ? randomPosition : Vector3.zero;
    }
}
