using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : ItemComponent
{
    protected override void Dead()  // ��ӹ��� �Լ��� ������
    {
        EarthComponent.ec.GetItemShield();
        base.Dead();
    }
}
