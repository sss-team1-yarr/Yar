using System;
using System.Collections.Generic;
using System.Linq;
using _03_Code.Player.Components;
using _03_Code.Player.Interface;
using UnityEngine;

namespace _03_Code.Player.Main {
    public class Player : MonoBehaviour {
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private HpManager hp;
        [SerializeField] private AnimationControl ani;

        private Dictionary<Type, IPlayerModule> _moduleDictionary;

        private void Awake() {
            GetComponent<Player>();
            _moduleDictionary = GetComponentsInChildren<IPlayerModule>().ToDictionary(type => type.GetType());

            InitializeComponents();
        }

        private void LateUpdate() {
            ani.OnJumpAni(contactChecker.IsGrounded);
        }
        
        public T GetModule<T>() {
            if (_moduleDictionary.TryGetValue(typeof(T), out var module)) return (T)module;

            var findModule = _moduleDictionary.Values.FirstOrDefault(moduleType => moduleType is T);

            if (findModule is T castedModule) return castedModule;

            return default;
        }

        private void InitializeComponents() {
            foreach (var module in _moduleDictionary.Values.OfType<IPlayerModule>()) module.Initialize(this);
        }

        public void HandlePlayerDeath() {
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }
}