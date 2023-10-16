using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Transform targetPos;

    private void Awake()
    {
        targetPos = target.transform;
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(targetPos.position.x, targetPos.position.y, -10);
    }
}
