using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Boss1Component : EnemyComponent
{
    PolygonCollider2D col1;

    [SerializeField]
    GameObject bossChild;


    protected override void Die()
    {
        CreateChild();
        base.Die();     // 부모의 Die함수 호출
    }

    protected override void DeactivateCollider()    // Override 부모와 같은 이름의 함수 대신 재정의한 이 함수가 호출됨
    {
        col1 = GetComponent<PolygonCollider2D>();
        col1.enabled = false;
    }

    void CreateChild()
    {
        
        Vector3 pos = transform.position;


        Instantiate(bossChild, new Vector3(pos.x + 0.5f, pos.y, pos.z), Quaternion.identity);
        Instantiate(bossChild, new Vector3(pos.x - 0.5f, pos.y, pos.z), Quaternion.identity);
        Instantiate(bossChild, new Vector3(pos.x, pos.y + 0.5f, pos.z), Quaternion.identity);
    }

}
