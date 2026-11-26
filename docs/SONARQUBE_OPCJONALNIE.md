# SonarQube Cloud — aktualna konfiguracja i wynik

Ten dokument zastępuje wcześniejsze instrukcje opisujące niedziałający pomiar. Na 26 listopada analiza kodu C# i przekazywanie pokrycia do SonarQube działają.

- Osobny workflow GitHub Actions: `.github/workflows/build.yml` (nie nadpisuję go paczką dokumentacyjną).
- Uwierzytelnianie: sekret `SONAR_TOKEN` w GitHub Actions. Nie umieszczam tokenu w repozytorium.
- Testy: 25/25 w .NET 10.
- Pokrycie: zbierane przez narzędzie Microsoft `dotnet-coverage`, raport XML przekazywany do SonarQube.
- Widok `Overall Code`: **60,0% z 425 linii objętych pomiarem**; duplikacje **0,0%**; Security **A**, Reliability **A**, Maintainability **A** z **jednym** otwartym problemem.

Samo zielone uruchomienie workflow i ocena A nie oznaczają automatycznie, że osobny **Quality Gate** jest zaliczony. Jego rzeczywisty wynik należy sprawdzić oddzielnie w panelu SonarQube.

Wcześniejsze 0% było problemem importu danych pokrycia, a nie wynikiem 25 niezaliczonych testów. Początkowy kolektor `XPlat Code Coverage` nie był rozpoznawany przez runner. Po zmianie na `dotnet-coverage` procent zaczął się poprawnie wyświetlać.
