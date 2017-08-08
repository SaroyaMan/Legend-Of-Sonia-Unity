using UnityEngine;

public class HealthPowerUp : MonoBehaviour {

    [SerializeField] private int healthToAdd;

    private GameObject player;
    private PlayerHealth playerHealth;
    private AudioSource audioSource;
    private SpriteRenderer sprite;

    private void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        GameManager.instance.RegisterPowerUp();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            sprite.enabled = false;
            audioSource.PlayOneShot(audioSource.clip);
            playerHealth.PowerUpHealth(healthToAdd);
            GameManager.instance.UnregisterPowerUp();
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
