# Sprint 3 — końcowa weryfikacja i przygotowanie oddania

**Planowany okres:** 30 listopada – 13 grudnia 2026 r.  
**Plan doprecyzowany:** 26 listopada, przed rozpoczęciem sprintu.  
**Termin roboczy zakończenia projektu:** 15 grudnia 2026 r.

## Cel sprintu

Chcę potwierdzić na fikcyjnych danych, że dostępne funkcje PasswordVault działają jako jeden spójny program. Przygotuję demonstrację, uporządkuję tablicę GitHub Projects i dokumentację końcową oraz wykonam ewentualne poprawki wynikające ze sprawdzenia aplikacji. Nowe funkcje nie są potrzebne do zamknięcia podstawowej wersji.

## Stan wejściowy — informacje znane 26 listopada

Mam już działający kod WPF, SQLite, szyfrowanie, eksport/import i reset. Testy xUnit zakończyły się wynikiem 25/25; zwykły workflow GitHub Actions jest zielony. Analiza SonarQube uwzględnia kod C# i pokazuje 60,0% pokrycia (425 linii). Po aktualizacji NuGet nie pojawia się wcześniejsze ostrzeżenie NU1903. Te prace wykonane przed Sprintem 3 są odnotowane w `docs/SPRINT2_REVIEW.md` — nie zapisuję ich jako przyszłych zadań ukończonych dopiero w S3.

## Plan zadań

| Zadanie | Priorytet | Czynności przewidziane w sprincie | Kryterium zakończenia |
|---|---|---|---|
| TEST-02 | Must | Przeprowadzić ręczny scenariusz na 20 fikcyjnych wpisach | Zapis, restart, odczyt i podstawowe operacje działają; wynik zanotowany |
| PORT-01 | Must | Potwierdzić eksport `.pvault` i lokalizację pliku | Można przygotować zaszyfrowaną kopię w wybranym miejscu |
| PORT-02 | Must | Potwierdzić import, w tym odmowę nieprawidłowego importu | Kopia jest odtwarzana, a błąd nie niszczy obecnego sejfu |
| US-06 | Should | Ręcznie sprawdzić anulowanie i potwierdzenie resetu | Anulowanie nie usuwa danych; reset nie kasuje osobnej kopii |
| TEST-01 | Must | Uruchomić testy i CI ponownie po ewentualnych poprawkach | Ostatni przebieg testów i build pozostaje zielony |
| DOC-03 | Must | Uzupełnić przeglądy i raport PDF o końcowe wyniki | Dokumenty zawierają faktyczne rezultaty, bez niepotwierdzonych sukcesów |
| PM-01 | Should | Powiązać istniejącą tablicę Projects z głównymi zadaniami CSV | Najważniejsze Issues mają sprint i aktualny status |
| QUAL-01 | Should | Zachować finalny screen i sprawdzić pojedynczy problem Maintainability / Quality Gate | Wyniki opisane zgodnie z tym, co pokazuje SonarQube |
| SEC-03 | Should | Potwierdzić, że dalsze zmiany nie wprowadziły nowych ostrzeżeń NuGet | Brak ponownego NU1903 lub udokumentowany nowy problem |

`QUAL-01` i `SEC-03` są już ukończone jako zadania wdrożenia analizy i kontroli zależności. Drobne ponowne sprawdzenie wyników w S3 nie oznacza powtarzania pracy od początku.

## Kryteria akceptacji

Główne scenariusze odpowiadają `AC-01–AC-14` z `docs/ACCEPTANCE_CRITERIA.md`. Do zamknięcia Sprintu 3 potrzebuję osobnego rezultatu testów ręcznych, aktualnego wyniku CI oraz materiałów pokazujących rzeczywistą tablicę Kanban i SonarQube. Zapiszę też niezamknięte ograniczenia, jeśli jakieś pozostaną.

## Zależności i kolejność

Najpierw wykonam testy ręczne na kopii fikcyjnych danych. Jeśli znajdę błąd, zapiszę go jako Issue, poprawię odpowiednią funkcję i ponowię testy. Raport końcowy uzupełnię po sprawdzeniu, bo jego treść powinna wynikać z rezultatów. Tablicę Projects mogę porządkować równolegle, bez blokowania testowania aplikacji.

## Czego nie planuję realizować

Windows Hello (`OPT-01`), synchronizacji z chmurą (`OPT-02`) ani zmiany silnika SQLite (`OPT-03`). To zadania o niskim priorytecie, poza MVP. Nie zapisuję ich jako wymaganych do ukończenia Sprintu 3.

## Przegląd i retrospektywa

Po 13 grudnia uzupełnię `docs/SPRINT3_REVIEW.md`: rzeczywiste wyniki, zadania niedokończone, aktualny stan ryzyk i krótkie wnioski. Nie przypisuję z góry ukończonych rezultatów przyszłemu sprintowi.
