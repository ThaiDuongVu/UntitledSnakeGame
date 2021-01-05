using UnityEngine;
using Photon.Pun;

public class Food : MonoBehaviour
{
    private Vector2Int maxPosition = new Vector2Int(25, 13);
    private Vector2Int minPosition = new Vector2Int(-25, -13);

    private const byte PositionChangedCode = 0;

    /// <summary>
    /// Unity Event function.
    /// Add event listener on object enabled.
    /// </summary>
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnChangedPosition;
    }

    /// <summary>
    /// Unity Event function.
    /// Remove event listener on object disabled.
    /// </summary>
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnChangedPosition;
    }

    /// <summary>
    /// On position changed event raised.
    /// </summary>
    private void OnChangedPosition(ExitGames.Client.Photon.EventData eventData)
    {
        if (eventData.Code == PositionChangedCode)
        {
            transform.position = new Vector2(((float[])eventData.CustomData)[0], ((float[])eventData.CustomData)[1]);
        }
    }

    /// <summary>
    /// Move food to a random position.
    /// </summary>
    public void RandomizePosition()
    {
        transform.position = new Vector2((int)Random.Range(minPosition.x, maxPosition.x), (int)Random.Range(minPosition.y, maxPosition.y));

        // Raise an event to notify the server that position changed
        float[] datas = new float[] { transform.position.x, transform.position.y };
        PhotonNetwork.RaiseEvent(PositionChangedCode, datas, Photon.Realtime.RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
    }
}
