using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Prosty system poruszania "Tap to Move" dla gry 2D.
    /// Postać idzie w miejsce kliknięcia.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Ustawienia")]
        [Tooltip("Prędkość poruszania się Pandy")]
        [SerializeField] private float moveSpeed = 5f;
        
        [Tooltip("Minimalna odległość od celu, przy której postać się zatrzymuje")]
        [SerializeField] private float stopDistance = 0.1f;

        private Vector2 targetPosition;
        private bool isMoving = false;
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            // Na początku celem jest aktualna pozycja (stoisz w miejscu)
            targetPosition = transform.position;
        }

        private void Update()
        {
            HandleInput();
            MoveCharacter();
            UpdateAnimation();
        }

        private void HandleInput()
        {
            // Obsługa kliknięcia myszką lub dotknięcia ekranu
            if (Input.GetMouseButtonDown(0))
            {
                SetTargetPosition(Input.mousePosition);
            }
        }

        private void SetTargetPosition(Vector3 screenInputPosition)
        {
            // Konwersja pozycji ekranu (piksele) na świat gry
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenInputPosition);
            
            // Ignorujemy oś Z w 2D
            targetPosition = new Vector2(worldPos.x, worldPos.y);
            isMoving = true;
        }

        private void MoveCharacter()
        {
            if (!isMoving) return;

            // Sprawdź dystans do celu
            float distance = Vector2.Distance(transform.position, targetPosition);

            if (distance > stopDistance)
            {
                // Przesuń się w stronę celu
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Obracanie sprite'a (Flip X) w zależności od kierunku
                if (targetPosition.x < transform.position.x)
                {
                    spriteRenderer.flipX = false; // Zmiana: Jeśli idzie w lewo
                }
                else if (targetPosition.x > transform.position.x)
                {
                    spriteRenderer.flipX = true; // Zmiana: Jeśli idzie w prawo
                }
            }
            else
            {
                // Dotarliśmy do celu
                isMoving = false;
            }
        }

        public void PlayEmotion(string emotionName)
        {
            if (animator != null)
            {
                animator.SetTrigger(emotionName);
            }
        }

        private void UpdateAnimation()
        {
            if (animator != null)
            {
                // Ustaw flagę "IsWalking" w zależności czy się ruszamy
                animator.SetBool("IsWalking", isMoving);
            }
        }
    }
}
