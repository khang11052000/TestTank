using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviourPunCallbacks
{
    //[SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateInput;
    [SerializeField] private InputField JoinInput;

    [SerializeField] private GameObject StartButton;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        UsernameMenu.SetActive(true);
    }

    public void OnConnectedToMater()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected!");
    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length > 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.NickName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateInput.text);
    }

    public void JoinGame()
    {
        // RoomOptions roomOptions = new RoomOptions();
        // roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinRoom(JoinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}