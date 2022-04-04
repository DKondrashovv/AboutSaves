using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private enum RotationAxes
    {
        MouseX = 0,
        MouseY = 1
    };
    
    [SerializeField] private RotationAxes axes;
    [SerializeField] private float sensitivityX = 2.0f;
    [SerializeField] private float sensitivityY = 2.0f;
    [SerializeField] private float minimumY = -360.0f;
    [SerializeField] private float maximumY = 360.0f;
    
    private float _rotationX;
    private float _rotationY;
    private Quaternion originalRotation;
    void Start()
    {
        originalRotation = transform.localRotation;
    }
    private float ClampAngle(float angle,float min,float max)
    {
        return Mathf.Clamp(angle, min, max);
    }
    void Update()
    {
        switch (axes)
        {
            case RotationAxes.MouseX:
            {
                _rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                var xQuaternion = Quaternion.AngleAxis(_rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
                break;
            }
            case RotationAxes.MouseY:
            {
                _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                _rotationY = ClampAngle(_rotationY, minimumY, maximumY);
                var yQuaternion = Quaternion.AngleAxis(-_rotationY ,Vector3.right);
                transform.localRotation = originalRotation * yQuaternion;
                break;
            }
        }
    }
}