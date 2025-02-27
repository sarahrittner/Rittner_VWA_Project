using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 0, -10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
