using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawnComponent : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemy = new GameObject[3];

    [SerializeField]
    Rect spawnBoundary;     // �� ��ȯ ����

    [SerializeField]
    float spawnRate = 2;    // �� ��ȯ ����
    float time = 0;         // ���� üũ�� �ð�

    [SerializeField]
    float bossSpawnRate = 5;
    float bossSpawnTime = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //enemy[0] = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemy.prefab", typeof(GameObject));
        //enemy[1] = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Boss1.prefab", typeof(GameObject));
        //enemy[2] = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Boss2.prefab", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.isGameStart == false || GameManager.gm.isGameOver == true)
        {
            return;
        }

        time += Time.deltaTime;

        if(time >= spawnRate)
        {
            SpawnEnemy(0);   // ���� �Լ� ȣ��
        }

        bossSpawnTime += Time.deltaTime;

        if(bossSpawnTime >= bossSpawnRate)
        {
            SpawnEnemy(Random.Range(1, 3));
        }
    }

    void SpawnEnemy(int num)
    {
        int spawnPos = Random.Range(0, 4);  // Left, Right, Up, Down    ���� ��ġ ������ �ε��� ����
        Vector3 pos = Vector3.zero; // ���� ��ġ�� ����� ����

        if (spawnPos == 0)   // Up
        {
            pos = new Vector3(Random.Range(spawnBoundary.xMin, spawnBoundary.xMax), spawnBoundary.yMax);
        }
        else if (spawnPos == 1)  // Down
        {
            pos = new Vector3(Random.Range(spawnBoundary.xMin, spawnBoundary.xMax), spawnBoundary.yMin);
        }
        else if (spawnPos == 2)  // Left
        {
            pos = new Vector3(spawnBoundary.xMin, Random.Range(spawnBoundary.yMin, spawnBoundary.yMax));
        }
        else if (spawnPos == 3)  // Right
        {
            pos = new Vector3(spawnBoundary.xMax, Random.Range(spawnBoundary.yMin, spawnBoundary.yMax));
        }

        GameObject temp = Instantiate(enemy[num], pos, Quaternion.identity, transform);  // ���� ����

        if (num == 0)
        {
            time = 0;   // ���� �ð� üũ ���� �ʱ�ȭ
        }
        else
        {
            bossSpawnTime = 0;
        }
    }
}
