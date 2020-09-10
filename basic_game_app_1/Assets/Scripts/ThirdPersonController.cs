using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private bool CanJump = true;
    private bool IsWalking = false;
    private bool IsCrouching = false;
    private Rigidbody RB;
    private float Speed = 5;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsWalking = Input.GetKey(KeyCode.LeftControl) || IsCrouching ? true : false;
        PlayerMovement();
        Jump();
        Crouch();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(hor, 0f, ver) * (Speed / (IsWalking ? 2 : 1)) * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }


    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && CanJump)
        {
            CanJump = false;
            RB.AddForce(0, 10, 0, ForceMode.Impulse);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (!IsCrouching)
            {
                IsCrouching = true;
                Vector3 scale = transform.localScale;
                scale.y = .5f;
                transform.localScale = scale;
            }
            else
            {
                IsCrouching = false;
                Vector3 scale = transform.localScale;
                scale.y = 1f;
                transform.localScale = scale;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            CanJump = true;
        }
    }
}