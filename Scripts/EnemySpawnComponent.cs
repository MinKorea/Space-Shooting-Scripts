using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawnComponent : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemy = new GameObject[3];

    [SerializeField]
    Rect spawnBoundary;     // 적 소환 영역

    [SerializeField]
    float spawnRate = 2;    // 적 소환 간격
    float time = 0;         // 간격 체크할 시간

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
            SpawnEnemy(0);   // 스폰 함수 호출
        }

        bossSpawnTime += Time.deltaTime;

        if(bossSpawnTime >= bossSpawnRate)
        {
            SpawnEnemy(Random.Range(1, 3));
        }
    }

    void SpawnEnemy(int num)
    {
        int spawnPos = Random.Range(0, 4);  // Left, Right, Up, Down    스폰 위치 구분할 인덱스 변수
        Vector3 pos = Vector3.zero; // 스폰 위치가 저장될 변수

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

        GameObject temp = Instantiate(enemy[num], pos, Quaternion.identity, transform);  // 적기 생성

        if (num == 0)
        {
            time = 0;   // 스폰 시간 체크 변수 초기화
        }
        else
        {
            bossSpawnTime = 0;
        }
    }
}
