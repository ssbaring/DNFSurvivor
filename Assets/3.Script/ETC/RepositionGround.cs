using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionGround : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;         //타일맵 위치
        float diffX = Mathf.Abs(playerPos.x - myPos.x);         //플레이어 위치 - 타일맵 위치
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.InputVector;        //방향
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)           //X축거리 차이가 Y축거리 차이보다 클 시
                {
                    transform.Translate(Vector3.right * dirX * 40);       //40은 1 타일의 길이(20) * 2
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
        }
    }
}
