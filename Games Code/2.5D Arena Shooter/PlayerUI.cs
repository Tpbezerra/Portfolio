using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public GameObject pauseUI;

	void Start () {
        if (pauseUI != null)
            pauseUI.SetActive(false);
	}
	
	void Update () {
        if (pauseUI != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                pauseUI.SetActive(!pauseUI.activeInHierarchy);
        }
	}
}
