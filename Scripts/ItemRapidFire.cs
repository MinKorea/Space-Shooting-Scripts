using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRapidFire : ItemComponent
{
    protected override void Dead()  // ��ӹ��� �Լ��� ������
    {
        GameObject.Find("PlayerShip").GetComponent<PlayerController>().GetRapidFireItem();
        base.Dead();
    }

    
}
