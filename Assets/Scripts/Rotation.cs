using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    void Update()
    {
        transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.deltaTime;
    }
}
