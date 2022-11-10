using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BulletPoolComponent : MonoBehaviour
{
    public static BulletPoolComponent bp;   // 전역설정

    [SerializeField]
    GameObject bullet;
    List<GameObject> bulletList = new List<GameObject>();   // 총알들을 담아둘 리스트

    int currentBullet = 0;

    // Start is called before the first frame update
    void Start()
    {
        bp = this;


        //bullet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bullet.prefab", typeof(GameObject));
        // 프로젝트 디렉토리에 있는 파일에 접근해서 파일을 가져옴

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
        //if(currentBullet >= bulletList.Count - 1)           // 현재 사용될 총알의 인덱스가 리스트 안의 카운트보다 높다면
        //{   
            if (transform.childCount == 0)        // 만약에 리스트 안의 처음 총알이 하이어라키에서 켜져있다면  
            {
                CreateBullet();                         // 총알 생성 함수 호출
            }
        //    else
        //    {
        //        currentBullet = 0;                      // 총알 인덱스 초기화
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
        

        //bulletList[currentBullet].SetActive(true);      // 총알 켜줌
        //bulletList[currentBullet].transform.position = pos; // 발사 위치 설정 
        //bulletList[currentBullet].transform.rotation = rot; // 회전 위치 설정(발사 방향)
        //bulletList[currentBullet].transform.parent = null;  // 부모 제거
        //bulletList[currentBullet].GetComponent<BulletComponent>().MoveBullet(); // 총알 발사 메소드(함수) 호출

        //currentBullet++;    // 총알 인덱스 증가

    }

    void CreateBullet()
    {
        GameObject temp = Instantiate(bullet);      // 총알 생성
        temp.SetActive(false);                      // 총알 게임 오브젝트 꺼둠    
        temp.transform.SetParent(transform);        // 총알의 부모 게임오브젝트 설정

        //bulletList.Add(temp);                       // 리스트에 총알 담아둠
    }

    public void ReturnBulletPool(GameObject bullet) // 총알 사용 후 풀에 다시 담는 메소드(함수)
    {
        bullet.SetActive(false);                    // 총알 꺼줌
        bullet.transform.SetParent(transform);      // 부모 재설정
        bullet.transform.position = transform.position;     // 부모의 위치로 이동

    }

}
