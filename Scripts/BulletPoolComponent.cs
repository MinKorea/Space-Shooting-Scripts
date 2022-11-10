using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BulletPoolComponent : MonoBehaviour
{
    public static BulletPoolComponent bp;   // ��������

    [SerializeField]
    GameObject bullet;
    List<GameObject> bulletList = new List<GameObject>();   // �Ѿ˵��� ��Ƶ� ����Ʈ

    int currentBullet = 0;

    // Start is called before the first frame update
    void Start()
    {
        bp = this;


        //bullet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bullet.prefab", typeof(GameObject));
        // ������Ʈ ���丮�� �ִ� ���Ͽ� �����ؼ� ������ ������

        InitPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitPool()
    {
        for(int i = 0; i < 10; i++)
        {
            CreateBullet();                     
        }
    }

    public void UseBullet(Vector3 pos, Quaternion rot, bool isRapidFire)
    {
        //if(currentBullet >= bulletList.Count - 1)           // ���� ���� �Ѿ��� �ε����� ����Ʈ ���� ī��Ʈ���� ���ٸ�
        //{   
            if (transform.childCount == 0)        // ���࿡ ����Ʈ ���� ó�� �Ѿ��� ���̾��Ű���� �����ִٸ�  
            {
                CreateBullet();                         // �Ѿ� ���� �Լ� ȣ��
            }
        //    else
        //    {
        //        currentBullet = 0;                      // �Ѿ� �ε��� �ʱ�ȭ
        //    }
        //}

        float speed = 0;

        if (isRapidFire) speed = 20;
        else speed = 10;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).position = pos;
        transform.GetChild(0).rotation = rot;    
        transform.GetChild(0).GetComponent<BulletComponent>().MoveBullet(speed);
        transform.GetChild(0).parent = null;
        

        //bulletList[currentBullet].SetActive(true);      // �Ѿ� ����
        //bulletList[currentBullet].transform.position = pos; // �߻� ��ġ ���� 
        //bulletList[currentBullet].transform.rotation = rot; // ȸ�� ��ġ ����(�߻� ����)
        //bulletList[currentBullet].transform.parent = null;  // �θ� ����
        //bulletList[currentBullet].GetComponent<BulletComponent>().MoveBullet(); // �Ѿ� �߻� �޼ҵ�(�Լ�) ȣ��

        //currentBullet++;    // �Ѿ� �ε��� ����

    }

    void CreateBullet()
    {
        GameObject temp = Instantiate(bullet);      // �Ѿ� ����
        temp.SetActive(false);                      // �Ѿ� ���� ������Ʈ ����    
        temp.transform.SetParent(transform);        // �Ѿ��� �θ� ���ӿ�����Ʈ ����

        //bulletList.Add(temp);                       // ����Ʈ�� �Ѿ� ��Ƶ�
    }

    public void ReturnBulletPool(GameObject bullet) // �Ѿ� ��� �� Ǯ�� �ٽ� ��� �޼ҵ�(�Լ�)
    {
        bullet.SetActive(false);                    // �Ѿ� ����
        bullet.transform.SetParent(transform);      // �θ� �缳��
        bullet.transform.position = transform.position;     // �θ��� ��ġ�� �̵�

    }

}
