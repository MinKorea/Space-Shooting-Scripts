using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    int hp = 0;
    [SerializeField]
    float rotSpeed = 0;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetHp();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    void SetHp()    // hp���� �Լ�
    {
        hp = Random.Range(10, 20);
    }

    void Rotation()
    {
        transform.eulerAngles += new Vector3(0, 0, rotSpeed * Time.deltaTime);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        StartCoroutine(HitColorChange());

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator HitColorChange()    // �ǰ� �� �ǰ� ��� ���� �ٲ��ִ� �Լ�
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        sr.color = new Color(1, 0.991630f, 0.504717f);
    }

}
