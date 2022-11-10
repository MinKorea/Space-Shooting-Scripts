using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnComponent : MonoBehaviour
{
    [SerializeField]
    GameObject[] items;     // ������ �����۵�

    [SerializeField]
    float spawnRate = 7;
    float time = 0;

    Vector3 pos;            // ���� ��ġ
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
            SpawnItem();    // ������ ���� �Լ� ȣ��
        }
    }

    void SpawnItem()    // ������ ���� �Լ�
    {
        int num = Random.Range(0, items.Length);    // ������ �迭�� ����ִ� ������ �� �����ϰ� �̱�

        while (true)
        {
            pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); // ���� ��ġ ����

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

        

        GameObject item = Instantiate(items[num], pos, Quaternion.identity);  // ������ ����

        Destroy(item, 7);

        time = 0;   // ���� �ð� �ʱ�ȭ
    }

}
