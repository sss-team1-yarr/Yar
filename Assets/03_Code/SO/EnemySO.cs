using UnityEngine;

namespace _03_Code.SO {
    [CreateAssetMenu(fileName = "Enemy data", menuName = "SO/Enemy", order = 0)]
    public class EnemySO : ScriptableObject {
        [Header("Enemy stat")] 
        public string name;
        public int health;
        public int speed;
        public int dropExp;
        
        [Header("Enemy stat/Approach")] 
        public float approachForce;
        public float approachTime;
        public int approachDamage;
    }
}