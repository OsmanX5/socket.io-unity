using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitySocketIO
{
    public enum ConnectionState
    {
        NotInitlized,
        Connected,
        Disconnected,
        Failed,
    }
}