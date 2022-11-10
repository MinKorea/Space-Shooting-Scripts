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
        base.Die();     // �θ��� Die�Լ� ȣ��
    }

    protected override void DeactivateCollider()    // Override �θ�� ���� �̸��� �Լ� ��� �������� �� �Լ��� ȣ���
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
