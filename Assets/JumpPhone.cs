using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class JumpPhone : MonoBehaviour
{
    public float jumpForce = 5f;
    public float rotationSpeed = 360f;
    public float jumpCooldown = 10f;
    public Color darkColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private bool canJump = true;
    private Rigidbody rb;
    private Renderer cubeRenderer;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cubeRenderer = GetComponent<Renderer>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (canJump && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (IsTouchingCube())
            {
                JumpAndRotateCoroutine();
                canJump = false;
                Invoke("EnableJump", jumpCooldown);
            }
        }
    }

    bool IsTouchingCube()
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = touch.position;

        if (raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Vector3 toCube = transform.position - hitPose.position;
            float distanceToCube = toCube.magnitude;

            if (distanceToCube < 0.1f) // 調整碰觸的距離
            {
                return true;
            }
        }

        return false;
    }

    void JumpAndRotateCoroutine()
    {
        StartCoroutine(JumpAndRotateSequence());
    }

    IEnumerator JumpAndRotateSequence()
    {
        animator.SetTrigger("Jump");
        // 繞一圈
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(Vector3.up * 360f);

        float elapsedTime = 0f;
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