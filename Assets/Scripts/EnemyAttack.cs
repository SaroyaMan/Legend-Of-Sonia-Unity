using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;
    private EnemyHealth enemyHealth;

	void Start () {
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(Attack());
    }
	
	void Update () {
        //Attack is in range and alive!
        if(Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive) { 
            playerInRange = true;
        } 
        else {
            playerInRange = false;
        }
	}

    private IEnumerator Attack() {
        if(playerInRange && !GameManager.instance.IsGameOver) {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(Attack());
    }

    public void EnemyBeginAttack() {
        foreach(var weapon in weaponColliders) {
            weapon.enabled = true;
        }
    }

    public void EnemyEndAttack() {
        foreach(var weapon in weaponColliders) {
            weapon.enabled = false;
        }
    }
}
