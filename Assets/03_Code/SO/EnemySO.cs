using UnityEngine;

namespace _03_Code.SO {
    [CreateAssetMenu(fileName = "Enemy data", menuName = "SO/Enemy", order = 0)]
    public class EnemySO : ScriptableObject {
        [Header("Enemy stat")] 
        public Sprite sprite;
        public string enemyName;
        public int speed;
        public int maxHealth;
        public int damage;
        public int dropExp;
    }
}