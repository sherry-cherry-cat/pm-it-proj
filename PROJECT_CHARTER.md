# Karta projektu — PasswordVault

**Okres projektu:** 1 listopada – 15 grudnia 2026 r.  
**Realizacja:** indywidualna  
**Technologia:** C#, WPF, .NET 10, SQLite

## Cel
Do 15 grudnia chcę przygotować działającą aplikację desktopową na Windows do przechowywania haseł w zaszyfrowanym sejfie. Planuję sprawdzić działanie na co najmniej 20 fikcyjnych wpisach oraz pokazać eksport i import zaszyfrowanego pliku. Kod ma być testowany lokalnie i przez GitHub Actions.

## Po co ten projekt?
Chcę przećwiczyć tworzenie aplikacji w C#, rozdzielenie logiki od interfejsu i podstawy bezpiecznego przechowywania danych. Projekt będzie też służył do pokazania pracy ze sprintami, GitHub Issues, Pull Requestami i testami.

## Zakres obowiązkowy
- Tworzenie i otwieranie sejfu hasłem master.
- Losowy klucz DEK szyfrujący dane; KEK wyprowadzany z hasła master.
- Lokalna baza SQLite i dodawanie, edycja oraz usuwanie kont.
- Lista zapisanych kont i maskowanie haseł.
- Eksport zaszyfrowanego sejfu do wybranego pliku i import na innym Windowsie.
- Testy podstawowych operacji i CI.

## Co odkładam na później
Windows Hello, automatyczna synchronizacja z chmurą, obsługa TOTP i wersja mobilna. Dla tego terminu te funkcje zbyt łatwo rozbudowałyby zakres.

## Harmonogram
| Etap | Termin | Planowany efekt |
|---|---|---|
| Sprint 1 | 1–15 listopada | Projekt WPF, podział na biblioteki, podstawy szyfrowania, CI |
| Sprint 2 | 16–29 listopada | Baza danych, wpisy i interfejs |
| Sprint 3 | 30 listopada – 13 grudnia | Eksport/import, testy, poprawki |
| Oddanie | 14–15 grudnia | Podsumowanie, demonstracja, wersja końcowa |

## Największe ryzyka
Brak czasu przy pracy indywidualnej, błędy w szyfrowaniu, problemy z konfiguracją CI i zależnościami NuGet. Szczegóły są w `RISK_REGISTER.csv`. Jeśli pojawi się opóźnienie, ograniczę wygląd interfejsu i funkcje dodatkowe, ale nie pominę testów kryptografii.

## Jak sprawdzę wynik?
Przeprowadzę scenariusze z `docs/ACCEPTANCE_CRITERIA.md` i pokażę wyniki testów na GitHubie. Do prezentacji użyję wyłącznie danych testowych.

**Uwaga:** harmonogram jest planem, nie historią zakończonych zadań.
