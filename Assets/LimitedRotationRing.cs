using UnityEngine;

public class RingRotationController : MonoBehaviour
{
    [Header("Parent Object")]
    public Transform parentObject; // The object to which the ring is attached.

    [Header("Rotation Limits")]
    public Vector2 rotationXLimit = new Vector2(-30, 30); // Limits for X-axis rotation.
    public Vector2 rotationYLimit = new Vector2(-30, 30); // Limits for Y-axis rotation.

    [Header("Rotation Sensitivity")]
    public float rotationSpeed = 5f; // Speed at which the ring reacts to parent movement.

    private Quaternion initialParentRotation; // Stores the initial rotation of the parent.
    private Quaternion initialRingRotation;   // Stores the initial rotation of the ring.

    void Start()
    {
        if (parentObject == null)
        {
            Debug.LogError("Parent Object is not assigned!");
            return;
        }

        // Capture the initial rotations of the parent and the ring.
        initialParentRotation = parentObject.rotation;
        initialRingRotation = transform.rotation;
    }

    void Update()
    {
        if (parentObject == null) return;

        // Calculate the rotation difference between the current and initial parent rotation.
        Quaternion parentRotationDelta = Quaternion.Inverse(initialParentRotation) * parentObject.rotation;

        // Apply the delta rotation to the ring's initial rotation.
        Quaternion targetRotation = initialRingRotation * parentRotationDelta;

        // Extract the target rotation's local Euler angles.
        Vector3 targetEulerAngles = targetRotation.eulerAngles;

        // Normalize angles to -180 to 180 range.
        targetEulerAngles.x = NormalizeAngle(targetEulerAngles.x);
        targetEulerAngles.y = NormalizeAngle(targetEulerAngles.y);

        // Clamp rotation based on the defined limits.
        targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, rotationXLimit.x, rotationXLimit.y);
        targetEulerAngles.y = Mathf.Clamp(targetEulerAngles.y, rotationYLimit.x, rotationYLimit.y);

        // Create a clamped rotation.
        Quaternion clampedRotation = Quaternion.Euler(targetEulerAngles);

        // Smoothly interpolate to the clamped rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, clampedRotation, Time.deltaTime * rotationSpeed);
    }

    // Normalize angles to -180 to 180 range.
    private float NormalizeAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return angle;
    }
}
