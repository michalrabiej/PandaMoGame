using UnityEngine;

namespace Game.Education
{
    /// <summary>
    /// Skrypt pozwalający przeciągać obiekt palcem/myszką.
    /// Sprawdza, czy obiekt trafił do pasującego DropSlotu.
    /// </summary>
    [RequireComponent(typeof(Collider2D))] // Wymaga Collidera do wykrywania dotyku
    public class DragAndDropItem : MonoBehaviour
    {
        [Header("Konfiguracja Przedmiotu")]
        [Tooltip("Musi pasować do slotId w DropSlot (np. 'Trash' pasuje do 'Trash')")]
        [SerializeField] private string targetSlotId;
        
        [Tooltip("Czy obiekt ma wrócić na miejsce, jeśli upuścimy go źle?")]
        [SerializeField] private bool returnOnMiss = true;

        private Vector3 startPosition;
        private bool isDragging = false;
        private Camera mainCamera;
        private int originalSortingOrder;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            mainCamera = Camera.main;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        private void OnMouseDown()
        {
            // Rozpoczęcie przeciągania
            isDragging = true;
            
            // Przesuń obiekt "na wierzch" (żeby nie chował się za tłem)
            if (spriteRenderer != null)
            {
                originalSortingOrder = spriteRenderer.sortingOrder;
                spriteRenderer.sortingOrder = 100; // Wysoka wartość warstwy
            }
        }

        private void OnMouseDrag()
        {
            if (isDragging)
            {
                // Aktualizuj pozycję obiektu za myszką/palcem
                Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePos.x, mousePos.y, 0f); // Oś Z zawsze 0
            }
        }

        private void OnMouseUp()
        {
            isDragging = false;

            // Przywróć warstwę
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = originalSortingOrder;
            }

            CheckForSlot();
        }

        private void CheckForSlot()
        {
            // Sprawdź czy pod obiektem jest jakiś Collider (DropSlot)
            // Raycast w punkcie upuszczenia
            Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);

            foreach (var hit in hits)
            {
                // Ignorujemy samego siebie
                if (hit.gameObject == gameObject) continue;

                DropSlot slot = hit.GetComponent<DropSlot>();
                if (slot != null)
                {
                    // Znaleziono slot, sprawdzamy czy ID pasuje
                    if (slot.slotId == targetSlotId)
                    {
                        SuccessDrop(slot);
                        return;
                    }
                }
            }

            // Jeśli pętla się skończyła i nie trafiliśmy -> powrót
            if (returnOnMiss)
            {
                transform.position = startPosition;
                Debug.Log("Pudło! Wracam na miejsce.");
            }
        }

        private void SuccessDrop(DropSlot slot)
        {
            Debug.Log("Brawo! Poprawne dopasowanie.");
            transform.position = slot.transform.position; // "Przyklej" do slotu
            
            // Powiadom slot
            slot.OnItemDropped();

            // Opcjonalnie: Wyłącz możliwość ponownego przeciągania
            this.enabled = false; 
            
            // Opcjonalnie: Zniszcz obiekt efektownie
            // Destroy(gameObject, 0.5f);
        }
    }
}
