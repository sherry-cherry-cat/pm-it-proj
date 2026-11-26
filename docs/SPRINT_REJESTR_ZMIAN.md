# Rejestr zmian i przeniesień między sprintami

Zachowuję pierwotne plany `SPRINT1_PLAN.md` i `SPRINT2_PLAN.md`, aby pokazać różnicę między planem a wykonaniem. W czasie pracy mogłam zmieniać bieżący backlog sprintu; historycznego planu nie poprawiam wstecznie. Wyniki i przesunięcia zapisuję w osobnych plikach `REVIEW`.

| Data lub okres | Zmiana / obserwacja | Powiązane ID | Skutek |
|---|---|---|---|
| 1–15.11 | Uruchomiłam szkielet WPF, lecz nie zamknęłam jeszcze całej kryptografii i CI | DEV-01, SEC-01, SEC-02, CI-01 | Niezamknięte prace przeszły do następnej iteracji |
| 16–26.11 | Dokończyłam mechanizm sejfu, SQLite i funkcje kont, poprawiłam reset i wyszukiwanie | SEC-01, SEC-02, DATA-01, US-01–US-06 | Powstała działająca wersja aplikacji |
| 25–26.11 | Poprawiłam błędne asercje kryptograficzne i uruchomiłam zielone CI | TEST-01, CI-01 | 25 testów, 0 błędów |
| Do 26.11 | Zaimplementowałam eksport i import przed planowanym S3 | PORT-01, PORT-02 | Kod gotowy; test ręczny pozostaje w planie S3 |
| Do 26.11 | Zmieniłam sposób zbierania pokrycia na `dotnet-coverage` | QUAL-01 | Ukończone przed S3, SonarQube pokazuje 60,0% z 425 linii |
| Do 26.11 | Zaktualizowałam zależności i ponownie zweryfikowałam NuGet | SEC-03 | Ukończone przed S3; wcześniejsze NU1903 usunięte |
| 26.11 | Dostosowałam plan Sprintu 3 przed startem | SPRINT3_PLAN | Priorytet: testy ręczne, demonstracja, finalne dokumenty |
| 30.11–13.12 | Wynik zapiszę po zakończeniu sprintu | SPRINT3_REVIEW | Brak fikcyjnego zamknięcia przyszłych zadań |

Kolumna `Planowany sprint` w `BACKLOG.csv` nie jest datą faktycznego ukończenia. Zadania planowane na S3 mogły być wykonane przed rozpoczęciem S3; tę różnicę zachowałam w statusie i przeglądzie Sprintu 2.
