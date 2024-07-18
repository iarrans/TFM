using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static UIManager Instance;

    public GameObject victoryScreen;

    public TextMeshProUGUI vicScreenEscapedText;
    public TextMeshProUGUI vicScreenTimeResultText;

    public List<GameObject> vicScreenButtons;
    

    private void Awake()
    {
        Instance = this;
        DOTween.Init();
    }

    public void RestartScene()
    {
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

        vicScreenEscapedText.gameObject.SetActive(true);
        vicScreenEscapedText.transform.DOPunchScale(new Vector3(1.15f,1.15f,1.15f),1,5,2);
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
}
