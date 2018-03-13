using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainUIController : MonoBehaviour {
	private static MainUIController _instance;
	public static MainUIController Instance{
		get{ 
			return _instance;
		}
	}
	public bool hasBorder=true;
	public int score = 0;
	public int length=0;
	public Text msgText;
	public Text scoreText;
	public Text lengthText;
	public Image pauseImage;
	public Sprite[] pauseSprites;
	public Image bgImage;
	private Color tempColor;
    public  bool isPause=false;
	public int direction = 1;
	void Awake(){
		_instance = this;
	}

	void Start(){
		
		if (PlayerPrefs.GetInt ("border", 1) == 0) {
			hasBorder = false;
			foreach (Transform t in bgImage.gameObject.transform) {
				t.gameObject.GetComponent<Image> ().enabled = false;
			}
		}

	}
	void Update(){
		if (PlayerPrefs.GetString ("gift").Equals ("1") && score > 520) {
			Debug.Log ("gift");
			UnityEngine.SceneManagement.SceneManager.LoadScene (2);
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
		switch (score/100) {
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
		case 4:
			ColorUtility.TryParseHtmlString ("#CCEEFFFF", out tempColor);
			bgImage.color = tempColor;
			msgText.text = "二阶段";
			break;
		case 5:
		case 6:
			ColorUtility.TryParseHtmlString ("#CCEEDBFF", out tempColor);
			bgImage.color = tempColor;
			msgText.text = "三阶段";
			break;
		case 7:
		case 8:
			ColorUtility.TryParseHtmlString ("#EBFFCCFF", out tempColor);
			bgImage.color = tempColor;
			msgText.text = "四阶段";
			break;
		case 9:
		case 10:
			ColorUtility.TryParseHtmlString ("#FFF3CCFF", out tempColor);
			bgImage.color = tempColor;
			msgText.text = "五阶段";
			break;
		case 11:
			ColorUtility.TryParseHtmlString ("#FFDACCFF", out tempColor);
			bgImage.color = tempColor;
			msgText.text = "无尽阶段";
			break;
		default:
			break;
		}
	}
	public void UpdateUI(int s=5,int l=1){
		score += s;
		length += l;
		scoreText.text = "得分:\n" + score;
		lengthText.text = "长度:\n" + length;
	}
	public void Pause(){
		isPause = !isPause;
		if (isPause) {
			Time.timeScale = 0;
			pauseImage.sprite = pauseSprites [1];

		
		} else {
			Time.timeScale = 1;
			pauseImage.sprite = pauseSprites [0];

		}
	}
	public void Home(){
		PlayerPrefs.SetString ("gift", "0");
		Debug.Log ("Home" + PlayerPrefs.GetString ("gift"));
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}
