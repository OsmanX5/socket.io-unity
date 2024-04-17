using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;

[RequireComponent(typeof(SocketIOConnecter))]   
public class AutoSocketIOConnectOnStart : MonoBehaviour
{
    SocketIOConnecter socketIOConnecter { get => GetComponent<SocketIOConnecter>(); }


    private void Start()
    {
        socketIOConnecter.Connect();
    }
}
