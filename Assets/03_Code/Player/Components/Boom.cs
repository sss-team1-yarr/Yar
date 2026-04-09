using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Player.Components {
    public class Boom : MonoBehaviour {
        [SerializeField] private GameObject player;


        private void Update() {
            if (player != null) {
                transform.position = player.transform.position;
            }
        }
    }
}