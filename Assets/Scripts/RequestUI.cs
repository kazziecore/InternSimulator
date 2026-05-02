using UnityEngine;

public class RequestUI : MonoBehaviour
{
    void LateUpdate()
{
    transform.forward = Camera.main.transform.forward; // this just makes the request ui face the camera lol
}
}
