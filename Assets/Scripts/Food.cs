using UnityEngine;

public class Food : MonoBehaviour
{
    private Vector2Int maxPosition = new Vector2Int(25, 13);
    private Vector2Int minPosition = new Vector2Int(-25, -13);

    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void Start()
    {
        RandomizePosition();
    }

    /// <summary>
    /// Move food to a random position.
    /// </summary>
    public void RandomizePosition()
    {
        transform.position = new Vector2((int)Random.Range(minPosition.x, maxPosition.x), (int)Random.Range(minPosition.y, maxPosition.y));
    }
}
