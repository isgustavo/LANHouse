using UnityEngine;

namespace ThreeHornDino.LanHouse
{
    public class NetworkClient
    {
        public int HostID { get; private set; } = -1;
        public int ClientID { get; private set; } = -1;

        public string ClientName { get; private set; } = "";

        public bool IsHost => HostID != -1 && ClientID != -1;

        public NetworkClient(string clientName, int clientID = -1, int hostID = -1)
        {
            HostID = hostID;
            ClientID = clientID;
            ClientName = clientName;
#if UNITY_EDITOR
            Debug.Log($"NetworkClient {ClientName} created clientID = {ClientID} hostID = {HostID}");
#endif
        }
    }
}