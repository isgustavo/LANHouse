using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ThreeHornDino.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace ThreeHornDino.LanHouse
{
    public class NetworkManager
    {
        public const int MAX_MESSAGE_SIZE = 1024;
        public const int MAX_CONNECTIONS = 4;

        public const string DEFAULT_ADDRESS = "127.0.0.1";
        public const int PORT = 4200;
        public const int BROADCAST_PORT = 4201;

        public const int BROADCAST_KEY = 4202;
        public const int BROADCAST_VERSION = 1;
        public const int BROADCAST_SUB_VERSION = 1;
        public const int BROADCAST_INTERVAL = 1000;

        internal ConnectionConfig ConnConfig { get; set; }
        internal byte ReliableChannel { get; set; }
        internal byte UnreliableChannel { get; set; }
        internal HostTopology Topology { get; set; }

        public NetworkClient LocalClient { get; internal set; }
        public Dictionary<int, NetworkClient> Clients { get; internal set; } = new Dictionary<int, NetworkClient>();

        public StateMachine States { get; internal set; }

        private NetworkManager()
        {
            States = new StateMachine(new Dictionary <Type, State>
            {
                { typeof(NetworkInitState), new NetworkInitState(this) },
                { typeof(NetworkLookingHostState), new NetworkLookingHostState(this) },
                { typeof(NetworkClientState), new NetworkClientState(this) },
                { typeof(NetworkHostState), new NetworkHostState(this) }
            });
        }

        public static NetworkManager Init()
        {
            NetworkManager nm = new NetworkManager();
            nm.States.ChangeState<NetworkInitState>();
            return nm;
        }

        public void Update()
        {
            States.OnUpdate();
        }

        //public IEnumerator JoinRoomCoroutine(Action<byte> OnCompleted)
        //{
        //    NetworkTransport.Init();

        //    ConnectionConfig cc = new ConnectionConfig();
        //    ReliableChannel = cc.AddChannel(QosType.Reliable);
        //    UnreliableChannel = cc.AddChannel(QosType.Unreliable);

        //    HostTopology topo = new HostTopology(cc, 1);

        //    int broadcastHostId = NetworkTransport.AddHost(topo, BROADCAST_PORT);

        //    NetworkTransport.SetBroadcastCredentials(broadcastHostId, BROADCAST_KEY, BROADCAST_VERSION, BROADCAST_SUB_VERSION, out _);

        //    string ipAddress = "";
        //    byte error;
        //    NetworkEventType networkEvent;
        //    do
        //    {
        //        byte[] msgInBuffer = new byte[MAX_MESSAGE_SIZE];

        //        networkEvent = NetworkTransport.ReceiveFromHost(broadcastHostId, out _, out _, msgInBuffer, MAX_MESSAGE_SIZE, out _, out error);

        //        if (networkEvent == NetworkEventType.BroadcastEvent)
        //        {
        //            NetworkTransport.GetBroadcastConnectionMessage(broadcastHostId, msgInBuffer, MAX_MESSAGE_SIZE, out _, out error);

        //            NetworkTransport.GetBroadcastConnectionInfo(broadcastHostId, out ipAddress, out _, out error);
        //            break;
        //        }

        //        yield return new WaitForEndOfFrame();
        //    }
        //    while (networkEvent == NetworkEventType.Nothing);

        //    NetworkTransport.Shutdown();

        //    NetworkTransport.Init();

        //    topo = new HostTopology(cc, MAX_CONNECTIONS);

        //    int clientID = NetworkTransport.AddHost(topo, 0);

        //    NetworkTransport.Connect(clientID, ipAddress, PORT, 0, out _);

        //    OnCompleted.Invoke(error);
        //}

        //public void Updater()
        //{
        //    if (LocalClient != null)
        //    {
        //        if (LocalClient.IsHost)
        //        {
        //            HostUpdater();
        //        } else
        //        {
        //            //ClientUpdater();
        //        }

        //    }
        //}

        //public void HostUpdater()
        //{
        //    int connectionId;
        //    int channel;
        //    byte[] recBuffer = new byte[MAX_MESSAGE_SIZE];
        //    int dataSize;
        //    byte error;

        //    NetworkEventType type = NetworkTransport.Receive(out _, out connectionId, out channel, recBuffer, MAX_MESSAGE_SIZE, out dataSize, out error);
        //    switch (type)
        //    {
        //        case NetworkEventType.ConnectEvent:
        //            if (!Clients.ContainsKey(connectionId))
        //            {
        //                Clients.Add(connectionId, new NetworkClient(connectionId));
        //                SendConnectedClients();
        //            }
        //            break;
        //        case NetworkEventType.DataEvent:
        //            //if (IsServer)
        //            //{

        //            //}
        //            //else
        //            //{
        //            //    OnClientReceiveDataEvent(recBuffer);
        //            //}
        //            break;
        //        case NetworkEventType.DisconnectEvent:
        //            Debug.Log($"Client id: {connectionId} has disconnected {channel}");
        //            break;
        //    }
        //}

        private void SendConnectedClients()
        {
            //foreach (int clientId in Clients.Keys)
            //{
            //    byte error;
            //    byte[] buffer = new byte[1024];
            //    Stream stream = new MemoryStream(buffer);
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(stream, clientId);

            //    int bufferSize = 1024;

            //    NetworkTransport.Send(HostID, clientConnectedId, ReliableChannel, buffer, bufferSize, out error);
            //}
        }

        //public void OnClientReceiveDataEvent(byte[] recBuffer)
        //{
        //    Stream stream = new MemoryStream(recBuffer);
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    int i = (int)formatter.Deserialize(stream);

        //    Debug.Log($"player connected id {i}");
        //}
    }
}