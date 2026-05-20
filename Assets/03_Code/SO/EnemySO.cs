using UnityEngine;

namespace _03_Code.SO {
    [CreateAssetMenu(fileName = "Enemy data", menuName = "SO/Enemy", order = 0)]
    public class EnemySO : ScriptableObject {
        [Header("Enemy default")] public new string name;

        public float scale;
        public RuntimeAnimatorController animController;

        [Header("Enemy stat")] public int health;

        public int speed;
        public int dropExp;

        [Header("Approach")] public float approachForce;

        public float approachTime;
        public int approachDamage;

        [Header("Extra stat")] public float detectRange;

        public float knockBackForce;
        public float knockBackTime;
    }
}