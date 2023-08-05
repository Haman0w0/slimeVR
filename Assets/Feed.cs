using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{
    public GameObject slime; // Slime 物體

    private Animator slimeAnimator;

    void Start()
    {
        slimeAnimator = slime.GetComponent<Animator>();
    }

    // 當 Banana 物件與其他物體碰撞時，會調用這個方法
    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物體是否是 Slime
        if (collision.gameObject == slime)
        {
            // 觸發 Slime 上的 Feed 觸發器
            slimeAnimator.SetTrigger("Feed");

            // 碰撞到 Slime，使 Banana 物件消失
            Destroy(gameObject);
        }
    }
}