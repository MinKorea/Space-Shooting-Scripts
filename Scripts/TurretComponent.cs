using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    [SerializeField]
    float tSpeed = 100;

    [SerializeField]
    float attackRate = 0.1f;
    float time = 0;

    GameObject ship;


    // Start is called before the first frame update
    void Start()
    {
        ship = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.isGameStart == false || GameManager.gm.isGameOver == true)
        {
            return;
        }

        MoveRotate();   // ȸ�� �̵� �Լ� ȣ��!!

        time += Time.deltaTime;

        if(time >= attackRate)
        {
            Shoot();
        }
    }

    void MoveRotate()   // ȸ�� �̵� �Լ�
    {
        transform.eulerAngles += new Vector3(0, 0, tSpeed * Time.deltaTime);
    }

    void Shoot()    // �Ѿ� �߻� �Լ�
    {
        BulletPoolComponent.bp.UseBullet(ship.transform.position, ship.transform.rotation, true);
        time = 0;
    }
}
