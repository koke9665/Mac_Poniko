using UnityEngine;
using System.Collections;

public class LifePanel : MonoBehaviour 
{
	public GameObject[] icons;

	// ライフに応じてスプライトを出し分ける
	public void UpdateLife (int life)
	{

        if (life == 2) {
            icons[2].active = false;
        } else if (life == 1) {
            icons[1].active = false;
        } else if(life == 0){
            icons[0].active = false;
        }
		//for (int i = 0; i < icons.Length; i++)
		//{
		///	if (i < life) icons[i].SetActive(true);
		//	else icons[i].SetActive(false);
		//}
	}
}