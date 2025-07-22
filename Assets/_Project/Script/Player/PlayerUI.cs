using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Setting UI Player")]
    [SerializeField] private Image imageHp;
    [SerializeField] private Image crossX;
    [SerializeField] private Image crown;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textCoin;

    [Header("Setting Menu Player")]
    [SerializeField] private GameObject pannelMenuPlayer;
    [SerializeField] private GameObject pannelOptionPlayer;
    [SerializeField] private GameObject pannelUIPlayer;
    [SerializeField] private GameObject pannelWin;
    [SerializeField] private GameObject pannelLose;

    [SerializeField] private string nameMenu = "Menu";
    [SerializeField] private string nameNextLevel = "Menu";


    private bool isMenuPlayerOpen;
    private bool isPannelOptionPl;

    private void Start()
    {
        UpdatePannel();
        crossX.gameObject.SetActive(false);
    }

    public void UpdateLife(int hp,int maxHp) => imageHp.fillAmount = (float)hp/maxHp;

    public void UpdateTime(int minuts,float seconds) => textTime.SetText(string.Format("{0:00}:{1:00}", minuts, (int)seconds));

    public void UpdateCoin(int coinTake, int coinToT) => textCoin.SetText(string.Format("{0}/{1}", coinTake, coinToT));

    public void IsOnAim(bool onAim) => crossX.gameObject.SetActive(onAim);

    public void OpenMenu()
    {
        isMenuPlayerOpen = !isMenuPlayerOpen;
        isPannelOptionPl = false;
        UpdatePannel();
        if(isMenuPlayerOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else ResumeGame();
    } 

    public void ResumeGame()
    {
        isMenuPlayerOpen = false;
        isPannelOptionPl = false;
        UpdatePannel();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoOption()
    {
        isPannelOptionPl = !isPannelOptionPl;
        UpdatePannel();
    }

    private void UpdatePannel()
    {
        pannelMenuPlayer.SetActive(isMenuPlayerOpen);
        pannelOptionPlayer.SetActive(isPannelOptionPl);
    }

    public void OnFinishGame()
    {
        textCoin.gameObject.SetActive(false);
        crown.gameObject.SetActive(true);
    }

    public void OnWin()
    {
        pannelMenuPlayer.SetActive(false);
        pannelOptionPlayer.SetActive(false);
        pannelUIPlayer.SetActive(false);

        Time.timeScale = 0;

        pannelLose.SetActive(false);
        pannelWin.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnLose()
    {
        pannelMenuPlayer.SetActive(false);
        pannelOptionPlayer.SetActive(false);
        pannelUIPlayer.SetActive(false);

        Time.timeScale = 0;

        pannelLose.SetActive(true);
        pannelWin.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoMenu() => SceneManager.LoadScene(nameMenu);

    public void GoNextLevel() => SceneManager.LoadScene(nameNextLevel);

    public void RePlay() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
