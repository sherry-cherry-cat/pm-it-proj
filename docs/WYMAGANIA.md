# Wymagania aplikacji

## Funkcjonalne

| ID | Wymaganie | Jak sprawdzę |
|---|---|---|
| F-01 | Mogę utworzyć sejf z hasłem master. | Pierwsze uruchomienie i utworzenie pliku. |
| F-02 | Mogę odblokować sejf. | Dobre hasło działa, złe nie. |
| F-03 | Mogę dodać, zmienić i usunąć konto. | Zmiany są widoczne po ponownym otwarciu. |
| F-04 | Mogę wyszukać konto po nazwie. | Lista filtruje wyniki. |
| F-05 | Mogę wygenerować losowe hasło. | Hasło ma 20 znaków, kolejne próby są różne. |
| F-06 | Mogę wyeksportować i zaimportować sejf. | Eksport w `.pvault`, import na drugiej instalacji. |

## Niefunkcjonalne

| ID | Wymaganie | Jak sprawdzę |
|---|---|---|
| NF-01 | Dane wpisów nie są zapisywane jawnie w SQLite. | Kontrola bazy i test zapisu. |
| NF-02 | Zmiana szyfrogramu jest wykrywana. | Negatywny test AES-GCM. |
| NF-03 | Projekt uruchamia się na Windows 11 i .NET 10. | Build i ręczne uruchomienie. |
| NF-04 | Testy automatyczne uruchamiają się po push/PR. | Wynik GitHub Actions. |
| NF-05 | Zły import nie niszczy poprzedniego sejfu. | Test z błędnym hasłem i ponowny odczyt. |

Wymagania są ograniczone do lokalnej wersji. Nie obiecuję usługi sieciowej ani dostępności 24/7, bo aplikacja działa na komputerze użytkowniczki.
