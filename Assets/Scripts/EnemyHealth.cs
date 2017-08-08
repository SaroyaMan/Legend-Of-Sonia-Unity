using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;

    private float timer;
    private Animator anim;
    private AudioSource audioSource;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody ridgitBody;
    private CapsuleCollider capsuleCollider;
    private bool isDissapeared;
    private int currentHealth;
    private ParticleSystem blood;

    public bool IsAlive { get { return isAlive; } }

    void Start () {
        GameManager.instance.RegisterEnemy(this);
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        ridgitBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        isAlive = true;
        currentHealth = startingHealth;
        blood = GetComponentInChildren<ParticleSystem>();
    }

    void Update () {
        timer += Time.deltaTime;
        if(isDissapeared) {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other) {
        if(timer > timeSinceLastHit && !GameManager.instance.IsGameOver) {
            if(other.tag == "PlayerWeapon") {
                TakeHit();
                blood.Play();
                timer = 0f;
            }
        }
    }

    private void TakeHit() {
        if(currentHealth > 0) {     //Player still alive
            anim.Play("Hurt");
            currentHealth -= 10;
            audioSource.PlayOneShot(audioSource.clip);
        }
        if(currentHealth <= 0) {
            KillEnemy();
        }
    }

    private void KillEnemy() {
        GameManager.instance.KilledEnemy(this);
        isAlive = false;
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        ridgitBody.isKinematic = true;
        StartCoroutine(RemoveEnemy());
    }

    private IEnumerator RemoveEnemy() {
        yield return new WaitForSeconds(4f); // wait 4 seconds after enemy dies
        isDissapeared = true; // start to sink the enemy
        yield return new WaitForSeconds(2f); // after 2 seconds
        Destroy(gameObject); // destroy the game object
    }
}
