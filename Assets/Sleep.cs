using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    private Vector3 originalScale;
    private Animator animator;

    void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
        animator.SetBool("Sleep", false);
    }

    void Update()
    {
        // 在指定的範圍內進行翻轉和改變大小的操作
        if (IsInTargetArea())
        {
            animator.SetBool("Sleep", true);
        }
        else
        {
            // 不在指定範圍內時還原方塊的大小和旋轉
            animator.SetBool("Sleep", false);
        }
    }

    // 檢查方塊是否在目標區域內
    private bool IsInTargetArea()
    {
        float x = transform.position.x;
        float z = transform.position.z;

        return (x >= 2.5f && x <= 4.5f) && (z >= 2.5f && z <= 4.5f);
    }
}