# PasswordVault

Projekt indywidualny na przedmiot **Zarządzanie projektami IT** (grupa 2). Napisałam lokalny menedżer haseł na Windows w C#/.NET 10 i WPF. Program korzysta z SQLite (EF Core), Argon2id i AES-256-GCM. To aplikacja edukacyjna, testowana wyłącznie na fikcyjnych danych i bez niezależnego audytu bezpieczeństwa.

## Funkcje

Tworzenie i odblokowanie sejfu hasłem master, dodawanie/edycja/usuwanie/wyszukiwanie wpisów, generator haseł, zaszyfrowany eksport i import `.pvault` oraz reset lokalnego sejfu z potwierdzeniem. W aktualnej wersji kod tych funkcji działa; pełna demonstracja ręczna na 20 przykładowych kontach pozostaje zaplanowana.

## Uruchomienie

1. W Visual Studio 2026 z obsługą `.NET desktop development` otwieram `Password-Vault/Password-Vault.slnx`.
2. Przywracam pakiety i wybieram `Build Solution`.
3. Ustawiam projekt `Password-Vault` jako startowy i uruchamiam (`F5`).
4. Tworzę lokalny sejf (hasło master co najmniej 12 znaków) lub odblokowuję istniejący.

Baza domyślnie znajduje się w `%LOCALAPPDATA%/PasswordVaultDemo/vault.sqlite`.

## Testy i analiza jakości — potwierdzone 26.11.2026

| Kontrola | Wynik |
|---|---|
| xUnit | **25/25** zaliczonych testów, 0 błędów |
| GitHub Actions | Zielona kompilacja, testy i `dotnet format whitespace` na `main` |
| SonarQube Cloud | Analiza C#, pokrycie **60,0% z 425 linii**, 0% duplikacji |
| SonarQube — problemy | Security A / 0; Reliability A / 0; Maintainability A / 1 |
| NuGet | Po aktualizacji zależności wcześniejsze `NU1903` nie występuje |

Wynik Quality Gate trzeba sprawdzić oddzielnie. Analiza i procent pokrycia nie zastępują testów ręcznych.

## Dokumentacja projektu

- [`PROJECT_CHARTER.md`](PROJECT_CHARTER.md) — cel SMART, zakres IN/OUT i interesariusze.
- [`BACKLOG.csv`](BACKLOG.csv) — **24 pozycje** z priorytetami, planowanym sprintem i stanem na 26.11; `QUAL-01` i `SEC-03` są ukończone przed S3.
- [`RISK_REGISTER.csv`](RISK_REGISTER.csv) — 12 ryzyk, działania i aktualny status.
- [`SPRINT1_PLAN.md`](SPRINT1_PLAN.md), [`SPRINT2_PLAN.md`](SPRINT2_PLAN.md) — historyczne plany zachowane bez przerabiania pierwotnych założeń.
- [`SPRINT3_PLAN.md`](SPRINT3_PLAN.md) — plan końcowej weryfikacji przed startem trzeciej iteracji.
- [`docs/SPRINT1_REVIEW.md`](docs/SPRINT1_REVIEW.md) — wynik S1, opóźnienia i przeniesienie zadań.
- [`docs/SPRINT2_REVIEW.md`](docs/SPRINT2_REVIEW.md) — szczegółowy stan S2: działająca aplikacja, 25 testów, CI, pokrycie i audyt NuGet.
- [`docs/SPRINT3_REVIEW.md`](docs/SPRINT3_REVIEW.md) — szablon wyniku do wypełnienia po S3.
- [`docs/WYMAGANIA.md`](docs/WYMAGANIA.md) i [`docs/ACCEPTANCE_CRITERIA.md`](docs/ACCEPTANCE_CRITERIA.md) — wymagania i kryteria testowe.
- [`docs/GITHUB_PROJECTS.md`](docs/GITHUB_PROJECTS.md) — plan odwzorowania CSV na istniejącą tablicę Kanban.
- [`docs/ARCHITEKTURA.md`](docs/ARCHITEKTURA.md), [`docs/CRYPTO_NOTES.md`](docs/CRYPTO_NOTES.md) — budowa i ograniczenia techniczne.

## Dodatkowe pomysły

Windows Hello, automatyczna synchronizacja chmurowa i zmiana silnika bazy danych pozostają niezrealizowanymi rozszerzeniami o niskim priorytecie poza MVP. Dokumentacja na 26 listopada nie zamyka z wyprzedzeniem Sprintu 3.
