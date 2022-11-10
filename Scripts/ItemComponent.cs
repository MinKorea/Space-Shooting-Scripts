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

    protected virtual void Dead()   // ���� �Լ�. ��� ���� Ŭ���� ������ ������ �ϱ� ����
    {
        Destroy(gameObject);
    }


}
