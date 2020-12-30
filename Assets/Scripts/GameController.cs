using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private Snake snake1;
    [SerializeField] private Snake snake2;
    private bool initSnakes;

    private Snake playerSnake;
    private Snake opponentSnake;

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
        // If first person join room
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            snake1.type = SnakeType.Player;
            snake2.type = SnakeType.Opponent;

            playerSnake = snake1;
            opponentSnake = snake2;

            if (joinOrder == 0) joinOrder = 1;
            initSnakes = true;
        }
        // If second person join room
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (!initSnakes)
            {
                snake1.type = SnakeType.Opponent;
                snake2.type = SnakeType.Player;

                playerSnake = snake2;
                opponentSnake = snake1;

                if (joinOrder == 0) joinOrder = 2;
                initSnakes = true;
            }

            playerSnake.name.Set(PhotonNetwork.NickName);
            if (joinOrder == 2) opponentSnake.name.Set(PhotonNetwork.PlayerList[0].NickName);
            else opponentSnake.name.Set(PhotonNetwork.PlayerList[1].NickName);

            if (!snake1.gameObject.activeInHierarchy) snake1.gameObject.SetActive(true);
            if (!snake2.gameObject.activeInHierarchy) snake2.gameObject.SetActive(true);
        }
    }
}
