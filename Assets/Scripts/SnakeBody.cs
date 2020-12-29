using UnityEngine;
using System.Collections.Generic;

public class SnakeBody : MonoBehaviour
{
    public List<Transform> Units;
    public List<Vector2> Positions { get; private set; } = new List<Vector2>();
    public List<Quaternion> Rotations { get; private set; } = new List<Quaternion>();

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
            Rotations.Add(unit.rotation);
        }

        Rescale();
    }

    /// <summary>
    /// Move each body units forward.
    /// </summary>
    /// <param name="previousHeadPosition">Position previously occupied by snake head</param>
    public void Move(Vector2 previousHeadPosition, Quaternion previousHeadRotation)
    {
        Vector2 previousBodyPosition = Positions[0];
        Quaternion previousBodyRotation = Rotations[0];

        Positions[0] = previousHeadPosition;
        Rotations[0] = previousHeadRotation;

        Units[0].position = Positions[0];
        Units[0].rotation = Rotations[0];

        for (int i = 1; i < Positions.Count; i++)
        {
            Vector2 tempPosition = Positions[i];
            Quaternion tempRotation = Rotations[i];

            Positions[i] = previousBodyPosition;
            Rotations[i] = previousBodyRotation;

            Units[i].position = Positions[i];
            Units[i].rotation = Rotations[i];

            previousBodyPosition = tempPosition;
            previousBodyRotation = tempRotation;
        }
    }

    public void Rescale()
    {
        float scaleGap = 0.5f / Units.Count;

        for (int i = 1; i < Units.Count; i++)
        {
            Units[i].transform.localScale = new Vector3(Units[i - 1].transform.localScale.x - scaleGap, Units[i - 1].transform.localScale.y - scaleGap, 1f);
        }
    }
}
