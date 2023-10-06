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
        Vector3 myPos = transform.position;         //Ÿ�ϸ� ��ġ
        float diffX = Mathf.Abs(playerPos.x - myPos.x);         //�÷��̾� ��ġ - Ÿ�ϸ� ��ġ
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.InputVector;        //����
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)           //X��Ÿ� ���̰� Y��Ÿ� ���̺��� Ŭ ��
                {
                    transform.Translate(Vector3.right * dirX * 40);       //40�� 1 Ÿ���� ����(20) * 2
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
        }
    }
}
