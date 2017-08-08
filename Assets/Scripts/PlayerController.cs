using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;
    private BoxCollider[] swordColliders;
    private GameObject fireTrail;
    private ParticleSystem fireTrailParticles;

	private void Start () {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
        fireTrail = GameObject.FindWithTag("Fire") as GameObject;
        fireTrailParticles =  fireTrail.GetComponent<ParticleSystem>();
        fireTrail.SetActive(false);
    }
	
	private void Update () {

        if(!GameManager.instance.IsGameOver) {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.SimpleMove(moveDirection * moveSpeed);

            if(moveDirection == Vector3.zero) {
                anim.SetBool("IsWalking", false);
            }
            else anim.SetBool("IsWalking", true);


            if(Input.GetMouseButtonDown(0)) {
                anim.Play("DoubleChop");
            }
            if(Input.GetMouseButtonDown(1)) {
                anim.Play("SpinAttack");
            }
        }
	}

    private void FixedUpdate() {

        if(!GameManager.instance.IsGameOver) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

            if(Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore)) {
                if(hit.point != currentLookTarget) {
                    currentLookTarget = hit.point;
                }
                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
    }

    private void BeginAttack() {
        foreach(var weapon in swordColliders) {
            weapon.enabled = true;
        }
    }

    private void EndAttack() {
        foreach(var weapon in swordColliders) {
            weapon.enabled = false;
        }
    }

    public void PowerUpSpeed() {
        StartCoroutine(FireTrail());
    }

    private IEnumerator FireTrail() {
        fireTrail.SetActive(true);
        moveSpeed = 10f;
        yield return new WaitForSeconds(10f);

        moveSpeed = 6f;
        var em = fireTrailParticles.emission;
        em.enabled = false;
        yield return new WaitForSeconds(3f);

        em.enabled = true;
        fireTrail.SetActive(false);
    }
}
