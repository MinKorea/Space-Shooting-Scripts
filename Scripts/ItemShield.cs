using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : ItemComponent
{
    protected override void Dead()  // 상속받은 함수를 재정의
    {
        EarthComponent.ec.GetItemShield();
        base.Dead();
    }
}
