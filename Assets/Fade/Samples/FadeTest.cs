using UnityEngine;
using System.Collections;

public class FadeTest : MonoBehaviour 
{
	public GameObject Start_SE;
    public bool start = true;
	private bool isMainColor = false;
	[SerializeField] Color color1 = Color.white, color2 = Color.white;
	[SerializeField] UnityEngine.UI.Image image = null;

	[SerializeField]
	CanvasGroup group = null;

	[SerializeField]
	Fade fade = null;

    public void Update()
    {
        if (Input.GetKeyDown("space")) { Fadeout(); }
    }

    public void Fadeout()
	{
        if (start == true)
        {
            Instantiate(Start_SE, new Vector3(0, 0, 0), Quaternion.identity);
            start = false;
        }
		group.blocksRaycasts = false;
		fade.FadeIn (1, () =>
		{
			image.color = (isMainColor) ? color1 : color2;
			isMainColor = !isMainColor;
//			fade.FadeOut(1, ()=>{
//				group.blocksRaycasts = true;
//			});
		});
	}
}
