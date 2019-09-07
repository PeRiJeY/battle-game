using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    public Transform target;

    public Vector3 offsetPosition;

    public bool lookAt = true;

    public float moveSpeed = 0.1f;
    public float rotationSpeed = 1.0f;

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            return;
        }


        Vector3 desiredPosition = target.TransformPoint(offsetPosition);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveSpeed);


        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            // transform.rotation = target.rotation;

            /*float targetY = transform.rotation.y + ((target.rotation.y - transform.rotation.y) * rotationInterpolationSpeed * Time.deltaTime);
            float targetW = transform.rotation.w + ((target.rotation.w - transform.rotation.w) * rotationInterpolationSpeed * Time.deltaTime);
            // Debug.Log(transform.rotation.y + " - " + target.rotation.y + " - " + targetY);
            transform.rotation = new Quaternion(target.rotation.x, targetY, target.rotation.z, targetW);*/

            Quaternion desiredRotation = target.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed);
        }
    }
}
