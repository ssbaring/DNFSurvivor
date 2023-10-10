using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] target;
    public Transform nearestTaget;

    private void FixedUpdate()
    {
        target = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);  //���� ĳ��Ʈ�� ��ȯ
        nearestTaget = GetNearest();

    }
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in target)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curdiff = Vector3.Distance(myPos, targetPos);

            if(curdiff <diff)       //���� ����� ������Ʈ ������ ��ü
            {
                diff = curdiff;
                result = target.transform;
            }
        }
        return result;
    }
}
