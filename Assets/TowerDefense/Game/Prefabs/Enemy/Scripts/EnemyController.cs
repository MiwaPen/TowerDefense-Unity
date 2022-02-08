using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IPooledObject{
    [SerializeField] private float _speed;
    private Rigidbody _rb;
    private LineRenderer _line;
    private int _lineLength;
    private Vector3 _nextPoint;
    private int _nextPointIndex;
    private bool _isMoving;

    public void OnObjectSpawn() {
        _line = FindObjectOfType<LineRenderer>();
        _lineLength = _line.positionCount-1;
        _rb = this.gameObject.GetComponent<Rigidbody>();
        StartMoving();
    }

    public void StartMoving() {
        _nextPointIndex = 0;
        _nextPoint = new Vector3(
            _line.GetPosition(_nextPointIndex).x, _line.GetPosition(_nextPointIndex).y, _line.GetPosition(_nextPointIndex).z);
        _rb.transform.position = _nextPoint;
        UpdateNextPoint();
        this.gameObject.SetActive(true);
        _isMoving = true;
     
    }
    private void Update() {
        Vector3 targetDirection = _nextPoint - transform.position;
        float singleStep = 5f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        Debug.DrawRay(_rb.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (_isMoving) {

            if (Mathf.RoundToInt(Vector3.Distance(_rb.transform.position,_nextPoint))>0 )
                _rb.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            else {

                if (_nextPointIndex < _lineLength) 
                    UpdateNextPoint();
                else { _isMoving = false; this.gameObject.SetActive(false); }
            }
        }
    }

    private void UpdateNextPoint() {
        _nextPointIndex++;
        _nextPoint = new Vector3(
            _line.GetPosition(_nextPointIndex).x,
            _line.GetPosition(_nextPointIndex).y,
            _line.GetPosition(_nextPointIndex).z);
    }
}
