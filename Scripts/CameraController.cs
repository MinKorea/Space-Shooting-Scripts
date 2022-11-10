using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cm;  // 전역 설정. 굳이 생성하지 않아도 어디서든 접근이 가능하도록

    Transform ship;         // 비행기 이미지의 트랜스폼을 담아둘 변수
    float lerpSpeed = 10;   // 카메라가 비행기를 따라가는 속도
    float dist = 0.8f;      // 카메라가 비행기를 떨어져서 따라가는 거리

    bool isShaking = false; // 카메라가 쉐이킹 중인지 아닌지 판단할 변수
    bool isSizeChange = false;  // 카메라 사이즈 중인지 아닌지 판단할 변수
    public bool SIZE { set { isSizeChange = value; } }  // 카메라 사이즈 세터

    float time = 0;         // 카메라 연출 시간


    // Start is called before the first frame update
    void Start()
    {
        cm = this;  // 자기자신을 변수에 연결

        ship = GameObject.Find("Shipimg").GetComponent<Transform>();
        // 하이어라키에 있는 오브젝트 중 Shipimg라는 게임오브젝트를 찾아서
        // 그 오브젝트에 있는 트랜스폼 컴포넌트를 담아줌
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

        FollowShip();   // 함수 호출

    }

    void FollowShip()   // 비행기 따라다니는 함수
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

    IEnumerator ShakeCam(float dur, float amount, float intensity)  // 코루틴 메소드(함수), 시간 제어에 용이
    {
        float t = dur;
        Vector3 oriPos = Camera.main.transform.localPosition;       // 카메라의 원래 위치를 담아둠
        Vector3 tarPos = Vector3.zero;                              // 카메라가 움직일 타겟 위치
        isShaking = true;

        while(t > 0)
        {
            if(tarPos == Vector3.zero)
            {
                tarPos = oriPos + (Random.insideUnitSphere * amount);   // 티겟 위치 재설정
            }

            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, tarPos, intensity * Time.deltaTime); // 타겟 위치로 이동

            if(Vector3.Distance(Camera.main.transform.localPosition, tarPos) < 0.02f)    // 현재 카메라 위치와 타겟의 위치가 0.02f보다 작다면
            {
                tarPos = Vector3.zero;      // 타겟위치 초기화
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
