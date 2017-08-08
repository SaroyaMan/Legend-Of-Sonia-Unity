using UnityEngine;

public class SpeedPowerUp: MonoBehaviour {

    private GameObject player;
    private PlayerController playerController;

    private void Start() {
        player = GameManager.instance.Player;
        playerController = player.GetComponent<PlayerController>();
        GameManager.instance.RegisterPowerUp();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            playerController.PowerUpSpeed();
            GameManager.instance.UnregisterPowerUp();
            Destroy(gameObject);
        }
    }
}
