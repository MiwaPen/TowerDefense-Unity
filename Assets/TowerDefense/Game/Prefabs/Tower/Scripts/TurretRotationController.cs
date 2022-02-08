using UnityEngine;

public class TurretRotationController : MonoBehaviour{
    private Transform _turret;
    private Transform _target;
    private CannonController _cannonController;
    [SerializeField] private float _rotationSpeed;

    private void Start(){
        _cannonController = this.gameObject.GetComponent<CannonController>();
        _turret = this.transform;
    }

    private void Update(){

        if (_target != null)
        {

            if (_target.gameObject.activeSelf)
            {

                Vector3 targetDirection = _target.position - _turret.transform.position;
                float singleStep = _rotationSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(_turret.forward, targetDirection, singleStep, 0.0f);
                Debug.DrawRay(_turret.position, newDirection, Color.red);
                _turret.rotation = Quaternion.LookRotation(newDirection);

            }
            _cannonController.StartShooting();
        }
        else
            _cannonController.StopShooting();
    }

    public void SetTarget(Transform _target){ this._target = _target; }
    public void StopTargeting() { this._target = null; }

}
