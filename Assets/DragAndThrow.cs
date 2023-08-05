using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndThrow : MonoBehaviour
{
    public GameObject foodPrefab; //

    private bool isDragging = false;
    private Rigidbody rb;
    private Transform initialParent;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialParent = transform.parent;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            rb.MovePosition(newPosition);
        }
    }

    private void OnMouseDown()
    {
        if (!isDragging)
        {
            isDragging = true;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            initialPosition = transform.position;

            // 在原地生成新的球體
            // GameObject newCube = Instantiate(foodPrefab, initialPosition, Quaternion.identity);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            rb.isKinematic = false;
            rb.velocity = (transform.position - initialPosition) * 10f; // 調整拋出的速度，這裡乘以 10 是為了增加拋出的力度
            rb.angularVelocity = Random.insideUnitSphere * 5f; // 調整拋出後的旋轉速度
            transform.parent = initialParent; // 放開後將球體放回原本的父物體，可以根據需要修改
        }
    }
}