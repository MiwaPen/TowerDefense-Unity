using UnityEngine;

public class Damageable : MonoBehaviour{
    private IStats _stats;

    private void Start(){
        _stats = this.gameObject.GetComponent<IStats>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.GetComponent<AmmoController>() != null)
            _stats.GetDamage(other.gameObject.GetComponent<AmmoController>().damage);
    }
}
