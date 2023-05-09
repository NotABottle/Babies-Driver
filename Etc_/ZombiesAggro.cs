using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesAggro : MonoBehaviour
{
    /*
    Zombie gets the distance between them and the player
    If within range,
    Move towards at some speed
    If out of range,
    Lerp towards starting position;
    */

    private Transform playerTransform;
    private Vector3 startPos;
    public float aggroDistance;
    public float attackDistance;
    [Range(0,1f)]
    public float moveSpeed;
    private int timeBetweenAttacks;
    private float timeOfLastAttack;

    private void Awake(){
        var yes = GameObject.FindGameObjectWithTag("PlayerBody");
        playerTransform = yes.transform;
        startPos = transform.position;
    }

    private void Update(){
        var distanceToPlayer = Vector3.Distance(playerTransform.position,transform.position);
        if(distanceToPlayer <= aggroDistance){
            //Move towards player
            var dirFromZomToPlayer = playerTransform.position - transform.position;
            transform.Translate(dirFromZomToPlayer * moveSpeed * Time.deltaTime);
        }
        else{
            //Move towards starting position
            transform.position = Vector3.MoveTowards(transform.position,startPos,float.MaxValue);
        }

        if(distanceToPlayer <= attackDistance){
            var timeSinceLastAttack = Time.timeSinceLevelLoad - timeOfLastAttack;
            if(timeSinceLastAttack > timeBetweenAttacks){
                //Deal Damage
                EventManager.TriggerEvent("DealDamage",null);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,aggroDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackDistance);
    }
}
