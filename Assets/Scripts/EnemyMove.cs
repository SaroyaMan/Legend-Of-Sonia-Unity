using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;

    void Start () {
        player = GameManager.instance.Player.transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
	
	void Update () {
        if(!GameManager.instance.IsGameOver && enemyHealth.IsAlive) {
            nav.SetDestination(player.position);
        }
        else if(!enemyHealth.IsAlive) {
            nav.enabled = false;
        }
        else {
            nav.enabled = false;
            anim.Play("Idle");
        }
    }
}
