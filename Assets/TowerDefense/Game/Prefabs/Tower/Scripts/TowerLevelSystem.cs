using UnityEngine;

public class TowerLevelSystem : MonoBehaviour{
    [SerializeField] private float _radiosIncreaser;
    [SerializeField] private float _attackSpeedIncreaser;
    [SerializeField] private float _radiosLimit;
    [SerializeField] private float _attackSpeedLimit;
    private FieldOfView fieldOfView;
    private CannonController cannon;

    private void Start(){
        fieldOfView = this.gameObject.GetComponentInChildren<FieldOfView>();
        cannon = this.gameObject.GetComponentInChildren<CannonController>();
    }

    public void IncreaseRadios(){
        if(fieldOfView.radios< _radiosLimit)
            fieldOfView.radios += _radiosIncreaser;
    }

    public void IncreaseAttackSpeed(){
        if (cannon.shootingSpeed > _attackSpeedLimit)
            cannon.shootingSpeed -= _attackSpeedIncreaser;
    }
}
