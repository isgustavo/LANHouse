using ThreeHornDino.Core;
using UnityEngine.Networking;

namespace ThreeHornDino.LanHouse
{
    public class NetworkInitState : State
    {
        private NetworkManager networkManager;

        public NetworkInitState(NetworkManager nm)
        {
            networkManager = nm;
        }

        public override void OnEnterState(State previousState = null)
        {
            base.OnEnterState(previousState);

            NetworkTransport.Init();

            networkManager.ConnConfig = new ConnectionConfig();
            networkManager.ReliableChannel = networkManager.ConnConfig.AddChannel(QosType.Reliable);
            networkManager.UnreliableChannel = networkManager.ConnConfig.AddChannel(QosType.Unreliable);

            networkManager.Topology = new HostTopology(networkManager.ConnConfig, NetworkManager.MAX_CONNECTIONS);
        }
    }
}