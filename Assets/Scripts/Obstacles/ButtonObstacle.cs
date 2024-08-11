using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObstacle : MonoBehaviour
{
    public GameObject obstacle;
    public List<GameObject> objectStanding;
    public bool isSwitchButton;
    [SerializeField] AudioClip buttonPressedSFX;
    [SerializeField] AudioClip buttonReleasedSFX;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody wightedObject = other.gameObject.GetComponent<Rigidbody>();
        if (wightedObject != null || other.CompareTag("Player"))
        {
            obstacle.SetActive(false);
            objectStanding.Add(other.gameObject);
            AudioManager.instance.PlaySFXClip(buttonPressedSFX);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody wightedObject = other.gameObject.GetComponent<Rigidbody>();
        if ((wightedObject != null || other.CompareTag("Player")) && isSwitchButton)
        {
            //Doble check para evitar que, si hay dos en el área, con irse uno se despulse
            if (objectStanding.Count == 1) obstacle.SetActive(true);
            objectStanding.Remove(other.gameObject);
            AudioManager.instance.PlaySFXClip(buttonReleasedSFX);

        }
    }
}
