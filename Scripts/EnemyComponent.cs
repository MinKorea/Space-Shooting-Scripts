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
    protected GameObject fx;  // ���� ��ƼŬ
    protected SpriteRenderer sr;  // ����� �̹���

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
        Vector3 dir = transform.position - Vector3.zero;    // �̵� ����

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;    // ���� ��ȯ
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Earth"))
        {
            // ������ ������ �ִ� �Լ� ȣ��
            EarthComponent.ec.TakeDamage(dmg);
            Die();
        }

    }
    
    public void TakeDamage(int _dmg)
    {
        hp -= _dmg; // �������� �縸ŭ hp ���� ������
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

    protected void CreateFx() // ����� ���� �� ����Ʈ ���� �Լ�
    {
        GameObject temp = Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(temp, 0.5f);
    }

    IEnumerator HitColorChange()    // �ǰ� �� ����� ���� �ٲ��ִ� �Լ�
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;
    }

}
