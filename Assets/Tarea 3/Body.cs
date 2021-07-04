using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Body : MonoBehaviour
{
    [Header("Movement")]
    public Transform previousObject;
    public float maxDistance = 2;
    [Header("Attack State")]
    public float detectionRadius = 3f;
    public Player player;
    public List<GameObject> spikes;

    private bool inAttackMode = false;
    
    private void Update() 
    {
        DetectPlayer();
        FollowObject();
    }

    private void DetectPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Body does not have assigned the player!");
            return;
        }
        
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectionRadius && !inAttackMode)
        {
            inAttackMode = true;
            SetSpikes(true);
        }
        else if (distance > detectionRadius && inAttackMode)
        {
            inAttackMode = false;
            SetSpikes(false);
        }
    }

    private void SetSpikes(bool mode)
    {
        if (spikes.Any(x => x == null))
        {
            Debug.LogError("Body does not have spikes assigned!");
            return;
        }
        
        foreach (GameObject item in spikes)
        {
            item.SetActive(mode);
        }
    }
    
    private void FollowObject()
    {
        if (previousObject == null)
        {
            Debug.LogError("Body does not have an object to follow!");
            return;
        }
        
        float distance = Vector3.Distance(transform.position, previousObject.position);
        
        if (distance > maxDistance) 
        {
            Vector3 followToCurrent = (transform.position - previousObject.position).normalized;
            followToCurrent.Scale(new Vector3(maxDistance, maxDistance, maxDistance));
            
            transform.position = previousObject.position + followToCurrent;
            transform.LookAt(previousObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
