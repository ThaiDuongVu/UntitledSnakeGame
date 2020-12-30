using UnityEngine;
using TMPro;
using Photon.Pun;

public class SnakeName : MonoBehaviour
{
    [SerializeField] private Snake snake;
    [SerializeField] private TMP_Text text;

    /// <summary>
    /// Unity Event function.
    /// Get component references.
    /// </summary>
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void OnEnable()
    {
        
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, (Vector2)snake.head.transform.position + Vector2.up * 2f, 0.2f);
    }

    /// <summary>
    /// Set a snake's display name.
    /// </summary>
    /// <param name="nameToSet">Name to set</param>
    public void Set(string nameToSet)
    {
        text.text = nameToSet;
    }
}
