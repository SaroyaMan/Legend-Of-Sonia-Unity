using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

    [SerializeField] private Transform player;

    private NavMeshAgent nav;
    private Animator anim;

    void Awake() {
        Assert.IsNotNull(player);    
    }

    void Start () {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        nav.SetDestination(player.position);
	}
}
