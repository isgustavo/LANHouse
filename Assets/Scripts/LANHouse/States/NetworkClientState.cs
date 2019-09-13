using ThreeHornDino.Core;
using UnityEngine.Networking;

namespace ThreeHornDino.LanHouse
{
    public class NetworkClientState : State
    {
        private NetworkManager networkManager;

        public NetworkClientState(NetworkManager nm)
        {
            networkManager = nm;
        }

        public override void OnEnterState(State previousState = null)
        {
            networkManager.Topology = new HostTopology(networkManager.ConnConfig, NetworkManager.MAX_CONNECTIONS);

            int clientID = NetworkTransport.AddHost(networkManager.Topology, 0);

            networkManager.LocalClient = new NetworkClient(Player.Current.PlayerName, clientID);
            NetworkTransport.Connect(clientID, networkManager.Address, NetworkManager.PORT, 0, out _);
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
                    break;
                case NetworkEventType.DataEvent:
                    break;
                case NetworkEventType.DisconnectEvent:
                    break;
            }
        }
    }
}
