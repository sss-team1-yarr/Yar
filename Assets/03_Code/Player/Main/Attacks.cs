using System.Collections;
using _03_Code.Items;
using _03_Code.Items.Weapons;
using _03_Code.Player.Components;
using _03_Code.Player.Input;
using _03_Code.Player.Interface;
using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Player.Main {
    public class Attacks : MonoBehaviour, IPlayerModule {
        [Header("Components")] [SerializeField]
        private Sword sword;

        [SerializeField] private HpManager hp;
        [field: SerializeField] public Weapon HoldingItem { get; private set; }

        [Header("VFX")] [SerializeField] private ParticleSystem vfx;

        [SerializeField] private GameObject vfxBoom;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        [Header("Settings")] [SerializeField] private int explosion;

        [SerializeField] private float skill3ActiveTime;
        private InputReceiver _input;

        private Player _owner;

        public bool IsFFF { get; private set; }

        private void OnDestroy() {
            _input.OnAttackInput -= HandleAttackInput;
            _input.OnSkill1Input -= HandleSkill1Input;
            _input.OnSkill3Input -= HandleSkill3Input;
        }

        public void Initialize(Player owner) {
            _owner = owner;
            _input = _owner.GetModule<InputReceiver>();
            _input.OnAttackInput += HandleAttackInput;
            _input.OnSkill1Input += HandleSkill1Input;
            _input.OnSkill3Input += HandleSkill3Input;
            HoldingItem?.HoldItem(new ItemUsingContext { User = _owner, Input = 0, Pressed = true });
        }

        private void HandleAttackInput(int btn, bool pressed) {
            HoldingItem?.Use(new ItemUsingContext {
                Input = btn,
                Pressed = pressed,
                User = _owner
            });
        }

        private void HandleSkill1Input() {
            hp.Damage(explosion);
            vfxBoom.transform.position = transform.position;
            impulseSource.GenerateImpulseWithForce(1f);
            //for (var i = 0; i < testDam.Length; i++) testDam[i]?.largePush();
            vfx.Play();
        }

        private void HandleSkill3Input() {
            if (IsFFF) return;
            StartCoroutine(FFFActive());
        }

        private IEnumerator FFFActive() {
            IsFFF = true;
            yield return new WaitForSeconds(skill3ActiveTime);
            IsFFF = false;
        }
    }
}