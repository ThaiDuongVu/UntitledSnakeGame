using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text pendingText;
    private float countdown = 6f;
    public byte countdownID;
    private bool doneCountdown;

    [SerializeField] private Snake snake1;
    [SerializeField] private Snake snake2;
    private bool initSnakes;

    private int joinOrder;

    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void Start()
    {
        snake1.gameObject.SetActive(false);
        snake2.gameObject.SetActive(false);
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void Update()
    {
        // If there's one person in room
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            // If one person left room then go back to lobby
            if (doneCountdown)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LoadLevel(0);
                SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
            }
            else
            {
                snake1.type = SnakeType.Player;
                snake2.type = SnakeType.Opponent;

                if (joinOrder == 0) joinOrder = 1;
                initSnakes = true;
            }
        }
        // If there are 2 people in room
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartCountdown();

            if (!initSnakes)
            {
                snake1.type = SnakeType.Opponent;
                snake2.type = SnakeType.Player;

                if (joinOrder == 0) joinOrder = 2;
                initSnakes = true;
            }

            // Set player name
            if (joinOrder == 2)
            {
                snake2.name.Set(PhotonNetwork.NickName);
                snake1.name.Set(PhotonNetwork.PlayerList[0].NickName);
            }
            else
            {
                snake1.name.Set(PhotonNetwork.NickName);
                snake2.name.Set(PhotonNetwork.PlayerList[1].NickName);
            }
        }
    }

    /// <summary>
    /// Countdown before game starts
    /// </summary>
    private void StartCountdown()
    {
        if (doneCountdown) return;

        countdown -= Time.deltaTime;
        pendingText.text = "Players found. Starting game in " + (int)countdown;

        // If countdown is done
        if (countdown <= 0f)
        {
            snake1.gameObject.SetActive(true);
            snake2.gameObject.SetActive(true);

            pendingText.gameObject.SetActive(false);

            doneCountdown = true;
        }
    }
}
