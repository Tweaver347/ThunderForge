
using UnityEngine;

public class InputManager : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerControls playerControls;

    public Vector2 movementInput;
    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }
    private void Update()
    {
        HandleAllInputs();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Cam.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        playerControls.PlayerMovement.Enable();
    }
    private void OnDisable()
    {
        playerControls.PlayerMovement.Disable();
    }
    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimValues(0, moveAmount);
    }
}
