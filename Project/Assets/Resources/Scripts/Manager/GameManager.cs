using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager m_instance;

    void Awake()
	{
        //PlayerPrefs.DeleteAll();
		if(m_instance == null)
		{
			m_instance = this;
		}
        DontDestroyOnLoad(gameObject);
        LoadScene("Void");
    }

    public void LoadGameScene()
    {
        LoadScene("Game");
        LoadGold();
    }

    private int m_gold;

    void LoadGold()
    {
        m_gold = PlayerPrefs.GetInt("RoseTrip_Gold");
    }

    public int GetPlayerGold()
    {
        m_gold = PlayerPrefs.GetInt("RoseTrip_Gold");
        return m_gold;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}

