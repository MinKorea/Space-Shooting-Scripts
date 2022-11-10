using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField]
    int hp = 4;
    [SerializeField]
    float speed = 4;
    [SerializeField]
    int dmg = 2;
    [SerializeField]
    int score = 0;

    [SerializeField]
    protected GameObject fx;  // 폭파 파티클
    protected SpriteRenderer sr;  // 비행기 이미지

    protected AudioSource sound;
    [SerializeField]
    protected AudioClip[] clips = new AudioClip[2];

    CircleCollider2D col;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        //fx = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/EnemyDestroyParticle.prefab", typeof(GameObject));

        sr = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.isGameStart == false || GameManager.gm.isGameOver == true)
        {
            return;
        }

        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
        SetAngle();
    }

    void SetAngle()
    {
        Vector3 dir = transform.position - Vector3.zero;    // 이동 방향

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;    // 각도 변환
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Earth"))
        {
            // 지구에 데미지 주는 함수 호출
            EarthComponent.ec.TakeDamage(dmg);
            Die();
        }

    }
    
    public void TakeDamage(int _dmg)
    {
        hp -= _dmg; // 데미지의 양만큼 hp 감소 시켜줌
        StartCoroutine(HitColorChange());

        if (hp <= 0)
        {
            Die();
            
        }
        else
        {
            sound.Play();
        }
    }

    protected virtual void Die()
    {
        sound.clip = clips[1];
        sound.Play();
        CreateFx();
        sr.enabled = false;
        GameManager.gm.GetScore(score);
        DeactivateCollider();

        Destroy(gameObject, 0.5f);
    }

    protected virtual void DeactivateCollider()
    {
        col.enabled = false;
    }

    protected void CreateFx() // 비행기 터질 때 이펙트 생성 함수
    {
        GameObject temp = Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(temp, 0.5f);
    }

    IEnumerator HitColorChange()    // 피격 시 비행기 색상 바꿔주는 함수
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;
    }

}
