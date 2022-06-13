using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public PlayerController player;
	public Text scoreLabel;
	public Text levelLabel;
	public LifePanel lifePanel;
	public Animator animator;
	public Animator g;
	public Animator a;
	public Animator m;
	public Animator e;
	public Animator o;
	public Animator v;
	public Animator e2;
	public Animator r;
	public Animator fadeout;
	public Animator life;
	public Animator camera_fade;
	public Animator gage_GameOver;
	public Animator gage_max_GameOver;
	public GameObject GameOver_SE;
	public GameObject DirectSun;


    bool life_switch = true;
    //実績用変数
    public static float Total_Run;

	float sun_move;

	public void Start(){
        //ハイスコアを削除する（だめでした）
        //PlayerPrefs.DeleteAll();

        Total_Run = PlayerPrefs.GetFloat("totalRun");
        PlayerController.Total_Chain = PlayerPrefs.GetFloat("totalChain");
    }

	public void Update ()
	{

		//背景の太陽の移動
		sun_move = sun_move + Time.deltaTime;
		DirectSun.transform.rotation = Quaternion.Euler(50, sun_move, 0);

		// スコアラベルを更新
		int score = CalcScore();
		scoreLabel.text = "Score : " + score + "m";

        //実績条件クリア確認

        if (score > 10000) {
            Game_Actuary.one_score = 1;
            PlayerPrefs.SetInt("One_Score", Game_Actuary.one_score);
        }

        if (score > 17000)
        {
            Game_Actuary.two_score = 1;
            PlayerPrefs.SetInt("Two_Score", Game_Actuary.two_score);
        }

        if (score > 20000)
        {
            Game_Actuary.three_score = 1;
            PlayerPrefs.SetInt("Three_Score", Game_Actuary.three_score);
        }

        if (PlayerController.Dash_Counter < 11 && score > 10000)
        {
            Game_Actuary.one_game_dash10_score15000 = 1;
            PlayerPrefs.SetInt("One_Game_10_Dash_15_Score", Game_Actuary.one_game_dash10_score15000);
        }

        if (player.Life() <= 1 && score > 15000) {
            Game_Actuary.no_damage_score15000 = 1;
            PlayerPrefs.SetInt("No_Damage_15_Score", Game_Actuary.no_damage_score15000);
        }

        //レベル更新
        float level = levelScore();
		levelLabel.text = "Level : "+ level ;

        // ライフパネルを更新
        //lifePanel.UpdateLife(player.Life());


        
        if (player.Life() == 2 )
        {
            life.SetBool("Damage_1", true);
            if (life_switch)
            {
                Invoke("LifeTime", 1f);
                life_switch = false;
            }
        }
        else if (player.Life() == 1)
        {
            if (life_switch == false)
            {
                Invoke("LifeTime", 1f);
                life_switch = true;
            }
            life.SetBool("animation_1", true);
            life.SetBool("Damage_2", true);
        }
        else if (player.Life() == 0){
            Invoke("LifeTime", 1f);
            life.SetBool("Damage_3", true);
        }

            // プレイヤーのライフが0になったらゲームオーバー
            if (player.Life() <= 0)
		{
			Instantiate (GameOver_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
			g.SetBool("animation", true);
			a.SetBool("animattion", true);
			m.SetBool("animation", true);
			e.SetBool("animation", true);
			o.SetBool("animation", true);
			v.SetBool("animation", true);
			e2.SetBool("animation", true);
			r.SetBool("animation", true);
			camera_fade.SetBool("GameOver", true);
			gage_GameOver.SetBool("GameOver", true);
			gage_max_GameOver.SetBool("GameOver", true);

			// これ以降のUpdateは止める
			enabled = false;

            //ゲーム内実績の更新
            Total_Run += score;

            if (Game_Actuary.cheat_mode == false)
            {

                // ハイスコアを更新
                if (PlayerPrefs.GetInt("HighScore") < score)
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }

                //実績用変数のセーブ
                PlayerPrefs.SetFloat("totalRun", Total_Run);
                PlayerPrefs.SetFloat("totalChain", PlayerController.Total_Chain);
                PlayerPrefs.SetFloat("totalJump", PlayerController.Total_Jump);
                PlayerPrefs.SetFloat("totalDash", PlayerController.Total_Dash);

            }
            Invoke("fadeout_action", 3.0f);
			// 6秒後にReturnToTitleを呼びだす
			Invoke("ReturnToTitle", 6.0f);
		}
	}

	int CalcScore ()
	{
		// プレイヤーの走行距離をスコアとする
		return (int)player.transform.position.z;
	}

	float levelScore()
	{
		float levelPosition = (float)player.transform.position.z;
		float levels = 0;
		if (levelPosition < 100) {
			levels = 1f;
		} else if (levelPosition < 200) {
			levels = 2f;
		}else if (levelPosition < 300) {
			levels = 3f;
		}else if (levelPosition < 700) {
			levels = 4f;
		}else if (levelPosition < 1400) {
			levels = 5f;
		}else if (levelPosition < 2500) {
			levels = 6f;
		}else if (levelPosition < 5000) {
			levels = 7f;
		}else if (levelPosition < 7500) {
			levels = 8f;
		}else if (levelPosition < 10000) {
			levels = 9f;
		}else if (levelPosition < 12500) {
			levels = 10f;
		}else if (levelPosition < 15000){
            levels = 11f;
        }else if (levelPosition < 17500){
            levels = 12f;
        }else if (levelPosition < 20000){
            levels = 13f;
        }else if (levelPosition < 1000000){
            levels = 14f;
        }
        return levels ;
	}

	void fadeout_action ()
	{
		Debug.Log ("フェードアウトします。");
	fadeout.SetBool("fadeout", true);
	}

	void ReturnToTitle ()
	{
        // タイトルシーンに切り替え
        SceneManager.LoadScene("Title");
	}

    void LifeTime() {
        // ライフパネルを更新
        lifePanel.UpdateLife(player.Life());
    }
}
