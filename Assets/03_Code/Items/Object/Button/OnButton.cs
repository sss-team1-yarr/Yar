using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace _03_Code.Items.Object.Button
{
    public class OnButton : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;

        private void Start()
        {
            enemy.SetActive(false);
        }

        public void PressedButton()
        {
            enemy.SetActive(true);
            Destroy(this);
        }
    }
}
