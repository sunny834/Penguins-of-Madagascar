using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }


    //Action schemes
    private RunnerControl actionScheme;

    #region Pubilc properties
    public bool Tap { get { return tap; } }
    public Vector2 TouchPosition { get { return touchPosition; } }
    public bool SwipeRight {  get { return swipeRight; } }
    public bool SwipeLeft {  get { return swipeLeft; } }
    public bool SwipeUp {  get { return swipeUp; } }
    public bool SwipeDown {  get { return swipeDown; } }

    #endregion

    #region Private properties
    private bool tap;
    private Vector2 touchPosition;
    private Vector2 startDrag;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;
    #endregion
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }

    private void SetupControl()
    {
        actionScheme = new RunnerControl();

        actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.Tap.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }

    public void OnEnable()
    {
        actionScheme.Enable();
    }
    public void OnDisable()
    {
        actionScheme.Disable();
    }
}
