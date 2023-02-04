using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 10;
    private Vector2 movementInput;
    private LayerMask crossroadLayer;
    private LayerMask uiLayer;

    private GameObject activeUiElement;
    private Coroutine openingCoroutine = null;
    private static GameObject UI;


    private void Start()
    {
        crossroadLayer = LayerMask.GetMask("Crossroad");
        uiLayer = LayerMask.GetMask("UI");
        if (!UI)
        {
            UI = GameObject.FindWithTag("ActionUI");
            UI.SetActive(false);
        }
    }

    private void Update()
    {
        Vector3 inputFittedToCamera = new Vector3(movementInput.x, movementInput.y, 0);

        transform.Translate(inputFittedToCamera * speed * Time.deltaTime);
        HandleMenuOpen();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, crossroadLayer))
        {
            TapAction tree = hit.transform.GetComponent<TapAction>();
            tree.tapAction();
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    public void onSecondaryHold(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        if (value == 1 && openingCoroutine == null)
        {
            openingCoroutine = StartCoroutine(MenuState());
        }
        else if (value == 0 && openingCoroutine != null)
        {
            StopCoroutine(openingCoroutine);
            UI.SetActive(false);
            openingCoroutine = null;

            if (activeUiElement)
            {
                activeUiElement.GetComponent<ActionUi>().Select();
                activeUiElement = null;
            }
        }
    }

    private IEnumerator MenuState()
    {
        yield return new WaitForSeconds(1);

        UI.SetActive(true);
        UI.transform.position = (transform.position - new Vector3(-1f, 1f, -1f));
    }

    private void HandleMenuOpen()
    {
        if (openingCoroutine == null) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, uiLayer))
        {
            if (!activeUiElement)
            {
                activeUiElement = hit.collider.gameObject;
                activeUiElement.GetComponent<ActionUi>().Activate();
            }
            else if (activeUiElement.Equals(hit.collider.gameObject))
            {
                return;
            }
            else
            {
                activeUiElement.GetComponent<ActionUi>().Passivate();
                activeUiElement = hit.collider.gameObject;
                activeUiElement.GetComponent<ActionUi>().Activate();
            }
        }
        else if (activeUiElement)
        {
            activeUiElement.GetComponent<ActionUi>().Passivate();
            activeUiElement = null;
        }
    }
}
