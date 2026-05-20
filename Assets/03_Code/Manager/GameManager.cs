using _03_Code.Exp;
using _03_Code.Player.Components;
using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code.Manager {
    public class GameManager : MonoBehaviour {
        public Player.Main.Player player;
        public PlayerControl playerControl;
        public PlayerHit playerHit;
        public DropManager dropManager;
        public static GameManager Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        private void Reset() {
            player = GameObject.FindWithTag("Player").GetComponent<Player.Main.Player>();
            playerControl = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerControl>();
            playerHit = GameObject.FindWithTag("Player").GetComponent<PlayerHit>();
            dropManager = GameObject.Find("DropManager").GetComponent<DropManager>();
        }
    }
}