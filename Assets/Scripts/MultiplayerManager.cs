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

    public List<Mesh> characterMeshes;
    public List<Material> p1Materials;
    public List<Material> p2Materials;
    public List<Material> p3Materials;
    public List<Material> p4Materials;

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
        playerGO.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Player " + planerNumber;

        //Se configuran las layers correctamente para los colliders exclusivos de jugadores
        int PlayerLayer = LayerMask.NameToLayer("Default");

        UIManager.Instance.AddPlayerToScreen(positionIndex);

        spawnPositions[positionIndex].gameObject.SetActive(false);

        AudioManager.instance.PlaySFXClip(PlayerJoinedSound);

        playerGO.layer = PlayerLayer;
        GameObject characterViewer = playerGO.transform.GetChild(1).gameObject;
        characterViewer.GetComponent<MeshFilter>().mesh = characterMeshes[positionIndex];      

        switch (positionIndex)
        {
            case 0:
                characterViewer.GetComponent<MeshRenderer>().SetMaterials(p1Materials);
                break;
            case 1:
                characterViewer.GetComponent<MeshRenderer>().SetMaterials(p2Materials);
                break;
            case 2:
                characterViewer.GetComponent<MeshRenderer>().SetMaterials(p3Materials);
                break;
            case 3:
                characterViewer.GetComponent<MeshRenderer>().SetMaterials(p4Materials);
                BoxCollider box =characterViewer.GetComponent<BoxCollider>();
                //TEST This size
                box.center = new Vector3(box.center.x, box.center.y, -0.006f);
                box.size = new Vector3(box.size.x, box.size.y, 0.013f);
                break;;
            default:
                print("Incorrect player index.");
                break;
        }

        positionIndex++;

    }
}
