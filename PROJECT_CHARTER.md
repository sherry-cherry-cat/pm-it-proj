# Project Charter — PasswordVault

**Wersja:** 1.1 • **Data przygotowania:** 2026-11-09 • **Charakter:** plan bazowy, nie raport wykonania

## 1. Cel (SMART)
Do **15 grudnia 2026 r.** przygotować i zaprezentować demonstracyjną aplikację desktopową dla Windows 11 w C#/.NET/WPF, umożliwiającą utworzenie zaszyfrowanego sejfu, jego odblokowanie hasłem master, obsługę co najmniej 20 testowych wpisów, bezpieczny eksport i import pliku na drugim środowisku Windows, a także przejście zestawu scenariuszy akceptacyjnych AC-01–AC-12 i automatycznych testów w CI. Wartości pomiarowe to **cele do zweryfikowania**, a nie wyniki.

## 2. Uzasadnienie i problem
Zbudowanie edukacyjnego rozwiązania do lokalnego zarządzania hasłami z oddzieleniem hasła master od losowego klucza danych i możliwością przenoszenia zaszyfrowanych plików bez własnego backendu chmurowego. Projekt demonstruje praktyki zarządzania backlogiem, ryzykiem, zakresem i automatycznymi kontrolami jakości.

## 3. Zakres
**IN / MVP (MUST):** nowy sejf; password-based KDF (Argon2id), wrapped DEK i AES-256-GCM; zapis zaszyfrowanych pól kont w lokalnym SQLite; utworzenie/edycja/usunięcie/lista wpisów; błędy i walidacja; eksport/import do wskazanego pliku z kontrolą spójności i ochroną przed nadpisaniem; testy; GitHub Issues/Projects/PR i CI.

**SHOULD:** generator losowych haseł, maskowanie i schowek, blokada po bezczynności.

**COULD:** Windows Hello jako *potwierdzenie działania* podczas aktywnej sesji; uproszczenia wizualne WPF UI.

**OUT:** dwukierunkowa synchronizacja chmurowa/API, wspólne sejfy wielu użytkowników, mobilny klient, TOTP, automatyczne odzyskiwanie nieznanego hasła master, gwarancje produkcyjnego audytu bezpieczeństwa.

## 4. Użytkownik i interesariusze
- Użytkownik docelowy: jedna osoba przechowująca własne dane dostępu na Windows.
- Autorka projektu: planowanie, programowanie, testowanie, dokumentacja, ryzyka i CI.
- Prowadzący: ocena kompletności pracy i zgodności z regulaminem przedmiotu (dodatkowe wymagania poza wzorcem należy potwierdzić).

## 5. Harmonogram i kamienie milowe
| Okres | Cel | Dowód |
|---|---|---|
| Sprint 1: 01–15.11.2026 | artefakty planistyczne, repozytorium, szkielet WPF, prototyp KEK/DEK | zaakceptowane dokumenty; build + testy podstawowe |
| Sprint 2: 16–29.11.2026 | SQLite i zaszyfrowane wpisy, CRUD, UI, generator | demonstracja działań i restart aplikacji |
| Sprint 3: 30.11–13.12.2026 | eksport/import, testy negatywne, zamknięcie MVP | scenariusze akceptacji, raport testów, demo |
| 14–15.12.2026 | odbiór i poprawki | tag wydania, raport końcowy, przekazanie |

**Nota historyczna:** planowany początek Sprintu 1 jest wcześniejszy niż data sporządzenia tej wersji dokumentacji. Nie poświadcza to wykonania prac przed 09.11.2026.

## 6. Mierzalne kryteria sukcesu
1. Wypełnienie kryteriów AC-01–AC-12 na danych syntetycznych, brak nierozwiązanych defektów krytycznych w demonstracji.
2. Brak jawnego hasła, loginu lub nazwy strony w wybranych kontrolnych fragmentach SQLite i eksportu (test strukturalny, nie dowód pełnego audytu kryptograficznego).
3. Ciągłe testy: workflow dokumentacji i — po utworzeniu rozwiązania — niezależny workflow `dotnet restore/build/test` na Windows.
4. Funkcjonujące zadania Issues / sprinty / Pull Requests i prowadzony rejestr ryzyk.
5. Import kopii na drugim środowisku Windows i jej odblokowanie poprawnym hasłem master.

## 7. Ograniczenia i zależności
Praca indywidualna; termin nieprzekraczalny 15.12.2026; 3 sprinty; brak dedykowanego serwera; ryzyka integracji bibliotek kryptograficznych i dostępności Windows Hello. Zakres COULD realizowany dopiero po zamknięciu MUST. Każda zmiana planu odnotowana w `docs/DECISION_LOG.md`.

## 8. Założenia
Windows 11 x64 i Visual Studio 2026 / .NET 10, lokalny plik SQLite, dostatecznie silne hasło master, brak atakującego w pełni kontrolującego odblokowaną sesję Windows. Eksport zawiera zaszyfrowany DEK z parametrami KDF, dzięki czemu przeniesienie nie wymaga DPAPI konkretnego komputera.

## 9. Kontrola zmian i akceptacja
Każda funkcja: Issue → gałąź feature/docs → PR do własnego `main` → testy → merge → aktualizacja zadania. Przy pracy jednoosobowej kontrolę PR zapewniają self-review/checklista i CI; nie deklarujemy formalnego zatwierdzenia przez drugą osobę. Koniec sprintu: przegląd zgodności z AC, uzupełnienie ryzyk i rzeczywisty wpis w raporcie.
