using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHp : ItemComponent
{
    protected override void Dead()
    {
        EarthComponent.ec.GetItemHp();
        base.Dead();
    }
}
