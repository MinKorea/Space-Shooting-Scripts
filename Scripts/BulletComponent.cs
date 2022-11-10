using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BulletComponent : MonoBehaviour
{

    [SerializeField]
    int dmg = 1;
    [SerializeField]
    public float speed = 10;

    Rigidbody2D rb;
    [SerializeField]
    GameObject fx;  // 총알 히트 이펙트

    TrailRenderer tr;   // 꼬리 이펙트

    void Start()
    {
        
    }

    public void MoveBullet(float _speed)
    {        

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if(tr == null)
        {
            tr = GetComponentInChildren<TrailRenderer>();
        }

        Vector2 dir = transform.position.normalized * _speed;

        rb.velocity = dir;

        Invoke("DestroyBullet", 3); // 이름의 함수가 3초 뒤에 호출

        //fx = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BulletParticle.prefab", typeof(GameObject));


        
    }
    void DestroyBullet()
    {
        if (IsInvoking())
        {
            CancelInvoke("DestroyBullet");
        }

        tr.Clear();

        BulletPoolComponent.bp.ReturnBulletPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyComponent>().TakeDamage(dmg);
            
        }
        else if (col.CompareTag("Item"))
        {
            col.GetComponent<ItemComponent>().TakeDamage(dmg);
        }
        else if (col.CompareTag("EnemyShield"))
        {
            col.GetComponent<EnemyShield>().TakeDamage(dmg);
        }


        CancelInvoke("DestroyBullet");
        CreateParticle();
        DestroyBullet();

        CameraController.cm.Shake(0.1f, 0.15f, 30f);
    }

    void CreateParticle()
    {
        GameObject temp = Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(temp, 0.5f);
    }

}
