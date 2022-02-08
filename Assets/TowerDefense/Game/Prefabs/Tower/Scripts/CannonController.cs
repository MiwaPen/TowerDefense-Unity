using System.Collections;
using UnityEngine;
using Zenject;

public class CannonController : MonoBehaviour{
    [SerializeField] public float shootingSpeed;
    private bool _canShoot;
    private Rigidbody _rigidbody;
    [Inject] private ObjectPooler _objectPooler;

    private void Start() {
        _canShoot = false;
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
       
    }

    private IEnumerator Shooting() {
        float delay = shootingSpeed;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (_canShoot) {
            yield return wait;
            Shoot();
        }
    }
    public void StartShooting() {

        if (_canShoot == false){
            _canShoot = true;
            StartCoroutine(Shooting());
        }
    }
    
    public void StopShooting() { StopCoroutine(Shooting()); _canShoot = false; }

    private void Shoot() {

        if(_canShoot)
            _objectPooler.SpawnFromPool(
                TagsHolder.ammo,
                _rigidbody.transform.position,
                _rigidbody.transform.rotation);
    }
}
