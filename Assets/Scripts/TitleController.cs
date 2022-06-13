using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
	public Text highScoreLabel;
	public Text totalChainLabel;
    public Animator Quit_title;

	public void Start ()
	{

        // ハイスコアを表示
        highScoreLabel.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";

		// ハイチェインの表示
		totalChainLabel.text = "High Total Chain :" + PlayerPrefs.GetInt("TotalChain") + "Chain";

        //SceneManager.UnloadScene("Main");

    }

    public void Update()
    {
        if (Input.GetKeyDown("space")) { Invoke("MainGame", 5.0f); }
    }

    public void OnStartButtonClicked ()
	{
		Invoke("MainGame", 5.0f);

	}

    public void OnActuaryButtonClicked()
    {
        Quit_title.SetBool("Quit", true);
        Invoke("Load_Actuary",1f);

    }

    public void Load_Actuary()
    {
        SceneManager.LoadScene("Actuary");
    }

    public void MainGame(){
        SceneManager.LoadScene("Main");
	}
}
