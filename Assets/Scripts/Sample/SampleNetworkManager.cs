using System;
using ThreeHornDino.LanHouse;

namespace ThreeHornDino.Sample
{
    public class SampleNetworkManager : NetworkManager
    {
        public SampleNetworkManager()
        {
            States.ChangeState<NetworkInitState>();
        }

        public override void JoinRoom(Action<byte> OnCompleted)
        {
            States.ChangeState<NetworkHostState>();
        }

        public override void MakeRoom(Action<byte> OnCompleted)
        {
            throw new NotImplementedException();
        }
    }
}


