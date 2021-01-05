using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Snake : MonoBehaviour, IOnEventCallback
{
    public SnakeType type;
    public byte changeDirectionID;
    public byte growID;
    public byte positionID;

    public float speed = 5f;
    public Vector2 CurrentDirection { get; private set; } = Vector2.up;

    public SnakeHead head;
    public SnakeBody body;
    public new SnakeName name;

    [SerializeField] private ParticleSystem firework;

    private InputManager inputManager;

    /// <summary>
    /// Unity Event function.
    /// Initialize input manager and add event listener on object enabled.
    /// </summary>
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);

        // If not player snake then don't read input
        if (type == SnakeType.Opponent) return;

        inputManager = new InputManager();
        inputManager.Enable();

        // Handle direction input
        inputManager.Snake.Direction.started += OnDirectionChanged;
    }

    #region Input Handling

    /// <summary>
    /// When a directional button is pressed.
    /// </summary>
    /// <param name="context">Input context</param>
    private void OnDirectionChanged(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = context.ReadValue<Vector2>();
        ChangeDirection(inputDirection);
    }

    #endregion

    /// <summary>
    /// Unity Event function.
    /// Stop reading input and remove event listener on object disabled.
    /// </summary>
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);

        // If not player snake then don't read input
        if (type == SnakeType.Opponent) return;

        inputManager.Disable();
    }

    /// <summary>
    /// Photon event callback.
    /// On event received.
    /// </summary>
    public void OnEvent(EventData eventData)
    {
        if (eventData.Code == growID && (int)eventData.CustomData > body.Units.Count)
        {
            Grow();
        }
        else if (eventData.Code == positionID)
        {
            head.transform.position = new Vector2(((float[])eventData.CustomData)[0], ((float[])eventData.CustomData)[1]);
            body.Move(new Vector2(((float[])eventData.CustomData)[2], ((float[])eventData.CustomData)[3]), new Quaternion(0f, 0f, ((float[])eventData.CustomData)[4], 0f));
        }
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void Update()
    {
        if (type == SnakeType.Opponent) return;

        // If snake head move by 1 unit then snake body move by 1 unit.
        if ((Vector2)head.transform.position != head.PreviousPosition)
        {
            body.Move(head.PreviousPosition, head.PreviousRotation);

            // Raise new position event
            float[] positionData = new float[] { head.transform.position.x, head.transform.position.y, head.PreviousPosition.x, head.PreviousPosition.y, head.transform.rotation.z };
            PhotonNetwork.RaiseEvent(positionID, positionData, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);

            head.PreviousPosition = head.transform.position;
            head.PreviousRotation = head.transform.rotation;
        }
    }

    /// <summary>
    /// Change snake direction.
    /// </summary>
    /// <param name="newDirection">New direction</param>
    public void ChangeDirection(Vector2 newDirection)
    {
        if (newDirection == Vector2.left || newDirection == Vector2.right)
        {
            if (CurrentDirection == Vector2.up || CurrentDirection == Vector2.down)
            {
                CurrentDirection = newDirection;
            }
        }
        else
        {
            if (CurrentDirection == Vector2.left || CurrentDirection == Vector2.right)
            {
                CurrentDirection = newDirection;
            }
        }
    }

    /// <summary>
    /// Grow snake's length by 1 unit.
    /// </summary>
    public void Grow()
    {
        // Spawn a new body unit and and add that unit to snake body list.
        body.Units.Insert(0, Instantiate(body.Units[0], head.transform.position, head.transform.rotation));
        body.Positions.Insert(0, body.Units[0].position);
        body.Rotations.Insert(0, body.Units[0].rotation);

        body.Units[0].parent = body.transform;

        body.Rescale();

        // Raise an event to notify the server that snake grows
        PhotonNetwork.RaiseEvent(growID, body.Units.Count, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
    }

    /// <summary>
    /// Snake eats a food object.
    /// </summary>
    /// <param name="food">Food object to eat</param>
    public void CollectFood(Food food)
    {
        Grow();

        Instantiate(firework, head.transform.position, head.transform.rotation);
        food.RandomizePosition();

        CameraShaker.Instance.Shake();
    }
}