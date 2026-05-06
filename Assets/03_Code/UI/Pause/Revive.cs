using System;
using UnityEngine;

namespace _03_Code.UI.Pause {
    public class Revive : MonoBehaviour {
        private void Start() {
            gameObject.SetActive(false);
        }
    }
}