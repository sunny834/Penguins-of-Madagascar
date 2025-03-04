using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    // Action schemes
    private RunnerControl actionScheme;

    // Configuration
    [SerializeField] private float sqrSwipeDeadzone = 50.0f;

    #region Public properties
    public bool Tap { get; private set; }
    public Vector2 TouchPosition { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }
    #endregion

    #region Private properties
    private Vector2 startDrag;
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Ensure singleton pattern
            return;
        }

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
        Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;
    }

    private void SetupControl()
    {
        actionScheme = new RunnerControl();

        actionScheme.Gameplay.Tap.performed += OnTap;
        actionScheme.Gameplay.TouchPosition.performed += OnPosition;
        actionScheme.Gameplay.StartDrag.performed += OnStartDrag;
        actionScheme.Gameplay.EndDrag.performed += OnEndDrag;
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        Vector2 delta = TouchPosition - startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > sqrSwipeDeadzone)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                SwipeRight = delta.x > 0;
                SwipeLeft = delta.x < 0;
            }
            else
            {
                SwipeUp = delta.y > 0;
                SwipeDown = delta.y < 0;
            }
        }
        startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = TouchPosition;
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        TouchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        Tap = true;
    }

    private void OnEnable()
    {
        actionScheme?.Enable();
    }

    private void OnDisable()
    {
        actionScheme?.Disable();
    }
}
