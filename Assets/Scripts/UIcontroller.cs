using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour {

	Animator animator;
	
	public PlayerController player;
	public LifePanel lifePanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// ライフパネルを更新
		lifePanel.UpdateLife(player.Life());

		if (player.Life() <= 0)
		{
			animator.SetBool("GameOver", true);
		}
	}
}
