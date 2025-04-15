using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // 플레이어를 향해 회전 (반대 방향 문제 해결)
            transform.LookAt(playerTransform);
            transform.Rotate(0, 180, 0); // 정면이 아닌 뒷면이 보이는 경우 180도 회전
        }
    }
}
