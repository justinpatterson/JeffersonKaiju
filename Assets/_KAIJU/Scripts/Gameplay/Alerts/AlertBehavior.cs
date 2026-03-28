using UnityEngine;

public class AlertBehavior : MonoBehaviour
{
    public Transform target;
    public Transform headNode;
    public Transform rootNode;
    private void Awake()
    {
        if (target != null) { AssignTarget(target); }
    }

    public void AssignTarget(Transform newTarget) 
    {
        target = newTarget;
        transform.position = target.position;
    }
    private void Update()
    {
        if (target) 
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*5f);
        }

        Quaternion rot = Quaternion.LookRotation(rootNode.position - Camera.main.transform.position);
        Vector3 eulerRot = rot.eulerAngles;
        eulerRot.x = 0f;
        eulerRot.z = 0f;
        rootNode.transform.eulerAngles = eulerRot;
    }

}
