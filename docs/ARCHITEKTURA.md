# Architektura PasswordVault

## Technologia i projekty

Aplikację stworzyłam w C# na .NET 10 jako Windows Presentation Foundation (WPF). Widok jest zdefiniowany w XAML (`MainWindow.xaml`), a zdarzenia kontrolek obsługuję w `MainWindow.xaml.cs`. Nie wdrażałam rozbudowanego MVVM; zamiast tego oddzieliłam interfejs od kodu kryptografii i zapisu danych.

Rozwiązanie `Password-Vault/Password-Vault.slnx` składa się z czterech projektów:

| Projekt | Rola |
|---|---|
| `Password-Vault` | Interfejs WPF; kompilacja `net10.0-windows`, `UseWPF=true` |
| `PasswordVault.Core` | Modele wpisów, generator haseł i interfejsy |
| `PasswordVault.Infrastructure` | `VaultCrypto`, `VaultRepository`, SQLite/EF Core, import i eksport |
| `PasswordVault.Tests` | 12 testów `VaultCryptoTests` i 13 testów `VaultTests` (łącznie 25) |

## Przepływ danych

Po odblokowaniu sejfu klucz DEK jest odzyskiwany przez Argon2id i AES-GCM. Przy dodawaniu wpisu formularz przekazuje dane do repozytorium, które szyfruje rekord i zapisuje szyfrogram do SQLite. W bazie nie przechowuję haseł kont w postaci jawnej. Baza znajduje się domyślnie w `%LOCALAPPDATA%/PasswordVaultDemo/vault.sqlite`.

Eksport `.pvault` przenosi zaszyfrowaną kopię danych. Import sprawdza nagłówek, wersję formatu i możliwość odszyfrowania rekordów przed transakcją zastępującą zawartość sejfu. Ogranicza to ryzyko utraty danych przy błędnym pliku.

## Zależności i narzędzia

Wykorzystuję EF Core SQLite, Konscious.Security.Cryptography.Argon2, xUnit oraz Microsoft.NET.Test.Sdk. Do kontroli jakości uruchamiam GitHub Actions, dotnet format, SonarQube Cloud i pomiar pokrycia `dotnet-coverage`. Na 26 listopada testy dały wynik 25/25, a SonarQube wskazuje 60,0% pokrycia z 425 linii.

Program nie korzysta z usługi chmurowej, zewnętrznego serwera bazy ani Windows Hello. Pozostają one pomysłami na przyszłe rozszerzenia, nie kryteriami zaliczenia MVP.
