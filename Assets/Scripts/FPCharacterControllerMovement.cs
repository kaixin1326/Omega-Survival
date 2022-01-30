using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharacterControllerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Transform characterTransform;
    private Vector3 movementDirection;
    private bool isCrouched;

    private float originHeight;
    public float RunningSpeed;
    public float WalkSpeed;
    public float CrouchSpeed;

    public float JumpHeight;
    public float Gravity = 9.8f;
    public float CrouchHeight;

    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
        originHeight = characterController.height;
        isCrouched = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float currentSpeed = WalkSpeed;
        if (characterController.isGrounded)
        {
            //实现移动
            var Horizontal = Input.GetAxis("Horizontal");
            var Vertical = Input.GetAxis("Vertical");

            //移动方向
            movementDirection = characterTransform.TransformDirection(new Vector3(Horizontal, 0, Vertical));

            //实现跳跃
            if (Input.GetButtonDown("Jump"))
            {
                movementDirection.y = JumpHeight;
            }
            //实现左shift加速
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (isCrouched)
                {
                    currentSpeed = CrouchSpeed;
                }
                else
                {
                    currentSpeed = RunningSpeed;
                }

            }
            else
            {
                if (isCrouched)
                {
                    currentSpeed = CrouchSpeed;
                }
                else
                {
                    currentSpeed = WalkSpeed;
                }
            }

            //实现ctrl下蹲
            if (Input.GetKeyDown(KeyCode.C))
            {
                //实现从站立循序渐进到蹲下
                var currentHeight = isCrouched ? originHeight : CrouchHeight;
                StartCoroutine(DoCrouch(currentHeight));
                isCrouched = !isCrouched;
            }
        }
        //实现重力
        movementDirection.y -= Gravity * Time.deltaTime;
        characterController.Move(currentSpeed * Time.deltaTime * movementDirection);
    }

    private IEnumerator DoCrouch(float _target)
    {
        float currentHeight = 0;
        while (Mathf.Abs(characterController.height - _target) > 0.1f)
        {
            yield return null;
            characterController.height = Mathf.SmoothDamp(characterController.height, _target, ref currentHeight, Time.deltaTime * 5);
        }
    }
}
