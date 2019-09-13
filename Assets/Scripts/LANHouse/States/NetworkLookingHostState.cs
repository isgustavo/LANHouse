using ThreeHornDino.Core;
using UnityEngine.Networking;

namespace ThreeHornDino.LanHouse
{
    public class NetworkLookingHostState : State
    {
        private NetworkManager networkManager;
        private int broadcastHostId;

        public NetworkLookingHostState(NetworkManager nm)
        {
            networkManager = nm;
        }

        public override void OnEnterState(State previousState = null)
        {
            HostTopology broadcastTopology = new HostTopology(networkManager.ConnConfig, 1);

            broadcastHostId = NetworkTransport.AddHost(broadcastTopology, NetworkManager.BROADCAST_PORT);

            NetworkTransport.SetBroadcastCredentials(broadcastHostId, NetworkManager.BROADCAST_KEY, NetworkManager.BROADCAST_VERSION,
                                                        NetworkManager.BROADCAST_SUB_VERSION, out _);

        }

        public override void OnUpdateState()
        {
            string ipAddress = "";
            byte error;
            NetworkEventType networkEvent;

            byte[] msgInBuffer = new byte[NetworkManager.MAX_MESSAGE_SIZE];

            networkEvent = NetworkTransport.ReceiveFromHost(broadcastHostId, out _, out _, msgInBuffer, NetworkManager.MAX_MESSAGE_SIZE, out _, out error);

            if (networkEvent == NetworkEventType.BroadcastEvent)
            {
                //NetworkTransport.GetBroadcastConnectionMessage(broadcastHostId, msgInBuffer, NetworkManager.MAX_MESSAGE_SIZE, out _, out error);

                NetworkTransport.GetBroadcastConnectionInfo(broadcastHostId, out ipAddress, out _, out error);

                networkManager.Address = ipAddress;

                networkManager.States.ChangeState<NetworkClientState>();
            }
        }     
    }
}
