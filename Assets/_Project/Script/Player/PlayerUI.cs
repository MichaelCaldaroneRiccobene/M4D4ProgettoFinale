using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public enum PanelType {menu,option,UiPlayer,WinScreen,LoseScreen }

    [Header("Setting UI Player")]
    [SerializeField] private Image imageHp;
    [SerializeField] private Image crossX;
    [SerializeField] private Image crown;

    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textCoin;

    [Header("Setting Menu Player")]
    [SerializeField] private GameObject[] pannels;
    [SerializeField] private GameObject pannelUIPlayer;

    [SerializeField] private string nameMenu = "Menu";
    [SerializeField] private string nameNextLevel = "Menu";


    private bool isMenuPlayerOpen;

    private void Start()
    {
        SetPannelsForEndGame(PanelType.UiPlayer);
        crossX.gameObject.SetActive(false);
    }

    public void UpdateLife(int hp,int maxHp) => imageHp.fillAmount = (float)hp/maxHp;

    public void UpdateTime(int minuts,float seconds) => textTime.SetText(string.Format("{0:00}:{1:00}", minuts, (int)seconds));

    public void UpdateCoin(int coinTake, int coinToT) => textCoin.SetText(string.Format("{0}/{1}", coinTake, coinToT));

    public void IsOnAim(bool onAim) => crossX.gameObject.SetActive(onAim);

    private void SetPannelsForGame(PanelType panelType)
    {
        for (int i = 0; i < pannels.Length; i++)
        {
            pannels[i].gameObject.SetActive(i == (int)panelType);
        }
        pannelUIPlayer.SetActive(true);
    }

    private void SetPannelsForEndGame(PanelType panelType)
    {
        for (int i = 0; i < pannels.Length; i++) pannels[i].gameObject.SetActive(i == (int)panelType);
    }

    public void OpenMenu()
    {
        isMenuPlayerOpen = !isMenuPlayerOpen;
        SetPannelsForGame(PanelType.menu);

        if(isMenuPlayerOpen)
        {
            CursorOnMenu();
            Time.timeScale = 0;
        }
        else ResumeGame();
    } 

    public void ResumeGame()
    {
        isMenuPlayerOpen = false;

        SetPannelsForGame(PanelType.UiPlayer);

        Time.timeScale = 1;
        CursorNotOnMenu();
    }

    public void GoMainPannel() => SetPannelsForGame(PanelType.menu);

    public void GoOption() => SetPannelsForGame(PanelType.option);

    public void OnFinishGame()
    {
        textCoin.gameObject.SetActive(false);
        crown.gameObject.SetActive(true);
    }

    public void OnWin()
    {
        SetPannelsForEndGame(PanelType.WinScreen);

        Time.timeScale = 0;
        CursorOnMenu();
    }

    public void OnLose()
    {
        SetPannelsForEndGame(PanelType.LoseScreen);

        Time.timeScale = 0;
        CursorOnMenu();
    }

    private void CursorOnMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CursorNotOnMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoMenu() => SceneManager.LoadScene(nameMenu);

    public void GoNextLevel() => SceneManager.LoadScene(nameNextLevel);

    public void RePlay() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
