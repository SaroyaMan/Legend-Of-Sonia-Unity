using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject tanker;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject ranger;

    private Animator heroAnim;
    private Animator tankerAnim;
    private Animator soldierAnim;
    private Animator rangerAnim;

    private void Awake() {
        Assert.IsNotNull(hero);
        Assert.IsNotNull(tanker);
        Assert.IsNotNull(soldier);
        Assert.IsNotNull(ranger);
    }

    private void Start () {
        heroAnim = hero.GetComponent<Animator>();
        tankerAnim = tanker.GetComponent<Animator>();
        soldierAnim = soldier.GetComponent<Animator>();
        rangerAnim = ranger.GetComponent<Animator>();
        StartCoroutine(ShowCase());
    }
	
	void Update () {
		
	}

    private IEnumerator ShowCase() {
        yield return new WaitForSeconds(1.2f);
        heroAnim.Play("SpinAttack");
        yield return new WaitForSeconds(1.2f);
        tankerAnim.Play("Attack");
        yield return new WaitForSeconds(1.2f);
        soldierAnim.Play("Attack");
        yield return new WaitForSeconds(1.2f);
        rangerAnim.Play("Attack");
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(ShowCase());
    }

    public void Battle() {
        SceneManager.LoadScene("Game_Scene");
    }

    public void Quit() {
        Application.Quit();
    }
}
