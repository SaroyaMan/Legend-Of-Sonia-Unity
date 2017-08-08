using UnityEngine;

public class Arrow : MonoBehaviour {

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
