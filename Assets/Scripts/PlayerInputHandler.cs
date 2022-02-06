using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用来处理输入
public class PlayerInputHandler : MonoBehaviour
{
    //方便跨文件调用
    public static PlayerInputHandler Instance;
    public float lookSensitivity = 1f;

    private void Awake()
    {
        Instance = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        return move;
    }

    public float GetMouseLookHorizontal()
    {
        return GetMouseLookAxis("Mouse X");
    }

    public float GetMouseLookVertical()
    {
        return GetMouseLookAxis("Mouse Y");
    }

    public float GetMouseLookAxis(string mouseInputName)
    {
        float i = Input.GetAxisRaw(mouseInputName);
        i *= lookSensitivity * 0.01f;

        return i;
    }

    public bool GetFireInputHeld()
    {
        return Input.GetButton("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}