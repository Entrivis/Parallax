using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    static GameObject player; //Using static to make sure that the value is never disappear after resets
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void CallDie()
    {
        StartCoroutine(nameof(Die));
    }

    IEnumerator Die()
    {
        player.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
