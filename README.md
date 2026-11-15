# PasswordVault

Projekt na zaliczenie z Zarządzania projektami IT. Robię prostego menedżera haseł na Windows w C# i WPF.

Aplikacja docelowo ma zapisywać loginy i hasła w zaszyfrowanym sejfie. Hasło master służy do odblokowania klucza szyfrującego dane. Chcę też umożliwić eksport zaszyfrowanego pliku i jego otwarcie na innym komputerze.

## Co jest teraz

- Projekt WPF (`Password-Vault/Password-Vault/`) — na razie okno testowe.
- `PasswordVault.Core` — podstawowy model formatu i interfejs szyfrowania.
- `PasswordVault.Infrastructure` — prototyp AES-GCM i Argon2id, bez podłączenia do WPF.
- `PasswordVault.Tests` — testy jednostkowe prototypu.
- `.github/workflows/ci.yml` — kompilacja i testy po push/PR.

Projektów z bibliotekami nie należy mylić z gotowym menedżerem haseł: **nie ma jeszcze bazy SQLite, widoków kont ani obsługi importu/eksportu w aplikacji**.

## Uruchomienie

Potrzebne: Visual Studio 2026 z obciążeniem **.NET desktop development** i .NET 10 SDK.

1. Otworzyć `Password-Vault/Password-Vault.slnx`.
2. Przy pierwszym uruchomieniu Visual Studio pobierze pakiety NuGet.
3. Wybrać projekt `Password-Vault` jako startowy i uruchomić `F5`.
4. Otworzyć `Test > Test Explorer` i uruchomić wszystkie testy.

Z terminala w katalogu repozytorium:

```powershell
dotnet restore .\Password-Vault\Password-Vault.slnx
dotnet build .\Password-Vault\Password-Vault.slnx --configuration Release
dotnet test .\Password-Vault\PasswordVault.Tests\PasswordVault.Tests.csproj --configuration Release
```

## Dokumentacja

- [Plan Sprintu 1](SPRINT1_PLAN.md)
- [Backlog](BACKLOG.csv)
- [Rejestr ryzyk](RISK_REGISTER.csv)
- [Przegląd Sprintu 1](docs/SPRINT1_REVIEW.md)
- [Opis pierwszej wersji kryptografii](docs/CRYPTO_NOTES.md)

Używam tylko danych testowych. Prawdziwych haseł i plików sejfu nie zapisuję w repozytorium.
