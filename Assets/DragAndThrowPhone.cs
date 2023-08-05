using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndThrowPhone : MonoBehaviour
{
    public float throwForceMultiplier = 10f;

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
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = touch.position;
                touchPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                rb.MovePosition(newPosition);
            }
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

            // 在原地生成新的物體
            // GameObject newCube = Instantiate(foodPrefab, initialPosition, Quaternion.identity);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            rb.isKinematic = false;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = touch.position;
                touchPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                Vector3 throwDirection = (Camera.main.ScreenToWorldPoint(touchPosition) - initialPosition).normalized;
                rb.velocity = throwDirection * touch.deltaPosition.magnitude * throwForceMultiplier;
            }

            rb.angularVelocity = Random.insideUnitSphere * 5f;
            transform.parent = initialParent;
        }
    }
}