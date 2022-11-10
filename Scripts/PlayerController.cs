using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float rotSpeed = 0; // ����� ȸ�� �ӵ�

    [SerializeField]
    Transform img;      // ����� �̹����� Ʈ�������� ��Ƶ� ����

    //[SerializeField]
    //GameObject bullet;  // �Ѿ� ������

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
        // �ڽ� ������Ʈ �� 0��°�� �ִ� Ʈ������ ������Ʈ�� ������

        //bullet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bullet.prefab", typeof(GameObject));
        // ������Ʈ ���丮�� �ִ� ���Ͽ� �����ؼ� ������ ������

        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gm.isGameStart == false || GameManager.gm.isGameOver == true)
        {
            return;
        }

        Move(); // �Լ� ȣ��


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


    void Move() // ȸ���ϴ� �Լ�
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


    public void GetRapidFireItem()  // �ڵ� ���� ������ ȹ�� �� ȣ��� �޼ҵ�(�Լ�)
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
