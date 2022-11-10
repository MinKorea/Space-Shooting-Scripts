using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Boss2Component : EnemyComponent
{
    PolygonCollider2D col2;


    protected override void DeactivateCollider()
    {
        col2 = GetComponent<PolygonCollider2D>();
        col2.enabled = false;
    }


    protected override void Die()
    {
        base.Die();

        for (int i = 0; i <= 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        
    }

}
