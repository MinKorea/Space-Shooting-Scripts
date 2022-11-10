using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRapidFire : ItemComponent
{
    protected override void Dead()  // 상속받은 함수를 재정의
    {
        GameObject.Find("PlayerShip").GetComponent<PlayerController>().GetRapidFireItem();
        base.Dead();
    }

    
}
