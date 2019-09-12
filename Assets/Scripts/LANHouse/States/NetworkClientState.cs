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

        public override void OnUpdateState()
        {
            base.OnUpdateState();
        }
    }
}
