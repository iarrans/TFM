using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static UIManager Instance;

    [Header("VICTORY SCREEN SETTINGS")]
    //Victory Screen Attributes
    public GameObject victoryScreen;

    public TextMeshProUGUI vicScreenEscapedText;
    public TextMeshProUGUI vicScreenTimeResultText;

    public List<GameObject> vicScreenButtons;

    [Header("JOIN SCREEN SETTINGS")]
    //Player join screen attributes
    public GameObject playerJoinScreen;
    public List<GameObject> playerJoinImages;
    public GameObject readyObject;
    public AudioClip levelStartAudio;
    public List<Sprite> readySprites;


    [Header("PAUSE SCREEN SETTINGS")]
    //Pause Screen Attributes
    public GameObject pauseScreen;


    private void Awake()
    {
        Instance = this;
        DOTween.Init();
        playerJoinScreen.SetActive(true);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

    public void ShowVictoryScreen()
    {

        Debug.Log("Victory!");

        //Para que deje de calcular la caida de los personajes
        foreach (GameObject player in MultiplayerManager.Instance.players) {
            player.SetActive(false);
        }

        //A partir de aquí, iría cualquier tipo de animacion
        StartCoroutine(VictoryScreenAnimation());
    }

    public IEnumerator VictoryScreenAnimation()
    {
        victoryScreen.SetActive(true);

        vicScreenEscapedText.transform.parent.gameObject.SetActive(true);
        vicScreenEscapedText.gameObject.SetActive(true);
        vicScreenEscapedText.transform.parent.DOPunchScale(new Vector3(1.15f,1.15f,1.15f),1,5,2);
        yield return new WaitForSeconds(2);

        //Check del tiempo
        float finalTime = GameManager.Instance.timeManager.tiempoTranscurrido;
        //Parsing del tiempo
        string leveltime;

        int minutos = Mathf.Max(Mathf.FloorToInt(finalTime / 60), 0);     //Extraemos los minutos
        int segundos = Mathf.Max(Mathf.FloorToInt(finalTime % 60), 0);    //Extraemos los segundos
        if (segundos >= 10) leveltime = minutos + ":" + segundos;                    //Si los segundos son mayores o iguales que 10, concatenamos y construimos el texto
        else leveltime = minutos + ":" + "0" + segundos;

        vicScreenTimeResultText.text = "It took you " + leveltime + "!";

        //Hacemos el tiempo visible, finalmente
        vicScreenTimeResultText.transform.parent.gameObject.SetActive(true);
        vicScreenTimeResultText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        foreach (GameObject button in vicScreenButtons)
        {
            button.SetActive(true);
            yield return new WaitForSeconds(1);
        }

    }

    public void LoadNextLevel()
    {
      GameManager.Instance.transform.GetChild(0).GetComponent<LevelManager>().LoadNextScene();     
    }

    //PAUSE SCREEN METHODS
    public void PauseGameplay()
    {
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
    }

    public void RestoreGameplay()
    {
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void AddPlayerToScreen(int number)
    {
        Image playerImage = playerJoinImages[number].GetComponent<Image>();
        TextMeshProUGUI playerText = playerJoinImages[number].transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        playerImage.sprite = readySprites[number];
        int playernumber = number + 1;
        playerText.text = "Player " + playernumber  + " ready!";

        if (number +1 >= GameManager.Instance.requiredPlayers)
        {
            readyObject.SetActive(true);
            AudioManager.instance.PlaySFXClip(levelStartAudio);
            GameManager.Instance.ShowLevel();
        }
    }
}
