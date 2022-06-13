using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController: MonoBehaviour
{
    //実績用回数カウンター
    float Jump_Counter;
    public static float Dash_Counter;
    //プレイ情報用カウンター
    public static float Total_Jump;
    public static float Total_Dash;
    //スマートフォンボタン対応用ボタン変数
    bool Smart_Bottom_Boost = false;

    const int MinLane = -2;
	const int MaxLane = 2;
	const float LaneWidth = 1.0f;
	const int DefaultLife = 3;
	public float BoostPower = 1000;
	public float CoolTime = 0;
	const  float BoostGages = 0;
	const float StunDuration = 0.5f;
	int chain = 0;
    public static float Total_Chain;
	int total_chain=0;
	float total_chain_time=0f;
	float one_time = 1;
	float chainBonus=0;
	float invincible=0;
	GameObject Dash;
	GameObject Dash_kasoku;

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;
	int targetLane;
	int life = DefaultLife;
	float recoverTime = 0.0f;
	public GameObject attack_effect;
	public GameObject attack ;
	public Text chainLabel;
	public Text chainBonusLabel;
	public Text total_chain_Label;
	public Text chain_plus_Label;
	public Text high_scoreLabel;
	public Image Gage;
	public float gravity;
	public float speedZ;
	public float speedX;
	public float speedJump;
	public float accelerationZ;
	public float acceleration2Z;
	public GameObject hit_effect;
	public GameObject Damage_SE;
	public GameObject Dash_SE;
	public GameObject Dash_kasoku_SE;
	public GameObject Dash_End_SE;
	public GameObject Jamp_SE;
	public GameObject Attack_SE;
	public GameObject LevelUp_SE;
    public GameObject Side_SE;
    public GameObject muteki_effect;
	public Animator animator2;
	public Animator geage_anime;
	public Animator camera_anime;
	public Animator chain_animation;
	public Animator chainBonus_animation;
	public Animator chainPlus_animation;
	public Animator score_upper;
	public Animator BgmController;
	public PlayerController player;
	public AudioSource Bgm;
	bool BoostSwitch = true;
	bool BoostDown = false;
	bool BoostCooltime = false;
	bool Chain_start=true;
	bool total_chain_count = false;
	bool new_score_upper = true;
	bool new_totalchain_upper = true;
    bool muteki_effect_destroy = false;
    bool muteki_effect_destroy_chain = false;
    bool One = true;
	bool Two = true;
	bool Thre = true;
	bool four = true;
	bool five = true;
	bool six = true;
	bool seven = true;
	bool eight = true;
	bool nine = true;
    bool ten = true;
    bool eleven = true;
    bool twelve = true;
    bool thirteen = true;

    public int Life ()
	{
		return life;
	}

	public bool IsStan ()
	{
		return recoverTime > 0.0f || life <= 0;
	}

	void Start ()
	{
		// 必要なコンポーネントを自動取得
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

        //実績用カウンター初期化
        Dash_Counter = 0;
        Jump_Counter = 0;

        //プレイヤー情報の取得
        Total_Jump = PlayerPrefs.GetFloat("totalJump");
        Total_Dash =  PlayerPrefs.GetFloat("totalDash");

    }

    void Update()
    {



            float range = (int)player.transform.position.z;
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();
        if (Input.GetKeyDown(KeyCode.Z)) OnClickBoostBottom();
        //Debug.Log (range);

        if(Smart_Bottom_Boost == true){
          Boost();

        }

        //無敵時間の減少
        invincible = invincible - Time.deltaTime;


		if (IsStan())
		{
			// 動きを止め気絶状態からの復帰カウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		}
		else
		{
			if (range < 100) {
				// 徐々に加速しZ方向に常に前進させる
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
				BgmController.SetBool ("damage", false);
			} else if (range < 200) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (One) {
					plusForce ();
					One = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 300) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (Two) {
					plusForce ();
					Debug.Log ("加速しました");
					Two = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 700) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (Thre) {
					plusForce ();
					Debug.Log ("加速しました");
					Thre = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 1400) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (four) {
					plusForce2();
					Debug.Log ("加速しました");
					four = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 2500) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (five) {
					plusForce2();
					Debug.Log ("加速しました");
					five = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 5000) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (six) {
					plusForce2 ();
					Debug.Log ("加速しました");
					six = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 7500) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (seven) {
					plusForce2 ();
					Debug.Log ("加速しました");
					seven = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 10000) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (eight) {
					plusForce2 ();
					Debug.Log ("加速しました");
					eight = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 12500) {
				float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
				BgmController.SetBool ("damage", false);
				if (nine) {
					plusForce2 ();
					Debug.Log ("加速しました");
					nine = false;
				}
				moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
			}else if (range < 15000){
                float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
                BgmController.SetBool("damage", false);
                if (ten)
                {
                    plusForce2();
                    Debug.Log("加速しました");
                    ten = false;
                }
                moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            }else if (range < 17500){
                float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
                BgmController.SetBool("damage", false);
                if (eleven)
                {
                    plusForce2();
                    Debug.Log("加速しました");
                    eleven = false;
                }
                moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            }else if (range < 2000)
            {
                float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
                BgmController.SetBool("damage", false);
                if (twelve)
                {
                    plusForce2();
                    Debug.Log("加速しました");
                    twelve = false;
                }
                moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            }else if (range < 100000){
                float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
                BgmController.SetBool("damage", false);
                if (thirteen)
                {
                    plusForce();
                    Debug.Log("加速しました");
                    thirteen = false;
                }
                moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);
            }

            // X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
			moveDirection.x = ratioX * speedX;
		}

			//ブーストのonの管理
		if (Smart_Bottom_Boost == false  || BoostDown == false) {
			if (BoostPower < 1000) {
					BoostPower = BoostPower +(60f * Time.deltaTime);
				//chainBonusの時間減少
				Debug.Log(chainBonus);
				chainBonus = chainBonus - Time.deltaTime;
			}
				BoostSwitch = true;
		}

		//ブースト終了時の減速
		if (BoostSwitch == true && BoostDown == true) {
			Destroy (attack.gameObject);
			GameObject Dash_End_SE2 = Instantiate (Dash_End_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
			Destroy(Dash_End_SE2,3);
			speedZ = speedZ / 8f;
			geage_anime.SetBool("boost_animation",false);
			camera_anime.SetBool ("blar", false);
			chainBonus_animation.SetBool ("upper", true);
			BoostDown = false;
			BoostCooltime = true;
			Chain_start = true;
            if (chainBonus >= 1) {
                GameObject muteki = Instantiate(muteki_effect, new Vector3(player.transform.position.x, player.transform.position.y  -0.2f, player.transform.position.z ), Quaternion.identity);
                muteki.transform.parent = player.transform;
                muteki_effect_destroy_chain = true;
            }
			Invoke("chaincontroller", 2.0f);
			destroy_method ();
		}


		//ブーストゲージの表示
		float BoostGages = BoostPower;
		Gage.fillAmount = BoostGages/1000f;

		// 重力分の力を毎フレーム追加
		moveDirection.y -= gravity * Time.deltaTime;

		// 移動実行
		Vector3 globalDirection = transform.TransformDirection(moveDirection);
		controller.Move(globalDirection * Time.deltaTime);

		// 移動後接地してたらY方向の速度はリセットする
		if (controller.isGrounded) moveDirection.y = 0;

		// 速度が0以上なら走っているフラグをtrueにする
		animator.SetBool("run", moveDirection.z > 0.0f);

		//ブーストのクールタイムの作成
			if (BoostCooltime) {
				CoolTime =CoolTime + Time.deltaTime;
				//Debug.Log (CoolTime);
				if (CoolTime > 4) {
					BoostCooltime = false;
					CoolTime = 0;
				geage_anime.SetBool("cool_down",true);
				}
			}

		//chainアニメーション
		if(total_chain_count){
			one_time -= Time.deltaTime;
			if (total_chain_time < chain) {
				if (one_time <= 0) {
					one_time = 0.01f;
					total_chain++;
                    Total_Chain++;
                    total_chain_time ++;
					total_chain_Label.text = " : " + total_chain;
				}
			} else {
				if (PlayerPrefs.GetInt ("TotalChain") < total_chain && Game_Actuary.cheat_mode == false) {
					PlayerPrefs.SetInt ("TotalChain", total_chain);
					if (new_totalchain_upper) {
						high_scoreLabel.text = "NEW Total Chain RECORD";
						score_upper.SetBool ("upper", true);
						Invoke ("NewScoreLabel", 3f);
						new_totalchain_upper = false;
					}
                }
				total_chain_count = false;
				total_chain_time = 0;
				chainPlus_animation.SetBool ("upper", false);
			}

            //実績の確認
            if (total_chain>300) {
                Game_Actuary.one_chain = 1;
                PlayerPrefs.SetInt("One_Chain", Game_Actuary.one_chain);
            }

            if (total_chain > 700)
            {
                Game_Actuary.two_chain = 1;
                PlayerPrefs.SetInt("Two_Chain", Game_Actuary.two_chain);
            }

            if (total_chain > 1200)
            {
                Game_Actuary.three_chain = 1;
                PlayerPrefs.SetInt("Three_Chain", Game_Actuary.three_chain);
            }
        }


            //ハイスコアのお知らせ
            if (PlayerPrefs.GetInt ("HighScore") < player.transform.position.z && new_score_upper && Game_Actuary.cheat_mode == false) {
			high_scoreLabel.text="NEW HIGH SCORE RECORD";
			score_upper.SetBool ("upper", true);
			Invoke ("NewScoreLabel",3f);
			new_score_upper = false;
            }

        //無敵エフェクトの消去
        if (muteki_effect_destroy)
        {
            if (invincible <= 0)
            {
                // GameObject型の配列dashに、"dash"タグのついたオブジェクトをすべて格納
                GameObject[] mutekis = GameObject.FindGameObjectsWithTag("muteki");

                // GameObject型の変数cubeに、cubesの中身を順番に取り出す。
                // foreachは配列の要素の数だけループします。
                foreach (GameObject muteki in mutekis)
                {
                    // 消す！
                    Destroy(muteki);
                }
                muteki_effect_destroy = false;
            }
        }
        if (muteki_effect_destroy_chain)
        {
            if (chainBonus <= 1)
            {
                // GameObject型の配列dashに、"dash"タグのついたオブジェクトをすべて格納
                GameObject[] mutekis = GameObject.FindGameObjectsWithTag("muteki");

                // GameObject型の変数cubeに、cubesの中身を順番に取り出す。
                // foreachは配列の要素の数だけループします。
                foreach (GameObject muteki in mutekis)
                {
                    // 消す！
                    Destroy(muteki);
                }
                muteki_effect_destroy_chain = false;
            }
        }
    }

	// 左のレーンに移動を開始
	public void MoveToLeft ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane > MinLane) targetLane--;
        GameObject Side_SE2 = Instantiate(Side_SE, new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
        Destroy(Side_SE2, 3);
    }

	// 右のレーンに移動を開始
	public void MoveToRight ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane < MaxLane) targetLane++;
        GameObject Side_SE2 = Instantiate(Side_SE, new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
        Destroy(Side_SE2, 3);
    }

	//ジャンプの設定
	public void Jump ()
	{
		if (IsStan()) return;
		if (controller.isGrounded)
		{
			moveDirection.y = speedJump;

			GameObject Jamp_object = Instantiate (Jamp_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
			Destroy (Jamp_object, 3);

			// ジャンプトリガーを設定
			animator.SetTrigger("jump");

            //ジャンプ回数カウント
            Jump_Counter++;
            Total_Jump++;
            if (49 < Jump_Counter) {
                Game_Actuary.one_game_jump50 = 1;
               PlayerPrefs.SetInt("One_Game_50_Jump", Game_Actuary.one_game_jump50);
            }


        }
	}


	//序盤レベルアップ時の処理
	public void plusForce(){
		Debug.Log ("加速しました");
		animator2.SetTrigger("LevelUp2");
		speedZ = speedZ * 1.3f;
		GameObject LevelUp_SE2 = Instantiate (LevelUp_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
		Destroy(LevelUp_SE2,3);
	}

	//後半レベルアップ時の処理
	public void plusForce2(){
		Debug.Log ("加速しました");
		animator2.SetTrigger("LevelUp2");
		speedZ = speedZ * 1.1f;
		GameObject LevelUp_SE2 = Instantiate (LevelUp_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
		Destroy(LevelUp_SE2,3);
	}

	public void destroy_method(){
		// GameObject型の配列dashに、"dash"タグのついたオブジェクトをすべて格納
		GameObject[] dashs = GameObject.FindGameObjectsWithTag("Dash");

		// GameObject型の変数cubeに、cubesの中身を順番に取り出す。
		// foreachは配列の要素の数だけループします。
		foreach (GameObject dash in dashs) {
			// 消す！
			Destroy(dash);
	}
	}
	//ブーストのアクション
	public void Boost (){
		if (BoostPower < 1) {
			BoostSwitch = true;
      Smart_Bottom_Boost = false;
		} else {
			if (BoostCooltime == false ) {
				if (BoostPower > 0) {
                    //ブースト中の減少(ただしチート中は減らない)
                    if (Game_Actuary.cheat_mode == false)
                    {
                        BoostPower = BoostPower - (Time.deltaTime * 160);
                    }
					geage_anime.SetBool ("boost_animation", true);
					geage_anime.SetBool("cool_down",false);
					chain_animation.SetBool ("upper", true);
					camera_anime.SetBool ("blar", true);
					if (BoostSwitch) {
						attack = Instantiate (attack_effect, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 1f), Quaternion.identity);
						attack.transform.parent = player.transform;
						GameObject Dash=Instantiate (Dash_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 1f), Quaternion.identity);
						Dash_kasoku=Instantiate (Dash_kasoku_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 1f), Quaternion.identity);
						speedZ = speedZ * 8f;
						moveDirection.z = moveDirection.z * 2.5f;
						BoostSwitch = false;
						BoostDown = true;

                        //実績用ダッシュカウンターの加算
                        Dash_Counter++;
                        Total_Dash++;

                    }
					//Debug.Log (BoostPower);
				}
			}
		}
	}

	//chainの処理関数
	public void chaincontroller(){

		chain_animation.SetBool ("upper", false);
		chainBonus_animation.SetBool ("upper", false);
		chainPlus_animation.SetBool ("upper", true);
		chain_plus_Label.text = "+" + chain;
		total_chain_count = true;
	}

	public void NewScoreLabel(){
		score_upper.SetBool ("upper", false);
	}

  public void OnClickBoostBottom(){
    if(	BoostCooltime == false){
    if(Smart_Bottom_Boost == true){
      Smart_Bottom_Boost = false;
    }else if(Smart_Bottom_Boost == false){
    Smart_Bottom_Boost = true;
  }
}
  }


	// CharacterControllerにコンジョンが生じたときの処理
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (Smart_Bottom_Boost==false || BoostPower <0 || BoostDown == false) {
			if (chainBonus < 1 && invincible <= 0) {
				if (IsStan ())
					return;
			}

			if (hit.gameObject.tag == "Enemy") {
				Instantiate (hit_effect, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
				if(chainBonus<1 && invincible <=0){
					BgmController.SetBool ("damage", true);
					if(life>1){
				Instantiate (Damage_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
				}

				// ライフを減らして気絶状態に移行
				life--;
				recoverTime = StunDuration;

				//無敵時間の発生
					invincible = 4;
                    GameObject muteki = Instantiate(muteki_effect, new Vector3(player.transform.position.x, player.transform.position.y -0.2f, player.transform.position.z ), Quaternion.identity);
                    muteki.transform.parent = player.transform;
                    muteki_effect_destroy = true;

                    // ダメージトリガーを設定
                    animator.SetTrigger ("damage");}

				// ヒットしたオブジェクトは削除
				Destroy (hit.gameObject);

			}
		} else if (Smart_Bottom_Boost && BoostPower > 0){
			if(Chain_start){
				chain = 0;
					chainBonus=0;
				chainLabel.text = ""+chain;
				chainBonusLabel.text = 0+ "秒";
				Chain_start = false;
			}

			if (hit.gameObject.tag == "Enemy") {
				GameObject Hit_object = Instantiate (hit_effect, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
				GameObject Attack_SE2 = Instantiate (Attack_SE, new Vector3 (player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z + 0.5f), Quaternion.identity);
				//ブースト中に衝突時は回復
				BoostPower = BoostPower + 6;

				chain = chain + 1;
				chainBonus = chain * 0.1f;
				chainBonus = Mathf.Round(chainBonus * 10)/10;
				chainLabel.text = ""+chain;
				//チェインボーナスの表示
				if (chainBonus >= 1) {
					chainBonusLabel.text = chainBonus - 1 + "秒";

                }
                else {
					chainBonusLabel.text = 0+ "秒";
				}
				// ヒットしたオブジェクトは削除
				Destroy (hit.gameObject);
				Destroy (Hit_object,3);
				Destroy (Attack_SE2,3);
			}
		}
	}
}
