using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float yOffset = 3f;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = new Vector3(0f, target.position.y + yOffset, transform.position.z);

    }
}