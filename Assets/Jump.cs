using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 5f;
    public float rotationSpeed = 360f;
    public float jumpCooldown = 10f;

    private bool canJump = true;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 當點擊螢幕時進行射線檢測，並只在點擊到史萊姆時觸發跳躍和旋轉
        if (canJump && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                JumpAndRotateCoroutine();
                canJump = false;
                Invoke("EnableJump", jumpCooldown);
            }
        }
    }

    void JumpAndRotateCoroutine()
    {
        StartCoroutine(JumpAndRotateSequence());
    }

    IEnumerator JumpAndRotateSequence()
    {
        animator.SetTrigger("Jump");
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(Vector3.up * 360f);
        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    void EnableJump()
    {
        canJump = true;
    }
}