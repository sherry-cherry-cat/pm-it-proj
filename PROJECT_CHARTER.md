# Project Charter – PasswordVault

**Przedmiot:** Zarządzanie projektami IT  
**Grupa:** 2  
**Realizacja:** indywidualna  
**Termin roboczy oddania:** 15.12.2026

## Cel projektu (SMART)

Chcę przygotować do 15 grudnia 2026 r. prostą aplikację desktopową do przechowywania haseł na Windows. Użytkowniczka ma móc utworzyć sejf zabezpieczony hasłem master, dodawać i zmieniać wpisy oraz odzyskać je z zaszyfrowanego eksportu. Sprawdzę działanie aplikacji na fikcyjnych danych, uruchomię testy jednostkowe i kontrole kodu na GitHubie. Przebieg prac podzieliłam na trzy sprinty.

Za mierzalny wynik przyjmuję możliwość zapisania i ponownego odczytania **20 przykładowych wpisów**, odrzucanie błędnego hasła, poprawny eksport/import oraz zielone testy i kompilację w GitHub Actions. Test 20 wpisów wymaga jeszcze ręcznego potwierdzenia w demonstracji; nie zastępuję go samym wynikiem testów jednostkowych.

## Zakres

**W zakresie (MVP):** aplikacja Windows w C#/.NET 10 i WPF, lokalny SQLite (EF Core), tworzenie i odblokowanie sejfu, dodawanie, edycja, usuwanie i wyszukiwanie kont, generator haseł, reset z potwierdzeniem, eksport/import zaszyfrowanego pliku, testy xUnit i CI.

**Poza MVP:** logowanie dodatkowo przez Windows Hello, automatyczna synchronizacja z chmurą, zmiana silnika SQLite na inny, aplikacja mobilna i udostępnianie sejfu innym użytkownikom. Zostawiłam te pomysły w backlogu z niskim priorytetem, bez deklarowania ich realizacji.

## Interesariusze i odpowiedzialność

- **Autorka projektu:** planowanie, programowanie, testy, GitHub i dokumentacja. Pracuję samodzielnie.
- **Użytkowniczka testowa:** korzysta z lokalnego sejfu i sprawdza, czy można zapisać oraz odzyskać dane.
- **Prowadzący:** ocenia spójność planu, realizacji, testów, kontroli jakości i raportu.

## Kryteria zakończenia

- Program można uruchomić i obsłużyć na Windows.
- Dodany wpis zostaje zachowany po ponownym uruchomieniu aplikacji.
- Złe hasło lub uszkodzone dane nie pozwalają odczytać zawartości.
- Eksport jest zaszyfrowany i można go ponownie zaimportować na danych testowych.
- Repozytorium zawiera backlog, wymagania, rejestr ryzyk, trzy plany sprintów oraz ich przeglądy.
- GitHub Actions uruchamia kompilację, testy i kontrolę formatowania. Dodatkowo pokazuję analizę SonarQube i tablicę GitHub Projects.

## Założenia i ograniczenia

Pracuję samodzielnie, w Visual Studio 2026, przy ograniczonym czasie. Dlatego najpierw kończę podstawową wersję programu, a dopiero potem dodatki. Aplikacja ma charakter edukacyjny i **nie przeszła audytu bezpieczeństwa**; do demonstracji używam wyłącznie wymyślonych danych logowania.

## Najważniejsze ryzyka

Opóźnienie terminów, błędy kryptograficzne, utrata danych przy imporcie/resetowaniu, podatności pakietów NuGet, starzenie się parametrów zabezpieczeń i niespójność dokumentacji z kodem. Szczegóły oraz działania ograniczające ryzyko są w `RISK_REGISTER.csv`.
