using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Use the singleton pattern to make the class globally accessible

    #region Singleton

    private static CameraShaker instance;

    public static CameraShaker Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<CameraShaker>();

            return instance;
        }
    }

    #endregion

    // How long to shake the camera
    private float shakeDuration;

    // How hard to shake the camera
    private float shakeIntensity;

    // How steep should the shake decrease
    private float decreaseFactor;

    private Vector3 originalPosition;
    
    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void Start()
    {
        originalPosition = transform.position;
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void Update()
    {
        Randomize();
    }

    /// <summary>
    /// Randomize camera position by shake intensity if is shaking.
    /// </summary>
    private void Randomize()
    {
        // While shake duration is greater than 0
        if (shakeDuration > 0)
        {
            // Randomize position
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
            // Decrease shake duration
            shakeDuration -= Time.fixedDeltaTime * decreaseFactor;
        }
        // When shake duration reaches 0
        else
        {
            // Reset everything and stop shaking
            shakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    /// <summary>
    /// Shake the camera.
    /// </summary>
    public void Shake()
    {
        shakeDuration = 0.15f;
        shakeIntensity = 0.3f;
        decreaseFactor = 2f;
    }
}
