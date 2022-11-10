using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EarthComponent : MonoBehaviour
{
    public static EarthComponent ec;        // 전역 설정

    [SerializeField]
    int hp = 100;
    [SerializeField]
    int maxHp = 100;

    [SerializeField]
    Slider hpBar;

    [SerializeField]
    SpriteRenderer sr;  // 지구 이미지

    [SerializeField]
    GameObject fx;  // 폭파 파티클

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
        // 같은 게임 오브젝트 내에 있는 컴포넌트 중에 SpriteRenderer 컴포넌트를 찾아서 담아줌

        turret = transform.GetChild(0).gameObject;
        shield = transform.GetChild(1).gameObject;
        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
        // 해당하는 이름의 게임오브젝트를 하이어라키에 찾고 그 오브젝트에 있는 컴포넌트 중 Slider를 담아줌
        
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
            return; // 현재 실드 활성화 된 상태라면 함수를 빠져나감
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

    void SetHpBar() // hp바 설정 함수
    {
        hp = maxHp;
        hpBar.maxValue = hp;
        hpBar.value = hp;
    }

    void CreateFx() // 지구 터질 때 나오는 이펙트 생성 함수
    {
        GameObject temp = Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(temp, 0.5f);
    }

    public void ActiveHpBar()   // 게임 시작 시 Hp바 켜주는 함수
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
        turret.GetComponent<TurretComponent>().enabled = true;  // 터렛 컴포넌트 켜줌
        turret.GetComponentInChildren<SpriteRenderer>().enabled = true; // 터렛 이미지 컴포넌트 켜줌
        GameManager.gm.InfoItemEffect("Turret ON");
        yield return new WaitForSeconds(7);
        isTurret = false;
        turret.GetComponent<TurretComponent>().enabled = false; // 터렛 컴포넌트 꺼줌
        turret.GetComponentInChildren<SpriteRenderer>().enabled = false; // 터렛 이미지 컴포넌트 꺼줌
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
