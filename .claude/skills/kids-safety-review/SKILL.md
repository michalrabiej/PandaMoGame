---
name: kids-safety-review
description: Przegląd zmian w grze Panda Mo pod kątem bezpieczeństwa i przyjazności dla małych dzieci (3–6 lat). Użyj przed commitem, po dodaniu przygody, postaci, dźwięku, grafiki, pakietu lub funkcji sieciowej, albo gdy użytkownik prosi o sprawdzenie zgodności z SAFETY_RULES.md.
---

# Przegląd bezpieczeństwa dla dzieci

Sprawdź bieżące zmiany (`git diff`, `git status` i nowe pliki) względem poniższej listy. Przed przeglądem przeczytaj `SAFETY_RULES.md`, `GAME_DESIGN.md` i `PANDA_MO_LORE.md`. Nic nie poprawiaj bez zgody użytkownika, tylko zgłoś wyniki.

## 1. Zasady twarde (`SAFETY_RULES.md`)
Wyszukaj w zmianach (Grep) i zgłoś każde trafienie:
- Zakupy, loot boxy, waluta za prawdziwe pieniądze: `IAP`, `Purchasing`, `purchase`, `store`.
- Reklamy i śledzenie: `Ads`, `AdMob`, `Analytics`, `Firebase`, `Unity Gaming Services`, `Advertisement`, identyfikatory urządzeń (`SystemInfo.deviceUniqueIdentifier`, `advertisingIdentifier`).
- Linki zewnętrzne: `Application.OpenURL`, `http://`, `https://`, `mailto:`, `WebView`.
- Czat, konta, logowanie, dane osobowe: `login`, `account`, `email`, `PlayerPrefs` z danymi osobowymi, formularze tekstowe (`InputField`).
- Nowe pakiety w `Packages/manifest.json` (wymagają zgody użytkownika) oraz zmiany w `ProjectSettings/`.
- Funkcje online: jeśli się pojawiają, czy są kontrole rodzicielskie.

## 2. Treści
- Brak przemocy, walki, broni, krwi, strachu, ciemnych lub groźnych scen, nagłych głośnych dźwięków.
- Postaci wyglądają przyjaźnie, bez gróźb i złoczyńców. Kontrola zgodności z `ART_STYLE.md`, jeśli jest wypełniony.
- Płacz Mo (`PandaMo_Crying`) tylko gdy przyjaciel ma kłopot i zawsze kończy się pomocą i uśmiechem.
- Morał przygody jest pozytywny, konkretny i zrozumiały dla dziecka.

## 3. Projekt bez frustracji
- Brak kary za błąd: brak punktów ujemnych, dźwięku „błąd”, czerwonych krzyży, limitów czasu, odliczania, „game over”.
- Reakcja na pomyłkę jest spokojna (np. `PandaMo_Thinking`, powrót przedmiotu na miejsce).
- Instrukcje podane obrazkiem lub głosem, nie samym tekstem.
- Cele dotyku duże (ok. 2 cm), brak wymaganych gestów wielodotykowych i precyzyjnych.
- Brak wciągających mechanik: licznik serii, presja codziennego powrotu, natrętne powiadomienia.

## 4. Format raportu
Odpowiedz krótko, po polsku:
- **Wynik:** OK / Uwagi / Blokada.
- **Blokady** (naruszenie sekcji 1): plik:linia i dlaczego.
- **Uwagi** (sekcje 2–3): plik:linia i propozycja zmiany.
- **Nie sprawdzono:** to, czego nie da się ocenić bez Unity (wygląd, dźwięk, zachowanie w grze) i co użytkownik powinien obejrzeć lokalnie.
