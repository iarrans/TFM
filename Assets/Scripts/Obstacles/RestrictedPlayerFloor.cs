using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedPlayerFloor : MonoBehaviour
{
    public int playersStanding;
    
    public GameObject floor;
    public List<GameObject> floorBarriers;
    public List<Material> playersLeftMaterials;

    //public int defaultyer;
    //public int restrictedFloorLayer;

    //Number of players that can stay on floor at the same time
    public int playerLimit = 1;

    public AudioClip fullFloorSFX;

    private void Start()
    {
        //defaultyer = LayerMask.NameToLayer("Default");
        //restrictedFloorLayer = LayerMask.NameToLayer("RestrictedFloorPlayer");
        playersStanding = 0;
        transform.GetComponent<Renderer>().material = playersLeftMaterials[playerLimit - playersStanding];
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersStanding++;
            if (playersStanding >= playerLimit)
            {
                foreach (GameObject barrier in floorBarriers)
                {
                    barrier.SetActive(true);
                }
                AudioManager.instance.PlaySFXClip(fullFloorSFX);
                transform.GetComponent<Renderer>().material = playersLeftMaterials[0];
            }
            else
            {
                transform.GetComponent<Renderer>().material = playersLeftMaterials[playerLimit - playersStanding];
                AudioManager.instance.PlaySFXClip(fullFloorSFX);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersStanding--;
            if (playersStanding < playerLimit)
            {
                foreach (GameObject barrier in floorBarriers)
                {
                    barrier.SetActive(false);
                }
                AudioManager.instance.PlaySFXClip(fullFloorSFX);
                transform.GetComponent<Renderer>().material = playersLeftMaterials[playerLimit-playersStanding];
            }

            /*if (collision.transform.parent != null)
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
            if (playersStanding.Count < playerLimit)
            {
                transform.parent.GetComponent<Renderer>().material = emptyFloorMaterial;
                AudioManager.instance.PlaySFXClip(fullFloorSFX);
            }*/
        }
    }

}
