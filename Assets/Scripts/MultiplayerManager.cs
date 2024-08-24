using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public List<Transform> spawnPositions;

    private int positionIndex;

    public List<GameObject> players;

    public AudioClip PlayerJoinedSound;

    public List<GameObject> characterModels;

    private void Awake()
    {
        Instance = this;
        positionIndex = 0;
    }

    public void PlayerAdded(GameObject playerGO)
    {
        PlayerControls playerControls = playerGO.GetComponent<PlayerControls>();

        //Añadimos a la lista de jugadores por si hiciera falta hacer algun efecto sobre ellos
        players.Add(playerGO);

        //Se asigna position de inicio
        playerGO.transform.position = spawnPositions[positionIndex].position;

        //Se asigna index de jugador
        playerControls.playerNumber = positionIndex;

        //Se cambia el texto para indicar num del jugador
        int planerNumber = positionIndex + 1;
        playerGO.name = "Player " + planerNumber;
        playerGO.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Player " + planerNumber;

        //Se configuran las layers correctamente para los colliders exclusivos de jugadores
        int PlayerLayer = LayerMask.NameToLayer("Default");

        UIManager.Instance.AddPlayerToScreen(positionIndex);

        playerGO.layer = PlayerLayer;
        playerGO.transform.GetChild(0).gameObject.layer = PlayerLayer;

        spawnPositions[positionIndex].gameObject.SetActive(false);

        positionIndex++;

        AudioManager.instance.PlaySFXClip(PlayerJoinedSound);

        /*switch (positionIndex)
        {
            case 0:
                PlayerLayer = LayerMask.NameToLayer("PlayerLayer1");
                break;
            case 1:
                PlayerLayer = LayerMask.NameToLayer("PlayerLayer2");
                break;
            case 2:
                PlayerLayer = LayerMask.NameToLayer("PlayerLayer3");
                break;
            case 3:
                PlayerLayer = LayerMask.NameToLayer("PlayerLayer4");
                break;;
            default:
                print("Incorrect player index.");
                break;
        }
        */
    }
}
