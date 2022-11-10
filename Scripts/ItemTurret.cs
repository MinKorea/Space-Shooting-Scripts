using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTurret : ItemComponent
{
    protected override void Dead()
    {
        EarthComponent.ec.GetItemTurret();
        base.Dead();
    }
}
