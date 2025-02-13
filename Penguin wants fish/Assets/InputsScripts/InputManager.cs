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

    //Configuration
    [SerializeField] private float sqrSwipeDeadzone =50.0f;
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
    private void LateUpdate()
    {
        ResetInputs();
    }
    private void ResetInputs()
    {
       tap= swipeDown=swipeUp=swipeLeft=swipeRight=false;
    }
    private void SetupControl()
    {
        actionScheme = new RunnerControl();

        actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {

        Vector2 delta =touchPosition - startDrag;
        float sqrDisatance = delta.sqrMagnitude;
        // confirmed swipe
        if (sqrDisatance > sqrSwipeDeadzone)
        {
            float x=Mathf.Abs(delta.x);
            float y= Mathf.Abs(delta.y);
            if (x > y)// left or right
            {
                if(delta.x > 0)
                    swipeRight = true;
                else 
                    swipeLeft = true;
            }
            else //up or down
            {
                if (delta.y > 0)
                    swipeUp = true;
                else 
                    swipeDown = true;
            }
        }
        startDrag=Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = touchPosition;
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        touchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        tap =true;
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
