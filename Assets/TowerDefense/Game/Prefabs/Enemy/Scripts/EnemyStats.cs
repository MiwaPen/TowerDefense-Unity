using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats{
    [SerializeField] public float healPoints;
    [SerializeField] public float enemyDamage;

    public void GetDamage(float damage){
        if (healPoints > 0)
            healPoints -= damage;
        IsAlive();
    }

    private void IsAlive(){
        if (healPoints <= 0)
            this.gameObject.SetActive(false);
    }
}
