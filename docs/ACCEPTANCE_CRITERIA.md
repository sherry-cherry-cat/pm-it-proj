# Kryteria akceptacji

## Sprint 1
- S1-01: aplikacja WPF uruchamia się na Windows.
- S1-02: biblioteki Core i Infrastructure budują się wraz z aplikacją.
- S1-03: zaszyfrowane dane można odszyfrować poprawnym hasłem master.
- S1-04: błędne hasło nie umożliwia odczytania danych.
- S1-05: zmodyfikowany szyfrogram lub zaszyfrowany klucz jest odrzucany.
- S1-06: wynik szyfrowania da się zapisać i odczytać jako JSON.
- S1-07: CI uruchamia build i testy na GitHubie.

## Docelowe MVP (Sprint 2 i 3)
- Użytkownik może dodać, edytować i usunąć wpis.
- Hasła i nazwy stron nie leżą jawnie w SQLite.
- Po ponownym uruchomieniu sejf można odblokować hasłem master.
- Można wybrać miejsce eksportu zaszyfrowanego sejfu i zaimportować go na drugim Windowsie.
- Błędny plik importu nie uszkadza dotychczasowego sejfu.
