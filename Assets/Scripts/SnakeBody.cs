using UnityEngine;
using System.Collections.Generic;

public class SnakeBody : MonoBehaviour
{
    public List<Transform> Units;
    public List<Vector2> Positions { get; private set; } = new List<Vector2>();

    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void Start()
    {
        Positions = new List<Vector2>();

        foreach (Transform unit in Units)
        {
            Positions.Add(unit.position);
        }
    }

    /// <summary>
    /// Unity Event function.
    /// Move each body units forward.
    /// </summary>
    /// <param name="previousHeadPosition">Position previously occupied by snake head</param>
    public void Move(Vector2 previousHeadPosition)
    {
        Vector2 previousBodyPosition = Positions[0];
        Positions[0] = previousHeadPosition;
        Units[0].position = Positions[0];

        for (int i = 1; i < Positions.Count; i++)
        {
            Vector2 temp = Positions[i];

            Positions[i] = previousBodyPosition;
            Units[i].position = Positions[i];

            previousBodyPosition = temp;
        }
    }
}
