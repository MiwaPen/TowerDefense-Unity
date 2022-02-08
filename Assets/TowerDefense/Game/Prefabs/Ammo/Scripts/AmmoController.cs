using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AmmoController : MonoBehaviour, IPooledObject {
    [SerializeField] public float damage;
    private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    private void Start() {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn() {
        this.gameObject.SetActive(true);
    }

    public void Update() {
        _rigidbody.transform.Translate(Vector3.forward*_speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<EnemyController>()!=null) {
            this.gameObject.SetActive(false);
        }
    }
}
