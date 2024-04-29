using UnityEngine;

public class RotateByInputAxis : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 5f;
    [SerializeField] string InputAxisName = "Horizontal";

    void Update()
    {
        float InputDirection = Input.GetAxis(InputAxisName);
        transform.rotation *= Quaternion.Euler(0f, 0f, InputDirection * RotationSpeed * Time.deltaTime);
    }
}
