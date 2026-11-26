# Wymagania PasswordVault

Podzieliłam je na funkcjonalne (co robi aplikacja) i niefunkcjonalne (jak ma działać). Powiązania z backlogiem pozwalają sprawdzić, po co jest każde zadanie.

## Wymagania funkcjonalne

| ID | Wymaganie | Zadanie | Kryteria |
|---|---|---|---|
| F-01 | Tworzenie sejfu przy pierwszym uruchomieniu. | SEC-01 | AC-01 |
| F-02 | Odblokowanie hasłem master i odmowa dostępu po złym haśle. | SEC-02 | AC-02, AC-03 |
| F-03 | Dodanie, edycja i usuwanie wpisów. | US-01, US-02, US-03 | AC-04, AC-05 |
| F-04 | Lista wpisów i wyszukiwanie po nazwie. | US-04 | AC-06 |
| F-05 | Generowanie losowego hasła. | US-05 | AC-09 |
| F-06 | Eksport sejfu do wybranego pliku i import kopii. | PORT-01, PORT-02 | AC-10, AC-11, AC-12 |
| F-07 | Reset lokalnego sejfu po potwierdzeniu. | US-06 | AC-13 |

## Wymagania niefunkcjonalne

| ID | Wymaganie | Jak je sprawdzę |
|---|---|---|
| NF-01 | Wpisy w SQLite są przechowywane jako szyfrogramy, bez jawnych haseł. | Kontrola modelu bazy i test zapisu. |
| NF-02 | Szyfrowanie wykrywa naruszenie integralności danych. | Testy AES-GCM dla zmodyfikowanego szyfrogramu. |
| NF-03 | Aplikacja działa na Windows i buduje się na .NET 10. | Uruchomienie w Visual Studio i build CI. |
| NF-04 | Testy i kontrola formatowania są uruchamiane przez GitHub Actions. | Zielone kroki build/test/format. |
| NF-05 | Nieudany import nie niszczy poprzedniego sejfu. | Test automatyczny i ręczny z błędnym plikiem. |
| NF-06 | Dane testowe nie trafiają do repozytorium. | `.gitignore`, przegląd `Git Changes`. |

## Poza zakresem pierwszej wersji

- Windows Hello jako dodatkowe potwierdzenie działań (`OPT-01`).
- Automatyczna synchronizacja pliku przez usługę chmurową (`OPT-02`).
- Zmiana silnika bazy danych z SQLite na inny (`OPT-03`).

Są to pomysły na przyszłość, a nie brakujące kryteria akceptacji MVP.
