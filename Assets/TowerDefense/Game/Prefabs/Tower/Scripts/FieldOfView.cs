using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour{
    public float radios;
    [Range(0,360)] public float angle;
    public GameObject[] enemyRef;
    public GameObject curretnTarget;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeeTarget;
    [SerializeField] private TurretRotationController _turret;

    private void Start(){
        enemyRef = GameObject.FindGameObjectsWithTag("Enemy");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck(){
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position,radios,targetMask);

        if (rangeCheck.Length != 0){

            Transform target = FindTargetInMinDistance(rangeCheck);
            curretnTarget = target.gameObject;
            Vector3 directionToTarger = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarger) < angle / 2){

                float distanceToTarger = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarger, distanceToTarger, obstructionMask)){
                    canSeeTarget = true;
                    _turret.SetTarget(curretnTarget.transform);
                }

                else {
                    canSeeTarget = false;
                    _turret.StopTargeting();
                } 
                   
            }
            else 
                canSeeTarget = false;
        }
        else if (canSeeTarget) 
            canSeeTarget = false;
    }

    private Transform FindTargetInMinDistance(Collider[] range){
        Transform targetInMinDistanse = range[0].transform;

        foreach (Collider target in range){
            if (Vector3.Distance(transform.position, target.transform.position)
                < Vector3.Distance(transform.position, targetInMinDistanse.position)){
                Vector3 directionToTarger = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarger) < angle / 2)
                    targetInMinDistanse = target.transform;
            } 
        }

        return targetInMinDistanse;
    }
}
