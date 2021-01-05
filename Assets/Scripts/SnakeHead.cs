using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private Snake snake;

    private Vector2 CurrentPosition { get; set; }
    public Vector2 PreviousPosition { get; set; }
    public Quaternion PreviousRotation { get; set; }

    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void Start()
    {
        CurrentPosition = transform.position;
        PreviousPosition = CurrentPosition;
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void FixedUpdate()
    {
        if (snake.type == SnakeType.Opponent) return;

        Move();
    }

    /// <summary>
    /// Move snake head in current snake direction.
    /// </summary>
    private void Move()
    {
        CurrentPosition += snake.CurrentDirection * (snake.speed * Time.deltaTime);
        // Snap snake head position to a grid.
        transform.position = new Vector2((int)CurrentPosition.x, (int)CurrentPosition.y);

        // Snake head look at moving direction.
        transform.up = Vector2.Lerp(transform.up, snake.CurrentDirection, 0.35f);
    }

    #region Trigger Handling

    /// <summary>
    /// Unity Event function.
    /// Handle trigger colliding with other colliders.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            snake.CollectFood(other.GetComponent<Food>());
        }
    }

    #endregion
}
