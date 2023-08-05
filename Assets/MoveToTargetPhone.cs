using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetPhone : MonoBehaviour
{
    public float moveSpeed = 5f;

    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float doubleClickTime = 0.3f;
    private bool canClick = true;

    void Update()
    {
        // 手機觸控檢測
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 檢測雙擊事件
            if (touch.tapCount == 2)
            {
                if (canClick)
                {
                    canClick = false;
                    StartCoroutine(DoubleClickDetection());
                }
                else
                {
                    StopCoroutine(DoubleClickDetection());
                    canClick = true;
                    MoveToTargetPosition(touch.position);
                }
            }
        }

        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                isMoving = false;
            }
        }
    }

    void MoveToTargetPosition(Vector2 screenPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            initialPosition = transform.position;
            isMoving = true;
        }
    }

    IEnumerator DoubleClickDetection()
    {
        yield return new WaitForSeconds(doubleClickTime);
        canClick = true;
    }
}