using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI() {

        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position,Vector3.up,Vector3.forward,360,fov.radios);

        Vector3 viewAngle1 = DerectionFromAngle(fov.transform.eulerAngles.y,-fov.angle/2);
        Vector3 viewAngle2 = DerectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngle1 * fov.radios);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.radios);

        if (fov.canSeeTarget){
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.curretnTarget.transform.position);
        }
    }

    private Vector3 DerectionFromAngle(float eulerY,float angleInDegrees) {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
