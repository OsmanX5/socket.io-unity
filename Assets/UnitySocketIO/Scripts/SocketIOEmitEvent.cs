using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnitySocketIO
{
    [Serializable]
    public class SocketIOEmitEvent
    {

        public string eventName;
        public List<SocketIOObject> data;
        public UnityEvent<SocketIOResponse> AcknowldgeEvent;
        public Action<SocketIOResponse> EventAction;

        SocketIOEmitEvent()
        {
            EventAction += OnEventActionInvoked;
        }
        ~SocketIOEmitEvent()
        {
            EventAction -= OnEventActionInvoked;
        }
        void OnEventActionInvoked(SocketIOResponse response)
        {
            Debug.Log("Recived A respons ");
            Debug.Log(response.ToString());
            AcknowldgeEvent?.Invoke(response);
        }
    }
}