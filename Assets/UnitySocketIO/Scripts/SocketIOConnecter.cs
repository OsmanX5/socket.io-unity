using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;


namespace UnitySocketIO
{
    [Serializable]
    public class  SocketIOQuery
    {

        public string Key;
        public string Value;
    }
    public class SocketIOConnecter : MonoBehaviour
    {
        #region Core Properaties
        [SerializeField] string URL = "https://www.example.com";

        [ SerializeField]
        ConnectionState state = ConnectionState.NotInitlized;

        [SerializeField]
        SocketIOUnity _socket;
        bool IsInit { get => _socket != null; }
        bool IsConnected { get => state == ConnectionState.Connected; }
        #endregion

        #region Core Connection Functions
        [SerializeField]
        UnityStringDictionary _query = new UnityStringDictionary();

        [SerializeField]
        UnityStringDictionary _ExtraHeaders = new UnityStringDictionary();

        public void UpdateConnectionURL(string connectionURL, Dictionary<string, string> query = null, Dictionary<string, string> extraHeaders = null)
        {
            URL = connectionURL;
            _query = new UnityStringDictionary(query);
            _ExtraHeaders = new UnityStringDictionary(extraHeaders);
        }
        public void Init()
        {
            var uri = new Uri(URL);
            _socket = new SocketIOUnity(uri, new SocketIOOptions
            {
                Query = _query.GetDictionary(),
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
                ExtraHeaders = _ExtraHeaders.GetDictionary()
            });
            AssignBasicEvents();
            AssignEvents();
            Debug.Log("Done Initlize ");
        }

        public void Connect()
        {
            Debug.Log("Conecting ... ");
            Init();
            _socket.Connect();
        }
        public void Disconnect()
        {
            Debug.Log("Dis Connecting ..");
            if (state == ConnectionState.Connected)
                _socket.Disconnect();
        }
        void AssignBasicEvents()
        {
            _socket.OnConnected += OnSocketConnected;
            _socket.OnDisconnected += OnSocketDisconnected;
            _socket.OnError += OnSocketError;
        }
        void UnAssignBasicEvents()
        {
            _socket.OnConnected -= OnSocketConnected;
            _socket.OnDisconnected -= OnSocketDisconnected;
            _socket.OnError -= OnSocketError;
        }
        private void OnSocketConnected(object sender, EventArgs e)
        {
            Debug.Log("Socket connected");
            state = ConnectionState.Connected;
            OnConnected?.Invoke();
        }
        private void OnSocketDisconnected(object sender, string e)
        {
            Debug.Log("Socket Disconnected :" + e);
            state = ConnectionState.Disconnected;
            OnDisconnected?.Invoke();
        }

        private void OnSocketError(object sender, string e)
        {
            Debug.Log("Socket Has error " + e);
            state = ConnectionState.Disconnected;
            OnError?.Invoke();
        }

        private void OnDisable()
        {
            UnAssignBasicEvents();
        }


        private void ResetState()
        {
            state = ConnectionState.NotInitlized;
            if (_socket != null)
                _socket.Disconnect();
            _socket = null;
        }
        #endregion

        #region Core Unity Events
        [SerializeField]
        UnityEvent OnConnected;
        [SerializeField]
        UnityEvent OnDisconnected;
        [SerializeField]
        UnityEvent OnError;
        #endregion


        #region Events
        List<SocketIOEvent> events;

        void AssignEvents()
        {
            if (_socket == null)
            {
                Debug.Log("Socket Is not init yet");
                return;
            }
            foreach (SocketIOEvent e in events)
            {
                _socket.On(e.eventName, e.EventAction);
            }
        }


        #endregion

        #region Emit
        void Emit(SocketIOEmitEvent emitEvent)
        {
            if(state != ConnectionState.Connected)
                return;
            _socket.Emit(emitEvent.eventName, emitEvent.data);

        }

        #endregion

    }

}
