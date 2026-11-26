# Zabezpieczenie sejfu i informacje techniczne

## Zastosowane elementy

Aplikacja używa hasła master do wyprowadzenia klucza KEK przez Argon2id. Dla lokalnego sejfu `VaultCrypto.cs` stosuje domyślnie 32 MiB pamięci, trzy iteracje i dwa wątki. Losowy 32-bajtowy DEK służy do szyfrowania wpisów przez AES-256-GCM. W nagłówku znajdują się sól, parametry KDF, wersja formatu i zaszyfrowany DEK; hasło master ani jawny DEK nie są tam zapisywane. Wpisy są szyfrowane osobno, z dodatkowym kontekstem AAD powiązanym z identyfikatorem rekordu.

W projekcie istnieje również `VaultCryptoService.cs` z wcześniejszym testowym formatem pliku i oddzielnymi parametrami (64 MiB / 3 / 1). To nie jest drugi aktywny format lokalnej bazy — obie klasy są wykorzystywane w różnych testach, nie należy mieszać ich parametrów.

## Sprawdzone właściwości

W xUnit mam testy dobrego i błędnego hasła, niepoprawnego szyfrogramu lub tagu, operacji na bazie, eksportu, importu i resetu. Aktualny wynik całego zestawu wynosi 25/25. Import sprawdza zawartość przed zastąpieniem danych i zapisuje ją transakcyjnie, żeby nieudana próba nie usuwała obecnego sejfu.

## Kontrola pakietów i ograniczenia

W starszym przebiegu otrzymałam ostrzeżenie `NU1903` dotyczące przechodniego `SQLitePCLRaw.lib.e_sqlite3 2.1.11`. Po aktualizacji zależności kolejne sprawdzenie nie wykazywało już wcześniejszego ostrzeżenia. Tę konkretną naprawę traktuję jako zakończoną; możliwość pojawienia się kolejnych podatności pozostaje ryzykiem do monitorowania.

Nie wdrożyłam automatycznej rotacji DEK ani pełnej migracji parametrów KDF. Klucz kryptograficzny nie ma automatycznej daty ważności; problemem jest potencjalna kompromitacja sekretu i starzenie się parametrów. Program ma charakter dydaktyczny i nie przeszedł niezależnego audytu, dlatego używam do niego fikcyjnych danych.
