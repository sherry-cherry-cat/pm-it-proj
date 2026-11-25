# PasswordVault

Projekt na przedmiot **Zarządzanie projektami IT**. Prosta aplikacja desktopowa w C# do przechowywania haseł w lokalnym sejfie. Interfejs jest napisany w WPF, a baza danych korzysta z SQLite i EF Core.

## Co działa w przygotowanym kodzie

- Utworzenie sejfu z hasłem master (minimum 12 znaków).
- Odblokowanie go hasłem master.
- Dodawanie, poprawianie, usuwanie i wyszukiwanie wpisów.
- Losowy generator haseł.
- Eksport zaszyfrowanej kopii do pliku `.pvault` i jej import.
- Testy xUnit i workflow GitHub Actions.

Dane wpisów (nazwy kont, loginy, hasła i notatki) są szyfrowane w całości. Hasło master służy do wyprowadzenia klucza KEK (Argon2id), który odblokowuje losowy klucz DEK. Sam DEK szyfruje wpisy przez AES-256-GCM. W bazie pozostają identyfikatory techniczne i szyfrogramy.

**Uwaga:** to projekt edukacyjny, a nie audytowany produkt do przechowywania prawdziwych haseł. Nie używaj w nim rzeczywistych danych logowania.

## Uruchomienie

1. Otwórz `Password-Vault/Password-Vault.slnx` w **Visual Studio 2026**.
2. Upewnij się, że masz obciążenie `.NET desktop development` i SDK .NET 10.
3. Kliknij prawym na rozwiązaniu → **Restore NuGet Packages**, następnie **Build → Build Solution**.
4. Projekt `Password-Vault` ustaw jako **Startup Project** i uruchom `F5`.
5. Wpisz hasło master i utwórz nowy sejf.

Lokalny plik powstaje w `%LOCALAPPDATA%\PasswordVaultDemo\vault.sqlite`. Import zastępuje aktualne dane po potwierdzeniu. Przed importem zrób eksport obecnego sejfu.

## Testy i CI

Lokalnie: **Test → Test Explorer → Run All Tests**. W GitHub Actions workflow `.NET - build, test i format` sprawdza kompilację, testy i formatowanie. Zielony status można wpisać do raportu dopiero po faktycznym wykonaniu workflow na GitHubie.

## Dokumentacja

- `PROJECT_CHARTER.md` — cel i zakres.
- `BACKLOG.csv` — zadania, User Stories, kryteria akceptacji i sprinty.
- `RISK_REGISTER.csv` — rejestr ryzyk.
- `SPRINT1_PLAN.md`, `SPRINT2_PLAN.md`, `SPRINT3_PLAN.md` — plany sprintów.
- `docs/WYMAGANIA.md`, `docs/ACCEPTANCE_CRITERIA.md`, `docs/DoR_DoD.md` — wymagania i kryteria.
- `docs/SPRINT*_REVIEW.md` — miejsce na rzeczywiste wyniki sprintów.
- `docs/RAPORT_KONCOWY_DO_UZUPELNIENIA.md` — podstawa raportu PDF.
- `docs/GITHUB_CHECKLIST.md` — konfiguracja repozytorium, PR, Issues i Projects.

## Ograniczenia wersji

Nie ma synchronizacji w chmurze, Windows Hello ani klienta mobilnego. Hasła pozostają w pamięci aplikacji w czasie odblokowanej sesji; nie dodawałam też automatycznej blokady i automatycznego czyszczenia schowka. Te elementy są poza obowiązkowym MVP.
