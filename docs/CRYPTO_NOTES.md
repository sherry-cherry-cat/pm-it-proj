# Pierwszy moduł szyfrowania — notatki

W ramach Sprintu 1 przygotowuję osobny moduł, który można testować bez uruchamiania WPF. To jeszcze nie jest gotowy sejf użytkownika.

## Jak działa wersja 1
1. Losuję 32-bajtowy DEK, który szyfruje dane.
2. Z hasła master i losowej soli wyprowadzam KEK przez Argon2id (64 MiB pamięci, 3 iteracje, 1 wątek).
3. DEK szyfruję kluczem KEK przez AES-256-GCM.
4. Dane szyfruję oddzielnie przez AES-256-GCM za pomocą DEK.
5. W pliku można zapisać wersję formatu, parametry KDF, sól, nonce, tagi i szyfrogram. Nie zapisuję tam jawnego DEK ani hasła master.

Przy odszyfrowywaniu najpierw sprawdzana jest wersja formatu i długości pól. AES-GCM odrzuca złe hasło oraz zmodyfikowane zaszyfrowane dane (w obu przypadkach weryfikacja taga się nie powiedzie).

## Ograniczenia
- Format `v1` jest prototypem. Parametry Argon2id są na razie stałe, a wielkość danych ograniczona do 1 MiB.
- Szyfrowanie nie jest jeszcze podłączone do okna WPF ani SQLite.
- Hasło master jest przekazywane jako `string`, więc nie mam pełnej kontroli nad czasem jego obecności w pamięci.
- Nie ma zarządzania zapisem atomowym, kopiami zapasowymi ani migracjami formatu — to kolejne zadania.
- Kod nie przeszedł niezależnego audytu bezpieczeństwa. Testować tylko na fikcyjnych hasłach.

## Testy
Poprawny odczyt, błędne hasło, naruszenie integralności danych i klucza, różne losowe wartości, zapis/odczyt JSON, odrzucenie złego nagłówka i puste dane.

Użyte elementy: `AesGcm` z .NET oraz pakiet `Konscious.Security.Cryptography.Argon2` (1.3.1).
