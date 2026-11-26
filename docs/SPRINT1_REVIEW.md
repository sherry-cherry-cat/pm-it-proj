# Sprint 1 — przegląd realizacji i retrospektywa

**Okres planowany:** 1–15 listopada 2026 r.  
**Data opracowania przeglądu:** 26 listopada 2026 r.  
**Plan źródłowy:** [`SPRINT1_PLAN.md`](../SPRINT1_PLAN.md) (nie zmieniam jego wcześniejszych założeń).

## 1. Cel i punkt wyjścia

Chciałam przygotować działającą podstawę projektu PasswordVault: uruchamiany program WPF, uporządkowane repozytorium i pierwsze elementy obsługi sejfu. Jednocześnie zaplanowałam Project Charter, ryzyka, backlog i początek automatycznej kontroli kodu. Zadania z planu miały różny charakter: dokumentacyjne (DOC-01, DOC-02), aplikacyjne (DEV-01, SEC-01, SEC-02) i związane z jakością (CI-01).

Nie zakładałam tworzenia trzech niezależnych prototypów. Kolejne sprinty rozwijają to samo rozwiązanie. Po pierwszym sprincie miałam uruchamialny szkielet, na którym mogłam sprawdzać następne zmiany.

## 2. Faktyczny przyrost na koniec Sprintu 1

- Utworzyłam rozwiązanie w Visual Studio i przygotowałam początkowy interfejs WPF. Program można było skompilować i uruchomić.
- Dodałam pierwsze materiały projektowe oraz pracowałam z repozytorium GitHub i historią commitów.
- Uruchomiłam proste okno z elementem testowym. Był to punkt wyjścia do podłączenia logiki sejfu, a nie gotowy menedżer haseł.
- Rozpoczęłam przygotowanie modułu kryptograficznego i organizacji testów, lecz nie miałam na 15 listopada potwierdzenia spełnienia wszystkich kryteriów.

## 3. Porównanie planu z realizacją

| ID | Założenie planu | Rezultat do 15.11 | Dalszy los |
|---|---|---|---|
| DOC-01 | Karta projektu i lista ryzyk | Opracowałam pierwsze materiały | Później uzupełniłam ryzyka o problem NuGet i kontrolę jakości |
| DOC-02 | Backlog i historie użytkowniczki | Rozpoczęłam prowadzenie backlogu | Rozwinęłam kryteria akceptacji i zależności w kolejnej iteracji |
| DEV-01 | Uruchamialny szkielet WPF | Zrealizowałam podstawowy widok i kompilację | Kontynuowałam pracę nad tym samym projektem |
| SEC-01 | Tworzenie sejfu | Niepotwierdzone jako ukończone | Dokończone po terminie, w trakcie Sprintu 2 |
| SEC-02 | Odblokowanie sejfu | Niepotwierdzone jako ukończone | Dokończone po terminie, w trakcie Sprintu 2 |
| CI-01 | GitHub Actions | Brak potwierdzonego zielonego przebiegu | Poprawione i uruchomione podczas Sprintu 2 |

Późniejszych wyników kryptografii i CI nie dopisuję do wyniku pierwszego sprintu. W `BACKLOG.csv` pierwotny sprint tych zadań nadal ma wartość `S1`, a status opisuje ukończenie po terminie.

## 4. Problemy i reakcja

Więcej pracy niż planowałam wymagały połączenie interfejsu z szyfrowaniem oraz przygotowanie automatycznej weryfikacji. Część czasu poświęciłam na organizację rozwiązania, zależności projektów i poprawienie podstawowych mechanizmów. Zamiast rozszerzać zakres o funkcje chmurowe, skupiłam się na działającej wersji lokalnej.

Nie zamykałam zadań wyłącznie na podstawie tego, że kod już istniał. Dla sejfu istotne było późniejsze sprawdzenie odblokowania poprawnym hasłem, odrzucania błędnego hasła i zachowania zapisanych danych.

## 5. Przeniesione elementy i retrospektywa

Do dalszej pracy przeszły SEC-01, SEC-02 i CI-01 oraz szczegółowe testy kryptografii. W Sprincie 2 rozbudowałam przyrost z pierwszego sprintu: od uruchamialnego okna do aplikacji zapisującej i odczytującej zaszyfrowane wpisy.

Z pierwszego sprintu wyniosłam konkretną korektę sposobu pracy: ograniczyłam liczbę rozpoczętych jednocześnie zadań i uzależniłam ich zamykanie od testów, nie od samej implementacji. Osobno zapisywałam opóźnienia i zmiany zakresu, aby w dokumentacji nie poprawiać po fakcie pierwotnego planu.

## 6. Dowody

Podstawą są pierwsze commity w repozytorium, zapisany plan Sprintu 1 i uruchamialna początkowa wersja WPF. Nie wskazuję niezweryfikowanych numerów commitów ani historycznych wyników testów. Wyniki 25/25 oraz 60% pokrycia pochodzą z późniejszej iteracji i opisuję je w przeglądzie Sprintu 2.
