using UnityEngine;

public class SpeedPowerUp: MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;
    private AudioSource audioSource;
    private SpriteRenderer sprite;

    private void Start() {
        player = GameManager.instance.Player;
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        GameManager.instance.RegisterPowerUp();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            sprite.enabled = false;
            audioSource.PlayOneShot(audioSource.clip);
            playerController.PowerUpSpeed();
            GameManager.instance.UnregisterPowerUp();
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
