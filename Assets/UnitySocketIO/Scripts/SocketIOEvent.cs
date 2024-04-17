using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnitySocketIO
{
    [Serializable]
    public class SocketIOEvent
    {

        public string eventName;

        public UnityEvent<SocketIOResponse> UnityEventAction;
        public Action<SocketIOResponse> EventAction;

        SocketIOEvent()
        {
            EventAction += OnEventActionInvoked;
        }
        ~SocketIOEvent()
        {
            EventAction -= OnEventActionInvoked;
        }
        void OnEventActionInvoked(SocketIOResponse response)
        {
            Debug.Log("Recived A respons ");
            Debug.Log(response.ToString());
            UnityEventAction?.Invoke(response);
        }
    }
}