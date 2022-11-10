using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float rotSpeed = 0; // 비행기 회전 속도

    [SerializeField]
    Transform img;      // 비행기 이미지의 트랜스폼을 담아둘 변수

    //[SerializeField]
    //GameObject bullet;  // 총알 프리팹

    float time = 0;
    [SerializeField]
    float attackRate = 0.1f;

    AudioSource sound;

    bool isRapidFire = false;

    IEnumerator enumerator;

    BulletComponent bull;

    

    // Start is called before the first frame update
    void Start()
    {
        img = transform.GetChild(0).GetComponent<Transform>();
        // 자식 오브젝트 중 0번째에 있는 트랜스폼 컴포넌트를 가져옴

        //bullet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bullet.prefab", typeof(GameObject));
        // 프로젝트 디렉토리에 있는 파일에 접근해서 파일을 가져옴

        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.isGameStart == false || GameManager.gm.isGameOver == true)
        {
            return;
        }

        Move(); // 함수 호출


        time += Time.deltaTime;


        if (!isRapidFire)
        {
            if (Input.GetMouseButton(0))
            {
                if (time > attackRate)
                {
                    
                    time = 0;
                    ShootBullet();
                }
            }    
        }
        else
        {
            if (time > attackRate)
            {
                time = 0;
                ShootBullet();
            }
        }
    }


    void Move() // 회전하는 함수
    {
        transform.eulerAngles += new Vector3(0, 0, (-rotSpeed * Input.GetAxis("Horizontal") * Time.deltaTime));

        img.localEulerAngles = new Vector3(0, 0, Input.GetAxis("Horizontal") * -30);
    }

    void ShootBullet()
    {
        //GameObject temp = Instantiate(bullet, img.position, transform.rotation);
        BulletPoolComponent.bp.UseBullet(img.position, transform.rotation, isRapidFire);
        sound.Play();
        
    }


    public void GetRapidFireItem()  // 자동 공격 아이템 획득 시 호출될 메소드(함수)
    {
        if (isRapidFire)
        {
            StopCoroutine(enumerator);
        }
        enumerator = RapidFireDuration();
        StartCoroutine(enumerator);

    }
    
    IEnumerator RapidFireDuration()
    {
        GameManager.gm.InfoItemEffect("RapidFire ON");
        isRapidFire = true;
        yield return new WaitForSeconds(7);
        isRapidFire = false;
        GameManager.gm.InfoItemEffect("RapidFire OFF");
        
    }

}
