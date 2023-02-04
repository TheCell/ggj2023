using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 movementInput;
    private LayerMask crossroadLayer;

    private void Start()
    {
        crossroadLayer = LayerMask.GetMask("Crossroad");
    }

    private void Update()
    {
        Vector3 inputFittedToCamera = Quaternion.AngleAxis(-30, Vector3.right) * new Vector3(movementInput.x, 0, movementInput.y);

        transform.Translate(inputFittedToCamera * speed * Time.deltaTime);

    }
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnClick(InputAction.CallbackContext ctx)
    {
        
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)* 1000, Color.white);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, crossroadLayer))
        {
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    public void onSecondaryHold(InputAction.CallbackContext ctx)
    {
        Debug.Log("Am Holding");
    }

}
