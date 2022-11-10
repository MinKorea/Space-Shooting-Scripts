using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnComponent : MonoBehaviour
{
    [SerializeField]
    GameObject[] items;     // 스폰될 아이템들

    [SerializeField]
    float spawnRate = 7;
    float time = 0;

    Vector3 pos;            // 스폰 위치
    float offset;           

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.isGameStart == false || GameManager.gm.isGameOver == true)
        {
            return;
        }

        time += Time.deltaTime;

        if(time >= spawnRate)
        {
            SpawnItem();    // 아이템 스폰 함수 호출
        }
    }

    void SpawnItem()    // 아이템 스폰 함수
    {
        int num = Random.Range(0, items.Length);    // 아이템 배열에 들어있는 아이템 중 랜덤하게 뽑기

        while (true)
        {
            pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); // 랜덤 위치 설정

            if (pos.x == 0 && pos.y == 0)
            {
                continue;
            }
            else
            {
                break;
            }
        }

        offset = Random.Range(3, 7);
        pos = pos.normalized * offset;

        

        GameObject item = Instantiate(items[num], pos, Quaternion.identity);  // 아이템 생성

        Destroy(item, 7);

        time = 0;   // 스폰 시간 초기화
    }

}
