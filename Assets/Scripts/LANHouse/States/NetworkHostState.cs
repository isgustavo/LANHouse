using ThreeHornDino.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace ThreeHornDino.LanHouse
{
    public class NetworkHostState : State
    {
        private NetworkManager networkManager;

        public NetworkHostState(NetworkManager nm)
        {
            networkManager = nm;
        }

        public override void OnEnterState(State previousState = null)
        {
            base.OnEnterState(previousState);

            int hostID = NetworkTransport.AddHost(networkManager.Topology, NetworkManager.PORT, null);

            byte error;
            if (NetworkTransport.StartBroadcastDiscovery(hostID, NetworkManager.BROADCAST_PORT,
                                                            NetworkManager.BROADCAST_KEY, NetworkManager.BROADCAST_VERSION,
                                                                    NetworkManager.BROADCAST_SUB_VERSION, new byte[NetworkManager.MAX_MESSAGE_SIZE],
                                                                    NetworkManager.MAX_MESSAGE_SIZE,NetworkManager.BROADCAST_INTERVAL, out error))
            {
#if UNITY_EDITOR
                Debug.Log("StartBroadcast Sucess!");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("StartBroadcast failed!");
#endif
            }

            int clientID = NetworkTransport.AddHost(networkManager.Topology, 0);

            NetworkTransport.Connect(clientID, NetworkManager.LOCALHOST_ADDRESS, NetworkManager.PORT, 0, out error);

            networkManager.LocalClient = new NetworkClient(Player.Current.PlayerName, clientID, hostID);
            networkManager.Clients.Add(clientID, networkManager.LocalClient);
        }

        public override void OnUpdateState()
        {
            int connectionId;
            int channel;
            byte[] recBuffer = new byte[NetworkManager.MAX_MESSAGE_SIZE];
            int dataSize;
            byte error;

            NetworkEventType type = NetworkTransport.Receive(out _, out connectionId, out channel, recBuffer, NetworkManager.MAX_MESSAGE_SIZE, out dataSize, out error);
            switch (type)
            {
                case NetworkEventType.ConnectEvent:
                    if (!networkManager.Clients.ContainsKey(connectionId))
                    {
                        //networkManager.Clients.Add(connectionId, new NetworkClient(connectionId));
                        //networkManager.SendConnectedClients();
                    }
                    break;
                case NetworkEventType.DataEvent:
                    break;
                case NetworkEventType.DisconnectEvent:
                    break;
            }
        }
    }
}