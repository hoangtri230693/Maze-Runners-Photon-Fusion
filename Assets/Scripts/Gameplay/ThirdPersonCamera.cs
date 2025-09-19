using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 4f;
    public float CameraDistance = 4f;
    public float MinDistance = 0.5f;
    public float CameraRadius = 0.2f;
    public LayerMask HitMask;

    private float verticalRotation;
    private float horizontalRotation;

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);
        horizontalRotation += mouseX * MouseSensitivity;

        Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

        //--- Tính vị trí mong muốn ---
        Vector3 desiredPosition = Target.position + rotation * new Vector3(0.2f, 1.365f, -CameraDistance);

        //--- Raycast chống clipping ---
        Vector3 direction = (desiredPosition - Target.position).normalized;
        if (Physics.SphereCast(Target.position, CameraRadius, direction, out RaycastHit hit, CameraDistance, HitMask))
        {
            float hitDistance = Mathf.Max(hit.distance, MinDistance);
            desiredPosition = Target.position + direction * hitDistance;
        }

        //--- Gán vị trí + rotation ---
        transform.position = desiredPosition;
        transform.rotation = rotation;
    }
}
