using System.Collections;
using UnityEngine;

public class CleanPhone : MonoBehaviour
{
    public GameObject cube; // 方塊物體
    private Animator slimeAnimator;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Camera mainCamera;

    void Start()
    {
        slimeAnimator = cube.GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isDragging)
        {
            // 使用觸控系統處理手指拖動
            if (Input.touchCount > 0)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                touchPosition.z = transform.position.z; // 保持方塊在原來的深度上移動
                transform.position = touchPosition;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isDragging)
        {
            isDragging = true;
            initialPosition = transform.position;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = (transform.position - initialPosition) * 10f; // 調整拋出的速度
            rb.angularVelocity = Random.insideUnitSphere * 5f; // 調整拋出後的旋轉速度
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞的物體是否是 slime
        if (collision.gameObject == cube)
        {
            // 觸發 slime 的 animator 的 "Clean" 觸發器
            slimeAnimator.SetTrigger("Clean");
        }
    }
}