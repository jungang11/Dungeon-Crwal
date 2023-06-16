using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    // Start is called before the first frame update
    private void TriggerSound(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range); 
    }

    public void TriggerSound(Transform transform, float range)
    {
        //Vector3 mapSoundOrigin = Extension.ConvertWorldtoMapPoint(transform); 
        //Vector3 mapSoundOrigin = GameManager.Map.mapPos.InverseTransformPoint(transform.position); 
        Sight soundOrigin = new Sight(transform.localPosition.x, transform.localPosition.z); 
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            IListenable listener = collider.GetComponent<IListenable>();
            listener?.ReturnHeardPosition(soundOrigin);
            //Return the sight which should print out tracable path for the listener. 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f); 
    }
}
