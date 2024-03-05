using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramingTransfort : MonoBehaviour
{
    public Transform target; // Đối tượng mục tiêu (người chơi)
    public float framingSpeed = 5f; // Tốc độ di chuyển của camera để ra khung
    public float distanceThreshold = 2f; // Khoảng cách tối thiểu giữa đối tượng mục tiêu và biên của khung
    public float framingMargin = 1.5f; // Margin để camera không nằm sát biên của khung

    private Camera mainCamera;
    private Vector3 currentVelocity;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (!target)
            return;

        // Tính toán vị trí mới cho camera
        Vector3 targetViewportPosition = mainCamera.WorldToViewportPoint(target.position);
        Vector3 newPosition = mainCamera.transform.position;

        // Kiểm tra xem đối tượng mục tiêu có nằm ngoài khung của camera không
        if (targetViewportPosition.x < 0f || targetViewportPosition.x > 1f ||
            targetViewportPosition.y < 0f || targetViewportPosition.y > 1f)
        {
            // Tính toán vị trí mới để đưa đối tượng mục tiêu về giữa khung camera
            float targetX = Mathf.Clamp01(targetViewportPosition.x);
            float targetY = Mathf.Clamp01(targetViewportPosition.y);
            Vector3 targetWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(targetX, targetY, mainCamera.nearClipPlane));
            Vector3 targetDirection = (targetWorldPosition - mainCamera.transform.position).normalized;
            newPosition += targetDirection * framingSpeed * Time.deltaTime;
        }

        // Lerp để di chuyển mượt mà camera
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, newPosition, ref currentVelocity, 0.1f);
    }
}
