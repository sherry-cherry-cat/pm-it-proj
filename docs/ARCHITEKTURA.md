# Krótki opis architektury

Rozwiązanie jest podzielone na cztery projekty.

- `Password-Vault` – aplikacja WPF, prosty formularz i obsługa przycisków. Na potrzeby MVP używam code-behind zamiast rozbudowanego MVVM.
- `PasswordVault.Core` – model wpisu i generator haseł. Ta biblioteka nie zależy od WPF.
- `PasswordVault.Infrastructure` – Argon2id, AES-GCM i zapis szyfrogramów w SQLite przez EF Core.
- `PasswordVault.Tests` – testy jednostkowe szyfrowania, zapisu i importu.

**Przepływ danych:** hasło master → Argon2id → klucz KEK → odszyfrowanie losowego DEK → AES-GCM → odszyfrowanie pojedynczych wpisów.

W bazie są identyfikatory wpisów oraz zaszyfrowane obiekty. Nazwa serwisu i login nie są przechowywane jawnie. Eksport `.pvault` zawiera nagłówek sejfu i szyfrogramy, dlatego można przenosić go na drugi komputer bez klucza sprzętowego.

**Uproszczenia:** nie ma autoblokady, bezpiecznego czyszczenia wszystkich tekstów z pamięci .NET, wieloużytkownikowości ani audytu kryptograficznego. Projekt służy tylko do zajęć.
