using System;
using UnityEngine;
using Game.Education;

namespace Game.Core
{
    /// <summary>
    /// Liczy postęp przygody (np. ile śmieci trafiło do kosza)
    /// i ogłasza jej ukończenie. Pomyłki nie są karane i nie wpływają na wynik.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Konfiguracja Przygody")]
        [Tooltip("Ile przedmiotów trzeba umieścić. 0 = policz wszystkie DragAndDropItem w scenie na starcie")]
        [SerializeField] private int itemsToComplete = 0;

        /// <summary>Zgłasza (umieszczone, wszystkie) po każdym poprawnym umieszczeniu.</summary>
        public event Action<int, int> ProgressChanged;

        /// <summary>Zgłaszane raz, po umieszczeniu wszystkich przedmiotów.</summary>
        public event Action AdventureCompleted;

        public int PlacedCount { get; private set; }
        public int TotalCount => itemsToComplete;
        public bool IsCompleted { get; private set; }

        private void OnEnable()
        {
            DragAndDropItem.Placed += HandleItemPlaced;
        }

        private void OnDisable()
        {
            DragAndDropItem.Placed -= HandleItemPlaced;
        }

        private void Start()
        {
            if (itemsToComplete <= 0)
            {
                itemsToComplete = FindObjectsByType<DragAndDropItem>(FindObjectsSortMode.None).Length;
            }
        }

        private void HandleItemPlaced(DragAndDropItem item)
        {
            if (IsCompleted) return;

            PlacedCount++;
            ProgressChanged?.Invoke(PlacedCount, itemsToComplete);

            if (PlacedCount >= itemsToComplete)
            {
                IsCompleted = true;
                AdventureCompleted?.Invoke();
            }
        }
    }
}
