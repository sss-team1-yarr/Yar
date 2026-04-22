using System;
using System.Collections.Generic;
using System.Linq;
using _03_Code.Player.Components;
using _03_Code.Player.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.Player {
    public class Player : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private HpManager hp;
        [SerializeField] private float dashPower;
        [SerializeField] private GameObject vfxBoom;
        [SerializeField] private GameObject pause;
        [SerializeField] private AnimationControl ani;
        
        private bool _isActived = false;
        private Dictionary<Type, IPlayerModule> _moduleDictionary;
        
        private void Awake() {
            GetComponent<Player>();
            _moduleDictionary = GetComponentsInChildren<IPlayerModule>().ToDictionary(
                type => type.GetType());
            
            InitializeComponents();
        }

        public T GetModule<T>() {
            if(_moduleDictionary.TryGetValue(typeof(T), out var module)) return (T)module;
            
            var findModule = _moduleDictionary.Values.FirstOrDefault(moduleType => moduleType is T);
            
            if (findModule is T castedModule) return castedModule;
            
            return default;
        }

        private void InitializeComponents() {
            foreach (var module in _moduleDictionary.Values.OfType<IPlayerModule>()) {
                module.Initialize(this);
            }
        }

        private void Update() {
            if(hp.hp <= 0) HandlePlayerDeath();
            if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                switch (_isActived) {
                    case false:
                        pause.SetActive(true);
                        _isActived = !_isActived;
                        break;
                    case true:
                        pause.SetActive(false);
                        _isActived = !_isActived;
                        break;
                }
            }
        }

        private void HandlePlayerDeath() {
            Destroy(gameObject);
            Time.timeScale = 0;
        }

        private void LateUpdate() {
            ani.OnJumpAni(contactChecker.IsGrounded);
        }
    }
}