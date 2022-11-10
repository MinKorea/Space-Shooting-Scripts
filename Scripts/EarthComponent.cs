using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EarthComponent : MonoBehaviour
{
    public static EarthComponent ec;        // ���� ����

    [SerializeField]
    int hp = 100;
    [SerializeField]
    int maxHp = 100;

    [SerializeField]
    Slider hpBar;

    [SerializeField]
    SpriteRenderer sr;  // ���� �̹���

    [SerializeField]
    GameObject fx;  // ���� ��ƼŬ

    AudioSource sound;

    IEnumerator enumerator;
    GameObject turret;
    GameObject shield;

    bool isTurret = false;
    bool isShield = false;


    // Start is called before the first frame update
    void Start()
    {
        ec = this;

        sr = GetComponent<SpriteRenderer>();
        // ���� ���� ������Ʈ ���� �ִ� ������Ʈ �߿� SpriteRenderer ������Ʈ�� ã�Ƽ� �����

        turret = transform.GetChild(0).gameObject;
        shield = transform.GetChild(1).gameObject;
        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
        // �ش��ϴ� �̸��� ���ӿ�����Ʈ�� ���̾��Ű�� ã�� �� ������Ʈ�� �ִ� ������Ʈ �� Slider�� �����
        
        SetHpBar();

        //fx = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/EarthDestroyParticle.prefab", typeof(GameObject));

        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _dmg)
    {
        if (isShield)
        {
            return; // ���� �ǵ� Ȱ��ȭ �� ���¶�� �Լ��� ��������
        }

        hp -= _dmg;
        hpBar.value = hp;
        CameraController.cm.Shake(0.3f, 0.3f, 50f);

        if (hp <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        sr.enabled = false;
        CreateFx();
        sound.Play();
        GameManager.gm.GameOver();
    }

    void SetHpBar() // hp�� ���� �Լ�
    {
        hp = maxHp;
        hpBar.maxValue = hp;
        hpBar.value = hp;
    }

    void CreateFx() // ���� ���� �� ������ ����Ʈ ���� �Լ�
    {
        GameObject temp = Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(temp, 0.5f);
    }

    public void ActiveHpBar()   // ���� ���� �� Hp�� ���ִ� �Լ�
    {
        for (int i = 0; i < hpBar.transform.childCount; i++)
        {
            hpBar.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void GetItemTurret()
    {
        if (isTurret)
        {
            StopCoroutine(enumerator);
        }

        enumerator = TurretDuration();
        StartCoroutine(enumerator);
    }

    IEnumerator TurretDuration()
    {
        isTurret = true;                                                    
        turret.GetComponent<TurretComponent>().enabled = true;  // �ͷ� ������Ʈ ����
        turret.GetComponentInChildren<SpriteRenderer>().enabled = true; // �ͷ� �̹��� ������Ʈ ����
        GameManager.gm.InfoItemEffect("Turret ON");
        yield return new WaitForSeconds(7);
        isTurret = false;
        turret.GetComponent<TurretComponent>().enabled = false; // �ͷ� ������Ʈ ����
        turret.GetComponentInChildren<SpriteRenderer>().enabled = false; // �ͷ� �̹��� ������Ʈ ����
        GameManager.gm.InfoItemEffect("Turret OFF");
    }

    public void GetItemShield()
    {
        if (isShield)
        {
            StopCoroutine(enumerator);
        }

        enumerator = ShieldDuration();
        StartCoroutine(enumerator);
    }

    IEnumerator ShieldDuration()
    {
        isShield = true;
        shield.GetComponent<SpriteRenderer>().enabled = true;
        GameManager.gm.InfoItemEffect("Shield ON");
        yield return new WaitForSeconds(7);
        shield.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.gm.InfoItemEffect("Shield OFF");
        isShield = false;

    }

    public void GetItemHp()
    {
        hp += 10;
        GameManager.gm.InfoItemEffect("HP++");
        if (hp >= maxHp)
        {
            hp = maxHp;

        }
        hpBar.value = hp;
    }

}
