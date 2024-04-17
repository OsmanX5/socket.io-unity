
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SocketIOTest : MonoBehaviour
{
    [SerializeField] string URL = "https://www.example.com";
    SocketIOUnity socket;


    private void Init()
    {
        var uri = new Uri(URL);
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
        {
            {"token", "UNITY" }
        }
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        Debug.Log("Done Initlize ");
        socket.OnConnected += OnConnected;
        socket.OnError += OnErrorHappen;
    }

    private void OnErrorHappen(object sender, string e)
    {
        Debug.Log($"Error {e} ... ");
    }


    void Connect()
    {
        Debug.Log("Conecting ... ");
        socket.Connect();
    }
    private void OnConnected(object sender, EventArgs e)
    {
        Debug.Log("Connected");
    }

}
