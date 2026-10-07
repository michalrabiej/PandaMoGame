using System.Collections;
using UnityEngine;
using Game.Core;
using Game.Education;

namespace Game.Player
{
    /// <summary>
    /// Zmienia minę Pandy Mo w reakcji na poczynania dziecka:
    /// Happy po dobrym uczynku, Thinking po pomyłce, Celebrating po ukończeniu przygody.
    /// Po chwili wraca do spokojnej miny.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PandaReactions : MonoBehaviour
    {
        [Header("Referencje")]
        [SerializeField] private GameManager gameManager;

        [Header("Miny Pandy Mo")]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite happySprite;
        [SerializeField] private Sprite thinkingSprite;
        [SerializeField] private Sprite celebratingSprite;

        [Header("Czas")]
        [Tooltip("Jak długo trwa krótka reakcja (Happy / Thinking)")]
        [SerializeField] private float reactionSeconds = 1.2f;

        [Tooltip("Jak długo trwa świętowanie po ukończeniu przygody")]
        [SerializeField] private float celebrationSeconds = 3f;

        private SpriteRenderer spriteRenderer;
        private Coroutine currentReaction;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            DragAndDropItem.Placed += HandlePlaced;
            DragAndDropItem.Missed += HandleMissed;
            if (gameManager != null) gameManager.AdventureCompleted += HandleCompleted;
        }

        private void OnDisable()
        {
            DragAndDropItem.Placed -= HandlePlaced;
            DragAndDropItem.Missed -= HandleMissed;
            if (gameManager != null) gameManager.AdventureCompleted -= HandleCompleted;
        }

        private void HandlePlaced(DragAndDropItem item)
        {
            // Świętowanie po ukończeniu nadpisze tę reakcję w tej samej klatce.
            Show(happySprite, reactionSeconds);
        }

        private void HandleMissed(DragAndDropItem item)
        {
            Show(thinkingSprite, reactionSeconds);
        }

        private void HandleCompleted()
        {
            Show(celebratingSprite, celebrationSeconds);
        }

        private void Show(Sprite sprite, float seconds)
        {
            if (sprite == null) return;

            if (currentReaction != null) StopCoroutine(currentReaction);
            currentReaction = StartCoroutine(ShowRoutine(sprite, seconds));
        }

        private IEnumerator ShowRoutine(Sprite sprite, float seconds)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(seconds);

            if (defaultSprite != null) spriteRenderer.sprite = defaultSprite;
            currentReaction = null;
        }
    }
}
