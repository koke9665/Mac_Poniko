using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Actuary : MonoBehaviour {

    public Text Total_Run_Label;
    public Text Total_Chain_Label;
    public Text Total_Jump_Label;
    public Text Total_Dash_Label;

    public static int one_game_jump50;
    public static int one_game_dash10_score15000;
    public static int no_damage_score15000;
    public static int one_score;
    public static int two_score;
    public static int three_score;
    public static int one_chain;
    public static int two_chain;
    public static int three_chain;
    public GameObject Clear_Mark1;
    public GameObject Clear_Mark2;
    public GameObject Clear_Mark3;
    public GameObject Clear_Mark4;
    public GameObject Clear_Mark5;
    public GameObject Clear_Mark6;
    public GameObject Clear_Mark7;
    public GameObject Clear_Mark8;
    public GameObject Clear_Mark9;

    public GameObject comment;
    public GameObject comment2;

    public Animator Quit_Actuary;

    //チートモードの設定
    public static bool cheat_mode = false;
    public Text Cheat_Comment;


    // Use this for initialization
    void Start () {

        if (cheat_mode)
        {
            Cheat_Comment.text = "チートモードON";
        }
        else if (cheat_mode == false)
        {
            Cheat_Comment.text = "チートモードOFF";
        }

        //トータル走行距離とチェインの表示
        GameController.Total_Run = PlayerPrefs.GetFloat("totalRun");
        PlayerController.Total_Chain = PlayerPrefs.GetFloat("totalChain");

        Total_Run_Label.text =  GameController.Total_Run + "m";
        Total_Chain_Label.text = PlayerController.Total_Chain + "Chain";
        Total_Jump_Label.text = PlayerPrefs.GetFloat("totalJump") + "";
        Total_Dash_Label.text = PlayerPrefs.GetFloat("totalDash") + "";

        //実績の確認

        //10000メートル達成
        one_score = PlayerPrefs.GetInt("One_Score");
        if (one_score == 1)
        {
           // Debug.Log("1万メートル達成");
            Clear_Mark1.active = true;
        }

        //20000メートル達成
        two_score = PlayerPrefs.GetInt("Two_Score");
        if (two_score == 1)
        {
           // Debug.Log("2万メートル達成");
            Clear_Mark2.active = true;
        }

        //25000メートル達成
        three_score = PlayerPrefs.GetInt("Three_Score");
        if (three_score == 1)
        {
          //  Debug.Log("2万5千メートル達成");
            Clear_Mark3.active = true;
        }

        //100チェイン達成
        one_chain = PlayerPrefs.GetInt("One_Chain");
        if (one_chain == 1)
        {
          //  Debug.Log("100チェイン達成");
            Clear_Mark4.active = true;
        }

        //100チェイン達成
        two_chain = PlayerPrefs.GetInt("Two_Chain");
        if (two_chain == 1)
        {
          //  Debug.Log("300チェイン達成");
            Clear_Mark5.active = true;
        }

        //100チェイン達成
        three_chain = PlayerPrefs.GetInt("Three_Chain");
        if (three_chain == 1)
        {
          //  Debug.Log("700チェイン達成");
            Clear_Mark6.active = true;
        }

        //1ゲーム内にジャンプ50回
        one_game_jump50 = PlayerPrefs.GetInt("One_Game_50_Jump");
        if (one_game_jump50 == 1) {
         //   Debug.Log("1ゲーム内にジャンプ50回クリア確認");
            Clear_Mark7.active = true;
        }

        //10回以下のダッシュで1万5千メートル
        one_game_dash10_score15000 = PlayerPrefs.GetInt("One_Game_10_Dash_15_Score");
        if (one_game_dash10_score15000 == 1)
        {
           // Debug.Log("ダッシュ10回以下の1万5千メートルクリア確認");
            Clear_Mark8.active = true;
        }

        //一度もダメージを受けずに1万5千メートル
        no_damage_score15000 = PlayerPrefs.GetInt("No_Damage_15_Score");
        if (no_damage_score15000 == 1)
        {
           // Debug.Log("ノーダメージ1万5千メートルクリア確認");
            Clear_Mark9.active = true;
        }

        //全ての条件を満たすと隠しコマンド出現！
        if (Clear_Mark9.active && Clear_Mark8.active && Clear_Mark7.active && Clear_Mark6.active && Clear_Mark5.active && Clear_Mark4.active && Clear_Mark3.active && Clear_Mark2.active && Clear_Mark1.active) {
            comment2.active = true;
            comment.active = false;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Clear_Mark9.active && Clear_Mark8.active && Clear_Mark7.active && Clear_Mark6.active && Clear_Mark5.active && Clear_Mark4.active && Clear_Mark3.active && Clear_Mark2.active && Clear_Mark1.active)
        {
            comment2.active = true;
            comment.active = false;
        }

    }

    public void OnClickReturnTitle()
    {
        Quit_Actuary.SetBool("Quit",true);
        Invoke("Return",1f);

        
    }

    void Return() {
        SceneManager.LoadScene("Title");
    }

    public void OnClickCheat() {
        if (cheat_mode) {
            cheat_mode = false;
            Cheat_Comment.text = "チートモードOFF";
        }else if (cheat_mode == false) {
            cheat_mode = true;
            Cheat_Comment.text = "チートモードON";
        }
    }
}
