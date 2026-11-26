# Stan weryfikacji projektu — 26 listopada 2026 r.

## Potwierdzone

- Aplikacja PasswordVault uruchamia się na Windows; w przesłanych screenach widać ekran odblokowania i listę kont.
- W rozwiązaniu są cztery projekty: WPF, `Core`, `Infrastructure`, `Tests`.
- Testy xUnit: **25/25 zaliczone, 0 niepowodzeń, 0 pominiętych** (Test Explorer i wcześniejszy TRX).
- GitHub Actions: zielony przebieg kompilacji, testów i `dotnet format whitespace` na gałęzi `main`.
- SonarQube analizuje kod C#, a `Overall Code` pokazuje **60,0% pokrycia z 425 linii**, 0,0% duplikacji, oceny Security A i Reliability A oraz **jeden** otwarty problem Maintainability (ocena A).
- Rozwiązany problem raportowania pokrycia: zamiast nieskutecznego kolektora XPlat wykorzystałam `dotnet-coverage` i raport XML przekazywany do SonarQube.
- Zależności NuGet: po aktualizacji nie występuje wcześniejsze ostrzeżenie `NU1903` dla SQLitePCLRaw.
- GitHub Projects: istnieje tablica Kanban i widok Roadmap z kilkoma Issues.
- Przegląd Sprintu 1 został zapisany po jego terminie; Sprint 2 trwa, a Sprint 3 jeszcze się nie rozpoczął.

## Do potwierdzenia osobno

- [ ] Wynik pełnego ręcznego przejścia po scenariuszu 20 fikcyjnych kont (`AC-14`).
- [ ] Ręczny scenariusz eksportu → resetu → importu i nieudanego importu.
- [ ] Jednoznaczny stan SonarQube Quality Gate (nie wynika automatycznie z ocen A).
- [ ] Końcowe odwzorowanie zadań CSV na GitHub Projects, z odpowiednimi statusami.
- [ ] Dostęp prowadzącego do repozytorium; w prezentacji jest wymóg prywatności, a udostępniony wcześniej fork był publiczny.
- [ ] Rezultat i retrospektywa Sprintu 3 po terminie 13 grudnia.

## Źródła dowodów

Zrzuty Test Explorera, GitHub Actions, SonarQube (pokrycie 60,0% i pliki C#), tablicy Kanban, widoków aplikacji oraz końcowy PDF. Nie podaję niezweryfikowanych adresów uruchomień ani nie twierdzę, że istnieje dodatkowy audyt bezpieczeństwa.
