using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMouseLook : MonoBehaviour
{
    private Transform CameraTransform;
    [SerializeField] private Transform characterTransform;//引入 使角色随着相机旋转
    private Vector3 cameraRotation;//保存每一帧X轴值
    public float MouseSensitivity;
    public Vector2 MaxminAngle;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var tmp_MouseX = Input.GetAxis("Mouse X");
        var tmp_MouseY = Input.GetAxis("Mouse Y");

        cameraRotation.x += tmp_MouseY * MouseSensitivity;
        cameraRotation.y += tmp_MouseX * MouseSensitivity;

        cameraRotation.x = Mathf.Clamp(value: cameraRotation.x, min: MaxminAngle.x, max: MaxminAngle.y);
        CameraTransform.rotation = Quaternion.Euler(x: cameraRotation.x, y: cameraRotation.y, z: 0);
        characterTransform.rotation = Quaternion.Euler(x: 0, y: cameraRotation.y, z: 0);
    }
}
