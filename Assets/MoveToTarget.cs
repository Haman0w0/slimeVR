using System.Collections;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動的速度

    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float doubleClickTime = 0.3f; // 雙擊的時間閾值
    private bool canClick = true;

    void Update()
    {
        // 滑鼠點擊時設定目標位置
        if (Input.GetMouseButtonDown(0))
        {
            // 雙擊檢測
            if (canClick)
            {
                canClick = false;
                StartCoroutine(DoubleClickDetection());
            }
            else
            {
                StopCoroutine(DoubleClickDetection());
                canClick = true;
                MoveToTargetPosition();
            }
        }

        // 如果正在移動，則進行移動
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // 判斷是否到達目標位置，停止移動
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                isMoving = false;
            }
        }
    }

    void MoveToTargetPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point; // 點擊位置作為目標位置
            targetPosition.y = transform.position.y; // 保持方塊在原來的高度上移動
            initialPosition = transform.position;
            isMoving = true; // 開始移動
        }
    }

    IEnumerator DoubleClickDetection()
    {
        yield return new WaitForSeconds(doubleClickTime);
        canClick = true;
    }
}