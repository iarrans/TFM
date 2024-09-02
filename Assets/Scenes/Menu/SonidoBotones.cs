using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;     // Asigna el AudioSource desde el inspector
    public AudioClip hoverSound;        // Sonido al pasar el ratón por encima
    public AudioClip clickSound;        // Sonido al hacer clic

    // Este método se llama cuando el ratón entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.clip = hoverSound;
            audioSource.Play();
        }
    }

    // Este método se llama cuando se hace clic en el botón
    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }
}

