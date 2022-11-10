using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cm;  // ���� ����. ���� �������� �ʾƵ� ��𼭵� ������ �����ϵ���

    Transform ship;         // ����� �̹����� Ʈ�������� ��Ƶ� ����
    float lerpSpeed = 10;   // ī�޶� ����⸦ ���󰡴� �ӵ�
    float dist = 0.8f;      // ī�޶� ����⸦ �������� ���󰡴� �Ÿ�

    bool isShaking = false; // ī�޶� ����ŷ ������ �ƴ��� �Ǵ��� ����
    bool isSizeChange = false;  // ī�޶� ������ ������ �ƴ��� �Ǵ��� ����
    public bool SIZE { set { isSizeChange = value; } }  // ī�޶� ������ ����

    float time = 0;         // ī�޶� ���� �ð�


    // Start is called before the first frame update
    void Start()
    {
        cm = this;  // �ڱ��ڽ��� ������ ����

        ship = GameObject.Find("Shipimg").GetComponent<Transform>();
        // ���̾��Ű�� �ִ� ������Ʈ �� Shipimg��� ���ӿ�����Ʈ�� ã�Ƽ�
        // �� ������Ʈ�� �ִ� Ʈ������ ������Ʈ�� �����
    }

    // Update is called once per frame
    void Update()
    {
        if (isSizeChange)
        {
            GameStartChangeSize();
        }
    }

    private void LateUpdate()
    {
        if (!GameManager.gm.isGameStart)
        {
            return;
        }

        FollowShip();   // �Լ� ȣ��

    }

    void FollowShip()   // ����� ����ٴϴ� �Լ�
    {
        transform.position = Vector3.Lerp(transform.position, ship.position * dist, lerpSpeed * Time.deltaTime);
    }

    public void Shake(float dur, float amount, float intensity)
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCam(dur, amount, intensity));
        }
    }

    IEnumerator ShakeCam(float dur, float amount, float intensity)  // �ڷ�ƾ �޼ҵ�(�Լ�), �ð� ��� ����
    {
        float t = dur;
        Vector3 oriPos = Camera.main.transform.localPosition;       // ī�޶��� ���� ��ġ�� ��Ƶ�
        Vector3 tarPos = Vector3.zero;                              // ī�޶� ������ Ÿ�� ��ġ
        isShaking = true;

        while(t > 0)
        {
            if(tarPos == Vector3.zero)
            {
                tarPos = oriPos + (Random.insideUnitSphere * amount);   // Ƽ�� ��ġ �缳��
            }

            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, tarPos, intensity * Time.deltaTime); // Ÿ�� ��ġ�� �̵�

            if(Vector3.Distance(Camera.main.transform.localPosition, tarPos) < 0.02f)    // ���� ī�޶� ��ġ�� Ÿ���� ��ġ�� 0.02f���� �۴ٸ�
            {
                tarPos = Vector3.zero;      // Ÿ����ġ �ʱ�ȭ
            }

            t -= Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = oriPos;
        isShaking = false;
    }

    void GameStartChangeSize()
    {
        time += Time.deltaTime;

        Camera.main.orthographicSize = Mathf.Lerp(10, 5, time);

        if(time >= 1)
        {
            isSizeChange = false;
            GameManager.gm.GameStart();
        }
    }

}
