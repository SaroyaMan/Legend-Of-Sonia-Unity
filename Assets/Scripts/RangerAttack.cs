using System.Collections;
using UnityEngine;

public class RangerAttack: MonoBehaviour {

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private Transform fireLocation;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private EnemyHealth enemyHealth;
    private GameObject arrow;

    private void Start() {
        arrow = GameManager.instance.Arrow;
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(Attack());
    }

    private void Update() {
        //Attack is in range and alive!
        if(Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive) {
            playerInRange = true;
            anim.SetBool("PlayerInRange", true);
            RotateTowards();
        }
        else {
            playerInRange = false;
            anim.SetBool("PlayerInRange", true);
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

    private void RotateTowards() {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    // called by event in the Animation of Ranger Model
    public void FireArrow() {
        GameObject newArrow = Instantiate(arrow) as GameObject;
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = transform.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 20f;  //20f is the speed of arrow
    }
}
