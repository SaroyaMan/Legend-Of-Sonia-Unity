using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject player;
    private bool isGameOver;

    public bool IsGameOver { get { return isGameOver; } }
    public GameObject Player { get { return player; } }

    public static GameManager instance = null;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void PlayerHit(int currentHP) {
        isGameOver = currentHP > 0 ? false : true;
    }
}
