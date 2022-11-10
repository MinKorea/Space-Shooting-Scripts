using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public int hp = 4;

    public void TakeDamage(int _dmg)
    {
        hp -= _dmg;

        if(hp <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()   // 가상 함수. 상속 받은 클래스 내에서 재정의 하기 위함
    {
        Destroy(gameObject);
    }


}
