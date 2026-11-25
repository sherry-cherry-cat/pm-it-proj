# Project Charter — PasswordVault

**Temat:** desktopowy menedżer haseł dla Windows  
**Realizacja:** indywidualna  
**Termin projektu (przyjęty w planie):** 15.12.2026

## Cel

Przygotuję prosty program, który pozwala utworzyć lokalny sejf, odblokować go hasłem master, zapisać co najmniej 20 przykładowych kont i przenieść ich zaszyfrowaną kopię do pliku. Do końca projektu chcę mieć też testy automatyczne, trzy podsumowania sprintów i działający workflow GitHub Actions.

## Zakres

**W projekcie:** WPF, C#/.NET 10, SQLite, dodawanie/edycja/usuwanie kont, losowy generator hasła, szyfrowanie Argon2id i AES-GCM, eksport/import pliku, testy i dokumentacja.

**Poza projektem:** automatyczna synchronizacja z chmurą, współdzielenie sejfu między użytkownikami, aplikacja mobilna i Windows Hello. Te funkcje mogłyby być rozbudową, ale nie są potrzebne do pokazania podstawowej wersji.

## Interesariusze

- **Autorka:** planuję sprinty, piszę kod, testuję i prowadzę repozytorium.
- **Użytkowniczka testowa:** chcę móc zapisać i odzyskać własne wpisy z sejfu.
- **Prowadzący:** sprawdza dokumentację, przebieg prac i efekt demonstracji.

## Kryteria sukcesu

- Aplikacja uruchamia się na Windows 11.
- Da się zapisać 20 testowych wpisów, zamknąć program i odczytać wpisy po odblokowaniu.
- Błędne hasło nie odblokowuje sejfu.
- Eksportowany plik da się zaimportować do pustego sejfu i odblokować hasłem master.
- Testy i kompilacja przechodzą w GitHub Actions, a dokumentacja pokazuje trzy sprinty.

## Założenia i ograniczenia

Pracuję sama, korzystam z Visual Studio 2026 i GitHuba. Na projekt mam ograniczony czas, dlatego ważniejsze jest działające MVP niż dodatkowe funkcje. Program przeznaczony jest do demonstracji na danych testowych, bez audytu bezpieczeństwa.

## Główne ryzyka

Największe zagrożenia to obsunięcie terminów, błędy w szyfrowaniu, kłopoty z NuGet i GitHub Actions oraz utrata danych podczas importu. Opisałam je w `RISK_REGISTER.csv` i staram się ograniczać testami oraz kopiami zapasowymi.
