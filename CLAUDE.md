@AGENTS.md

# Panda Mo: zasady dla Claude Code

Gra 2D w Unity (URP) dla małych dzieci (ok. 3–6 lat). Obowiązujące dokumenty: `GAME_DESIGN.md`, `SAFETY_RULES.md`, `PANDA_MO_LORE.md`, `ART_STYLE.md`, `BACKLOG.md`. Reguły z `AGENTS.md` mają pierwszeństwo.

## Środowisko
- Sesje w chmurze nie mają edytora Unity, więc nie da się tam sprawdzić kompilacji ani konsoli. Zaznacz to w podsumowaniu i poproś o sprawdzenie lokalnie.
- Komunikacja i komentarze w kodzie po polsku, nazwy w kodzie po angielsku.

## Struktura projektu
- Wszystko własne w `Assets/_Game/`: `Scripts/<Moduł>/`, `Sprites/`, `Scenes/`, `Prefabs/`, `Audio/`, `Animations/`, `ScriptableObjects/`.
- Przestrzenie nazw `Game.<Moduł>` (np. `Game.Player`, `Game.Education`). Jedna klasa na plik, nazwa pliku = nazwa klasy.
- Nigdy nie usuwaj ani nie przenoś plików bez `.meta` w parze. Przenoś tylko przez operacje zachowujące GUID.
- Nie edytuj ręcznie plików `.unity`, `.prefab`, `.asset`, jeśli można tego uniknąć. Zmiany w scenach rób przez MCP for Unity lub zleć użytkownikowi.

## Kod C#
- Pola edytowalne w Inspectorze: `[SerializeField] private`, nie `public`. Dodawaj `[Header]` i `[Tooltip]`.
- `Awake` do pobrania własnych komponentów, `Start` do odwołań do innych obiektów. Referencje przypisuj w Inspectorze lub cache'uj raz, bez `GetComponent`/`Find*`/`Camera.main` w `Update`.
- Brak alokacji w `Update`: bez `new`, LINQ, konkatenacji stringów i `foreach` po kolekcjach tworzonych w pętli. Do ruchu stosuj `Time.deltaTime`, do fizyki `FixedUpdate`.
- Komponenty mają jedną odpowiedzialność. Komunikację rozdzielaj zdarzeniami (`event Action`, `UnityEvent`) zamiast twardych referencji i singletonów. Singleton tylko dla `GameManager`.
- Dane (przygody, nagrody, dialogi) trzymaj w `ScriptableObject`, nie wpisuj na sztywno w kodzie.
- Usuwaj subskrypcje zdarzeń w `OnDisable`/`OnDestroy`. `Debug.Log` tylko tymczasowo, bez logów w produkcji.
- Stringi w tagach, warstwach i animatorze zastępuj stałymi lub `Animator.StringToHash`.
- Wstaw `[RequireComponent]` tam, gdzie skrypt zakłada komponent.

## Wejście
- Docelowo nowy Input System (pakiet jest w projekcie). Nowy kod nie używa `UnityEngine.Input`. Istniejące skrypty (`PlayerMovement`, `DragAndDropItem`) mają być zmigrowane (patrz `BACKLOG.md`).
- Obsługuj dotyk i mysz tak samo (jeden wskaźnik), bez gestów wielodotykowych.

## Sceny, prefaby, 2D
- Powtarzalne obiekty (śmieci, postacie, nagrody) jako prefaby. Sceny zostają małe i czytelne, z porządkiem w hierarchii (puste obiekty-grupy).
- Sprite'y: Pixels Per Unit spójne w całym projekcie, kompresja i rozmiar tekstur dopasowane do urządzeń mobilnych (max 2048), atlasy (Sprite Atlas) dla grup sprite'ów.
- Warstwy (`Sorting Layers`) zamiast ręcznych wartości `sortingOrder`. Fizyka 2D tylko tam, gdzie potrzebna; bez ciężkich colliderów na tle.
- Jedna kamera ortograficzna. UI z `Canvas Scaler` (Scale With Screen Size), dopasowane do `Safe Area`.

## Mobile i wydajność
- Cel: płynne 60 FPS na starszych telefonach i tabletach. Mało overdraw, brak dużych efektów post-processingu.
- Pooling dla obiektów często tworzonych i niszczonych. Nie niszcz i nie twórz obiektów w pętli.
- Audio: kompresja (Vorbis), muzyka jako streaming, krótkie efekty jako Decompress On Load.
- Profiluj przed optymalizacją, nie zgaduj.

## Projekt dla dzieci
- Cele dotyku min. ok. 2 cm, duże i wyraźne. Instrukcje obrazkiem i głosem, bez wymogu czytania.
- Bez kary za błąd: brak punktów ujemnych, brak dźwięku „błąd”, brak limitów czasu. Reakcja na pomyłkę jest spokojna i zachęcająca.
- Brak reklam, zakupów, śledzenia, kont, czatu i linków zewnętrznych (patrz `SAFETY_RULES.md`). Nie dodawaj SDK analitycznych ani reklamowych.
- Każda nowa przygoda uczy konkretnego dobrego uczynku i trwa ok. 3–5 minut.

## Przepływ pracy
1. Przeanalizuj istniejące pliki i dokumenty przed zmianą.
2. Jedno zadanie na raz, potem stop.
3. Sprawdź kompilację i konsolę (lokalnie w Unity). Jeśli nie możesz, napisz to wprost.
4. Podaj listę zmienionych plików.
5. Commit i push tylko po wyraźnej zgodzie użytkownika.
