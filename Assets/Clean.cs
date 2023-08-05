using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject cube; // 方塊物體

    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Animator slimeAnimator;

    void Start()
    {
        slimeAnimator = cube.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = newPosition;
        }
    }

    private void OnMouseDown()
    {
        if (!isDragging)
        {
            isDragging = true;
            initialPosition = transform.position;

            // 停止 Soap 的運動
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 取消 Soap 的重力
            rb.useGravity = false;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            // 在放開滑鼠時重新啟用 Soap 的重力
            rb.useGravity = true;

            // 設置 Soap 的速度和旋轉速度，以使它被拋出
            rb.velocity = (transform.position - initialPosition) * 10f;
            rb.angularVelocity = Random.insideUnitSphere * 5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物體是否是 cube
        if (collision.gameObject == cube)
        {
            // 觸發 cube 的 animator 的 "Clean" 觸發器
            Animator cubeAnimator = cube.GetComponent<Animator>();
            if (cubeAnimator != null)
            {
                cubeAnimator.SetTrigger("Clean");
            }
        }
    }
}