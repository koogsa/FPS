using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TurretSpawn : MonoBehaviour
{
    public GameObject turret;  // ������ �ͷ� ������
    public Terrain spawnTerrain; // �ͷ��� ������ Terrain
    public GameObject player;  // �÷��̾� ������Ʈ

    public float spawnInterval = 2f; // ���� ���� (��)
    public int turretCount = 1;      // �� ���� ������ �ͷ� ����
    public int maxTurretCount = 10;  // �ִ� �ͷ� ����
    public float playerAvoidRange = 5f; // �÷��̾� �ֺ� ȸ�� ���� (5x5)

    private float terrainPosX;
    private float terrainPosZ;
    private float spawnTimer;
    private List<Vector3> spawnedPositions = new List<Vector3>(); // ������ ��ġ ����Ʈ

    void Start()
    {
        // Terrain�� ��ġ ����
        terrainPosX = spawnTerrain.transform.position.x;
        terrainPosZ = spawnTerrain.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // �ִ� �ͷ� ������ �����ϸ� �� �̻� �������� ����
        if (spawnedPositions.Count >= maxTurretCount)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        // ��ȯ ������ �����ϸ� �ͷ� ����
        if (spawnTimer >= spawnInterval)
        {
            for (int i = 0; i < turretCount; i++)
            {
                Vector3 spawnPosition = GetValidRandomPosition();
                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(turret, spawnPosition, Quaternion.identity); // �ͷ� ����
                    spawnedPositions.Add(spawnPosition); // ������ ��ġ�� ����Ʈ�� �߰�
                }
            }
            spawnTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    // ��ȿ�� ������ ��ġ�� ��ȯ
    Vector3 GetValidRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool validPositionFound = false;

        for (int attempt = 0; attempt < 30; attempt++) // �ִ� 30�� �õ�
        {
            float randomX = Random.Range(terrainPosX, terrainPosX + spawnTerrain.terrainData.size.x);
            float randomZ = Random.Range(terrainPosZ, terrainPosZ + spawnTerrain.terrainData.size.z);
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));
            randomPosition = new Vector3(randomX, y, randomZ);

            // �÷��̾� ȸ�� �Ÿ� üũ
            if (Vector3.Distance(randomPosition, player.transform.position) > playerAvoidRange)
            {
                validPositionFound = true;
                break;
            }
        }

        return validPositionFound ? randomPosition : Vector3.zero;
    }
}
