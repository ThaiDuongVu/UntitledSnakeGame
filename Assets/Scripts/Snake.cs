using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public const float Speed = 5f;
    public Vector2 CurrentDirection { get; private set; } = Vector2.up;

    [SerializeField] private SnakeHead head;
    [SerializeField] private SnakeBody body;

    private InputManager inputManager;

    /// <summary>
    /// Unity Event function.
    /// Initialize input manager on object enabled.
    /// </summary>
    private void OnEnable()
    {
        inputManager = new InputManager();
        inputManager.Enable();

        // Handle direction input
        inputManager.Snake.Direction.started += OnDirectionChanged;
    }

    #region Input Handling

    /// <summary>
    /// Change snake direction when a directional button is pressed.
    /// </summary>
    /// <param name="context">Input context</param>
    private void OnDirectionChanged(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = context.ReadValue<Vector2>();

        if (inputDirection == Vector2.left || inputDirection == Vector2.right)
        {
            if (CurrentDirection == Vector2.up || CurrentDirection == Vector2.down)
            {
                CurrentDirection = inputDirection;
            }
        }
        else
        {
            if (CurrentDirection == Vector2.left || CurrentDirection == Vector2.right)
            {
                CurrentDirection = inputDirection;
            }
        }
    }

    #endregion

    /// <summary>
    /// Unity Event function.
    /// Stop reading input on object disabled.
    /// </summary>
    private void OnDisable()
    {
        inputManager.Disable();
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void Update()
    {
        // If snake head move by 1 unit then snake body move by 1 unit.
        if ((Vector2)head.transform.position != head.PreviousPosition)
        {
            body.Move(head.PreviousPosition);
            head.PreviousPosition = head.transform.position;
        }
    }

    /// <summary>
    /// Grow snake's length by 1 unit.
    /// </summary>
    public void Grow()
    {
        Transform headTransform = head.transform;

        // Spawn a new body unit and and that unit to snake body list.
        body.Units.Insert(0, Instantiate(body.Units[0], headTransform.position, headTransform.rotation));
        body.Units[0].parent = body.transform;
        body.Positions.Insert(0, body.Units[0].position);
    }
}