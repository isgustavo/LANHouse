using System;
using System.Collections.Generic;
using ThreeHornDino.Core;
using UnityEngine.Networking;

namespace ThreeHornDino.LanHouse
{
    public abstract class NetworkManager
    {
        public const int MAX_MESSAGE_SIZE = 1024;
        public const int MAX_CONNECTIONS = 4;

        public const string LOCALHOST_ADDRESS = "127.0.0.1";
        public const int PORT = 4200;
        public const int BROADCAST_PORT = 4201;

        public const int BROADCAST_KEY = 4202;
        public const int BROADCAST_VERSION = 1;
        public const int BROADCAST_SUB_VERSION = 1;
        public const int BROADCAST_INTERVAL = 1000;

        internal string Address { get; set; }
        internal ConnectionConfig ConnConfig { get; set; }
        internal byte ReliableChannel { get; set; }
        internal byte UnreliableChannel { get; set; }
        internal HostTopology Topology { get; set; }

        public NetworkClient LocalClient { get; internal set; }
        public Dictionary<int, NetworkClient> Clients { get; internal set; } = new Dictionary<int, NetworkClient>();

        public StateMachine States { get; internal set; }

        public NetworkManager()
        {
            States = new StateMachine(new Dictionary<Type, State>
            {
                { typeof(NetworkInitState), new NetworkInitState(this) },
                { typeof(NetworkLookingHostState), new NetworkLookingHostState(this) },
                { typeof(NetworkClientState), new NetworkClientState(this) },
                { typeof(NetworkHostState), new NetworkHostState(this) }
            });
        }

        public abstract void MakeRoom(Action<byte> OnCompleted);

        public abstract void JoinRoom(Action<byte> OnCompleted);

        public void Update()
        {
            States.OnUpdate();
        }
    }
}