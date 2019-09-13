using UnityEngine;
using UnityEngine.UI;

namespace ThreeHornDino.Sample
{
    public class NetworkSceneController : MonoBehaviour
    {
        private void OnEnable()
        {
            GameObject.Find("MakeRoomButton").GetComponentInParent<Button>().onClick.AddListener(OnMakeRoom);
            GameObject.Find("JoinRoomButton").GetComponentInParent<Button>().onClick.AddListener(OnJoinRoom);
        }

        private void OnMakeRoom()
        {
            //GameSession.Current.NetworkManager.MakeRoom();
        }

        private void OnJoinRoom()
        {
            //GameSession.Current.NetworkManager.JoinRoom();
        }
    }

}

