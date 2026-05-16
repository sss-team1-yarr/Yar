using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class FinalSkill : MonoBehaviour {
        [SerializeField] private ParticleSystem boomParticles;
        
        public void HugeBoom() {
            boomParticles.Play();
        }
    }
}