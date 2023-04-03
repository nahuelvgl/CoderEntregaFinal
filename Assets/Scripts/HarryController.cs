using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class HarryController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 harryScale;
    [SerializeField] private Animator harryAnimator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] private Camera m_camera;
    [SerializeField] private float rotationSpeed;
    bool sizeController;

    private void Start()
    {
      harryScale = transform.localScale;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
        Cursor.lockState = hasFocus? CursorLockMode.None:CursorLockMode.Confined;
    }

    void Update()
    {
        Move(GetMovementInput());
        Rotate(GetRotationInput());
    }

    private void Rotate(Vector2 p_scrollDelta)
    {
        transform.Rotate(Vector3.up, p_scrollDelta.x * rotationSpeed * Time.deltaTime, Space.Self);
    }

    private Vector2 GetRotationInput()
    {
        var l_mouseX = Input.GetAxis("Mouse X");
        var l_mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(l_mouseX, l_mouseY);
    }

    private Vector3 GetMovementInput()
    {
        var l_horizontal = Input.GetAxis("Horizontal");
        var l_vertical = Input.GetAxis("Vertical");
        return new Vector3(l_horizontal, 0, l_vertical).normalized;
    }

    private void Move(Vector3 p_inputMovement)
    {
        var transform1 = transform;
        transform1.position += (p_inputMovement.z * transform1.forward + p_inputMovement.x * transform1.right) *
                               (speed * Time.deltaTime);
        harryAnimator.SetFloat(Speed, p_inputMovement.magnitude);
    }
    private void RestoreSize()
    {
        transform.localScale *= 0.2f;
    }
    private void Shrink()
    {
        transform.localScale /= 0.2f;
    }
    public void ToggleSize()
    {
        if (sizeController)
        {
            Shrink();
            sizeController = false;
        }
        else
        {
            RestoreSize();
            sizeController = true;

        }
    }
}