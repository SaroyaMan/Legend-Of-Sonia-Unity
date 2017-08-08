using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeSinceLastHit = 2f;
    [SerializeField] private Slider healthSlider;

    private float timer;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;
    private AudioSource audioSource;
    private ParticleSystem blood;

    public int CurrentHealth {
        get {
            return currentHealth;
        }
        set {
            currentHealth = value < 0 ? 0 : value;
        }
    }

    private void Awake() {
        Assert.IsNotNull(healthSlider);
    }

    // Use this for initialization
    private void Start () {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audioSource = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update () {
        timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other) {
        if(timer >= timeSinceLastHit && !GameManager.instance.IsGameOver) {
            if(other.tag == "Weapon") {
                TakeHit();
                timer = 0;
            }
        }
    }

    private void TakeHit() {
        if(currentHealth > 0) {     //Player still alive
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audioSource.PlayOneShot(audioSource.clip);
            blood.Play();
        }
        if(currentHealth <= 0) {
            KillPlayer();
        }
    }

    private void KillPlayer() {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        audioSource.PlayOneShot(audioSource.clip);
        blood.Play();
    }

    public void PowerUpHealth(int health) {
        if(CurrentHealth <= startingHealth - health) {
            CurrentHealth += health;
        }
        else if (CurrentHealth < startingHealth) {
            CurrentHealth = startingHealth;
        }
        healthSlider.value = CurrentHealth;
    }
}
