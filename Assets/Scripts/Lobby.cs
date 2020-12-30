using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class Lobby : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
{
    [SerializeField] private string gameVersion;

    [SerializeField] private TMP_InputField usernameInput;
    private string username;
    private bool isUsernameSet;

    [SerializeField] private TMP_InputField createRoomInput;
    [SerializeField] private TMP_InputField joinRoomInput;

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Animator messageAnimator;

    /// <summary>
    /// Unity Event function.
    /// Early initialization.
    /// </summary>
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /// <summary>
    /// Unity Event function.
    /// Initialize before first frame update.
    /// </summary>
    private void Start()
    {
        Connect();
    }

    /// <summary>
    /// Unity Event function.
    /// Update once per frame.
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// Connect to server using predefined settings.
    /// </summary>
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnConnected()
    {

    }

    #region Photon Connection Callbacks

    public void OnConnectedToMaster()
    {
        Debug.Log("Connected");
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {

    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {

    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {

    }

    #endregion

    #region Photon Matchmaking Callbacks

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {

    }

    public void OnCreatedRoom()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        ShowMessage(message);
    }

    public void OnJoinedRoom()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        ShowMessage(message);
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        ShowMessage(message);
    }

    public void OnLeftRoom()
    {

    }

    #endregion

    /// <summary>
    /// Set player username.
    /// </summary>
    public void SetUsername()
    {
        // If username not entered then do nothing
        if (string.IsNullOrEmpty(usernameInput.text))
        {
            ShowMessage("Username not entered");
            return;
        }

        username = usernameInput.text;
        isUsernameSet = true;

        PhotonNetwork.NickName = username;

        ShowMessage("Username set to " + username);
    }

    /// <summary>
    /// Create a new room with a room code.
    /// </summary>
    public void CreateRoom()
    {
        // If username not set then do nothing
        if (!isUsernameSet)
        {
            ShowMessage("Username not set");
            return;
        }
        // If room name not entered then do nothing
        if (string.IsNullOrEmpty(createRoomInput.text))
        {
            ShowMessage("Room name not entered");
            return;
        }

        // Create new open room with 2 players
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(createRoomInput.text, roomOptions, TypedLobby.Default);
    }

    /// <summary>
    /// Join an existing room with a room code.
    /// </summary>
    public void JoinRoom()
    {
        // If username not set then do nothing
        if (!isUsernameSet)
        {
            ShowMessage("Username not set");
            return;
        }
        // If room name not entered then do nothing
        if (string.IsNullOrEmpty(joinRoomInput.text))
        {
            ShowMessage("Room name not entered");
            return;
        }

        // Join an open room
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    /// <summary>
    /// Join a random open room without any code.
    /// </summary>
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// Display a feedback message to player.
    /// </summary>
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageAnimator.SetTrigger("intro");
    }
}
