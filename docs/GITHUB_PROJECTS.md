# GitHub Projects — tablica i odwzorowanie backlogu

Prowadzę istniejącą tablicę GitHub Projects z widokami Board (Kanban) oraz Roadmap. Widoczne na screenie przykładowe Issues dotyczą m.in. SonarQube, GitHub Actions, Windows Hello i chmury, ale nie odwzorowują jeszcze wszystkich 24 pozycji `BACKLOG.csv`.

## Jak porządkuję tablicę

1. Zmieniam roboczą nazwę `untitled project` na `PasswordVault — projekt` (jeśli jeszcze tego nie zrobiłam).
2. Zachowuję kolumny `Backlog`, `Ready`, `In progress`, `In review`, `Done`; nie muszę tworzyć drugiej tablicy.
3. Dodaję pole `Sprint` (`S1`, `S2`, `S3`, `Poza MVP`) i ewentualne pole `Priorytet` (`Must`, `Should`, `Could`).
4. Zadania w Issue oznaczam identyfikatorami z `BACKLOG.csv`, np. `[SEC-01] Utworzenie sejfu`. Sprawdzam istniejące Issues, aby nie powielać tych samych zadań.
5. Zachowuję rzeczywiste statusy: ukończony kod i test nie oznaczają automatycznie ukończonej demonstracji ręcznej.

## Przykładowe statusy zgodne z dokumentacją

| Zadanie | Zalecany status po sprawdzeniu Issue | Uzasadnienie |
|---|---|---|
| CI-01 | Done | Build, test i format na zielono |
| TEST-01 | Done | 25/25 testów |
| QUAL-01 | Done | Analiza SonarQube i raportowanie 60,0% działają |
| SEC-03 | Done | Sprawdzone zależności, wcześniejsze NU1903 usunięte |
| PM-01 | In progress | Tablica istnieje, ale nie wszystkie zadania są przypisane |
| TEST-02 | Backlog / Ready | Pełny test 20 fikcyjnych wpisów jeszcze bez wyniku |
| OPT-01, OPT-02, OPT-03 | Backlog | Niski priorytet, poza MVP |

Dokument ten nie zmienia statusów na GitHubie. Po wgraniu plików CSV i Markdown aktualizację samych Issues oraz pól Projects wykonam oddzielnie w interfejsie GitHuba.

## Dowody

Przed oddaniem warto zachować aktualny screenshot tablicy Board z widocznymi stanami, Roadmap oraz jednego Issue zawierającego ID, User Story i kryterium akceptacji. Link do tablicy może znajdować się w README lub raporcie, ale nie wpisuję go tutaj bez potwierdzenia adresu.
