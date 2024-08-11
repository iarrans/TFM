using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedPlayerFloor : MonoBehaviour
{
    public List<GameObject> playersStanding;
    
    public GameObject floor;
    public Material emptyFloorMaterial;
    public Material fullFloorMaterial;
    public int defaultyer;
    public int restrictedFloorLayer;

    //Number of players that can stay on floor at the same time
    public int numberOfPlayers = 1;

    public AudioClip fullFloorSFX;

    private void Start()
    {
        defaultyer = LayerMask.NameToLayer("Default");
        restrictedFloorLayer = LayerMask.NameToLayer("RestrictedFloorPlayer");
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("A");
            if (playersStanding.Count < numberOfPlayers)
            {
                if (collision.transform.parent != null)
                {
                    playersStanding.Add(collision.gameObject);
                    collision.transform.parent.gameObject.layer = restrictedFloorLayer;
                    collision.transform.gameObject.layer = restrictedFloorLayer;
                }
                else
                {
                    playersStanding.Add(collision.transform.gameObject);
                    collision.transform.gameObject.layer = restrictedFloorLayer;
                }
           
            }
            if (playersStanding.Count >= numberOfPlayers)
            {
                transform.parent.GetComponent<Renderer>().material = fullFloorMaterial;
                AudioManager.instance.PlaySFXClip(fullFloorSFX);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent != null)
            {
                collision.transform.parent.gameObject.layer = defaultyer;
                collision.transform.gameObject.layer = defaultyer;
                playersStanding.Remove(collision.gameObject);
            }
            else
            {
                collision.transform.gameObject.layer = defaultyer;
                playersStanding.Remove(collision.transform.gameObject);
            }
            if (playersStanding.Count < numberOfPlayers)
            {
                transform.parent.GetComponent<Renderer>().material = emptyFloorMaterial;
                AudioManager.instance.PlaySFXClip(fullFloorSFX);
            }         
        }
    }

}
