using UnityEngine;

namespace Game.Education
{
    /// <summary>
    /// Oznacza obiekt jako miejsce, do którego można wrzucić przedmiot.
    /// Np. Kosz na śmieci, Pudełko z poprawną cyfrą.
    /// </summary>
    public class DropSlot : MonoBehaviour
    {
        [Header("Konfiguracja Slotu")]
        [Tooltip("Unikalny identyfikator slotu, np. 'Trash', 'Circle', 'Number5'")]
        public string slotId;

        // Opcjonalnie: Efekt wizualny po najechaniu, dźwięk sukcesu itp.
        
        public void OnItemDropped()
        {
            Debug.Log($"Przedmiot wrzucony do slotu: {slotId}");
            // Tutaj w przyszłości dodamy logikę punktacji (GameManager)
        }
    }
}
