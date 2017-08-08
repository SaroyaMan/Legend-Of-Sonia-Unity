using UnityEngine;

public class HealthPowerUp : MonoBehaviour {

    [SerializeField] private int healthToAdd;

    private GameObject player;
    private PlayerHealth playerHealth;

	private void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        GameManager.instance.RegisterPowerUp();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            playerHealth.PowerUpHealth(healthToAdd);
            GameManager.instance.UnregisterPowerUp();
            Destroy(gameObject);
        }
    }
}
