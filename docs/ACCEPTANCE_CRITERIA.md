# Kryteria akceptacji podstawowej wersji

Nie wszystkie poniższe punkty mają taki sam rodzaj testu. Jedne sprawdzam w xUnit, inne wymagają uruchomienia aplikacji.

| ID | Jak sprawdzę | Rodzaj |
|---|---|---|
| AC-01 | Przy pierwszym uruchomieniu tworzę sejf z hasłem master co najmniej 12 znaków. | ręczny + jednostkowy |
| AC-02 | Po zamknięciu programu poprawne hasło ponownie otwiera sejf. | ręczny + jednostkowy |
| AC-03 | Złe hasło lub zmodyfikowany szyfrogram nie ujawniają danych. | jednostkowy |
| AC-04 | Mogę dodać wpis, zapisać go i odczytać po restarcie. | ręczny + jednostkowy |
| AC-05 | Edycja i usunięcie dotyczą wskazanego wpisu i zapisują się poprawnie. | ręczny + jednostkowy |
| AC-06 | Lista i wyszukiwanie działają, także dla kilku różnych serwisów. | ręczny |
| AC-07 | W SQLite znajdują się zaszyfrowane rekordy, a nie jawne hasła. | przegląd bazy/kodu |
| AC-08 | Hasło w formularzu jest domyślnie ukryte. | ręczny |
| AC-09 | Generator tworzy hasło o zadanej dozwolonej długości i odrzuca złą długość. | jednostkowy |
| AC-10 | Eksport zapisuje zaszyfrowany plik `.pvault` w wybranym miejscu. | ręczny + jednostkowy |
| AC-11 | Import poprawnego eksportu pozwala odtworzyć zapisane wpisy. | ręczny + jednostkowy |
| AC-12 | Zły import nie nadpisuje wcześniej istniejących danych. | jednostkowy + ręczny |
| AC-13 | Reset pokazuje potwierdzenie; anulowanie nic nie usuwa, a potwierdzenie usuwa lokalny sejf bez kasowania eksportu. | ręczny + jednostkowy |
| AC-14 | Potrafię zapisać i ponownie odczytać 20 fikcyjnych kont. | ręczny, do potwierdzenia |

**Wyniki potwierdzone na 26.11:** testy xUnit 25/25, zielone CI i analiza SonarQube z pokryciem 60,0% (425 linii). Kontrola NuGet po aktualizacji nie zgłasza już poprzedniego NU1903. Nie oznaczam całej tabeli jako zaliczonej tylko dlatego, że testy automatyczne przeszły; AC-14 i pełny scenariusz ręczny wymagają jeszcze sprawdzenia.
