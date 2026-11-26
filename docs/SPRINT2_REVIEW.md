# Sprint 2 — stan realizacji, wyniki i problemy

**Planowany okres:** 16–29 listopada 2026 r.  
**Stan na:** 26 listopada 2026 r. (sprint nadal trwa)  
**Plan źródłowy:** [`SPRINT2_PLAN.md`](../SPRINT2_PLAN.md), zachowany bez zmian.

## 1. Cel iteracji

Zaplanowałam przejście od uruchamialnego szkieletu do lokalnego sejfu, w którym można zapisać dane konta i odzyskać je po ponownym uruchomieniu aplikacji. Zgodnie z planem obejmowało to SQLite (DATA-01), operacje na wpisach (US-01–US-04), generator (US-05) i reset (US-06). Włączyłam też zadania z pierwszego sprintu, których nie zdążyłam zamknąć w terminie: utworzenie i odblokowanie sejfu (SEC-01, SEC-02) oraz CI (CI-01).

W trakcie pracy część funkcjonalności przewidzianych pierwotnie na Sprint 3 powstała wcześniej. W backlogu zachowałam ich planowane przypisanie do S3; odrębnie zaznaczam wcześniejsze przygotowanie kodu i testów.

## 2. Realizacja planowanych funkcji

| ID | Co wykonałam | Czym mogę potwierdzić |
|---|---|---|
| DATA-01 | Podłączyłam lokalną bazę SQLite przez Entity Framework Core i zapis szyfrogramów | Kod `VaultRepository`, test zachowania wpisów po ponownym otwarciu |
| US-01 | Dodałam formularz zapisywania konta | Widok WPF i testy repozytorium |
| US-02 | Dodałam edycję istniejących rekordów | Testy edycji i ponownego odczytu |
| US-03 | Dodałam usuwanie wskazanego wpisu | Test, który sprawdza pozostawienie pozostałych rekordów |
| US-04 | Przygotowałam listę kont oraz wyszukiwanie; poprawiłam jego działanie | Interfejs i zmiana w kodzie, pełny scenariusz ręczny nadal do wykonania |
| US-05 | Dodałam generator haseł z walidacją długości | Testy poprawnej i niepoprawnej długości |
| US-06 | Dodałam reset lokalnego sejfu i potwierdzenie w UI | Test resetu; ręczne sprawdzenie anulowania pozostaje w planie demonstracji |
| SEC-01 / SEC-02 | Dokończyłam tworzenie i odblokowanie sejfu | Testy dobrego i złego hasła, działające okno WPF |
| CI-01 | Poprawiłam automatyczną kompilację, uruchamianie testów i formatowanie | Zielone przebiegi GitHub Actions na `main` |

Zrealizowałam też eksport i import zaszyfrowanej kopii (`PORT-01`, `PORT-02`) oraz testy automatyczne (`TEST-01`) przed planowanym Sprintem 3. Nie zastępuje to pełnego testu ręcznego eksportu i importu, zapisanego w `TEST-02`.

## 3. Przyrost, który mam 26 listopada

Aplikacja działa na Windows jako WPF w .NET 10. Mogę utworzyć sejf i odblokować go hasłem master, zarządzać wpisami, wyszukać konto i wygenerować hasło. Dane trafiają do lokalnego SQLite w postaci zaszyfrowanej. Dodałam również eksport do `.pvault`, import oraz przycisk resetu z potwierdzeniem. To jeden rozbudowywany program, a nie oddzielny projekt na każdy sprint.

Interfejs pozostawiłam w prostym modelu XAML + obsługa zdarzeń w `MainWindow.xaml.cs`. Logika danych i kryptografii znajduje się w odrębnych projektach `Core` i `Infrastructure`, a testy w `PasswordVault.Tests`.

## 4. Testy i poprawki

- Najnowszy potwierdzony wynik to **25 testów zaliczonych, 0 niepowodzeń, 0 pominiętych**. Testy obejmują szyfrowanie, błędne hasła, modyfikację szyfrogramów, bazę SQLite, eksport/import i reset.
- Początkowo część testów kończyła się błędem asercji przy AES-GCM: test wymagał dokładnie `CryptographicException`, a .NET zgłaszał podtyp `AuthenticationTagMismatchException`. Zmieniłam odpowiednie asercje na `Assert.ThrowsAny<CryptographicException>` i ponownie uruchomiłam zestaw testów.
- Działający workflow `.github/workflows/ci.yml` uruchamia kompilację, testy i kontrolę białych znaków przez `dotnet format whitespace`. Poprzednie czerwone przebiegi zostawiłam w historii, a aktualne wyniki są zielone.

### Poprawa raportowania pokrycia — QUAL-01 (ukończone przed S3)

Analizę SonarQube Cloud połączyłam z GitHub Actions. Początkowo automatyczna analiza obejmowała tylko niewielką część plików; później skaner zaczął uwzględniać kod C#. Następny problem dotyczył pokrycia: same testy przechodziły, ale SonarQube pokazywał `0%`, ponieważ nie dostawał poprawnego raportu.

Podjęłam próbę zbierania pomiaru przez `XPlat Code Coverage`. Log wskazał, że runner nie znajduje tego kolektora. Zmieniłam sposób pomiaru na narzędzie `dotnet-coverage` i raport XML, który SonarQube może odczytać. W potwierdzonym wyniku **Overall Code** otrzymałam **60,0% pokrycia z 425 linii objętych pomiarem** oraz **0,0% duplikacji**. W analizie widnieją oceny A za Security i Reliability, jeden otwarty problem Maintainability i ocena A w tej kategorii.

Nie utożsamiam tych wyników z osobnym statusem Quality Gate, którego nie potwierdziłam na dostarczonym screenie. Zadanie `QUAL-01` zamknęłam jako uruchomienie analizy i poprawnego pomiaru, nie jako bezwarunkowe zaliczenie wszystkich reguł jakości.

### Audyt NuGet i aktualizacja zależności — SEC-03 (ukończone przed S3)

W starszym przebiegu CI występowało ostrzeżenie `NU1903` dotyczące przechodniej zależności `SQLitePCLRaw.lib.e_sqlite3` w wersji `2.1.11`. Sprawdziłam pakiety NuGet i zaktualizowałam zależności. **Po aktualizacji kolejne uruchomienie nie zgłaszało już wcześniejszego ostrzeżenia.** Nie zapisuję tego problemu jako nadal otwartego.

W notatkach technicznych utrzymałam informacje o Argon2id, szyfrowaniu AES-GCM oraz ryzyku braku rotacji klucza i migracji parametrów KDF. To osobne ograniczenia projektu — aktualizacja NuGet nie usuwa ich automatycznie. Kontrola nowych podatności nadal ma znaczenie w przyszłych wersjach.

## 5. Zadania pozostające do potwierdzenia

- [ ] Pełne przejście po interfejsie na fikcyjnych danych: utworzenie i odblokowanie, CRUD, wyszukiwanie i ponowne uruchomienie.
- [ ] Ręczne sprawdzenie resetu z anulowaniem oraz odtworzenia kopii po resecie.
- [ ] Pełny test eksportu i importu, także dla uszkodzonego pliku, bez nadpisania istniejącego sejfu.
- [ ] Demonstracja zapisania i ponownego odczytu 20 fikcyjnych kont (kryterium `AC-14`).
- [ ] Uzupełnienie powiązań między backlogiem CSV a GitHub Projects i sprawdzenie dostępu prowadzącego.

Nie oznaczam tych punktów jako zaliczonych na podstawie samych 25 testów jednostkowych. Plan zakładał sprawdzenie aplikacji także ręcznie.

## 6. Zmiany zakresu i wnioski w trakcie sprintu

W porównaniu z planem rozpoczęłam więcej prac nad kontrolą jakości. Wcześniej niż przewidywałam wdrożyłam eksport, import, reset i testy; część czasu poświęciłam potem na naprawę CI, asercji i konfiguracji SonarQube. Priorytet zachowały działająca wersja lokalna i poprawne odczytywanie zaszyfrowanych danych. Windows Hello, synchronizację chmurową i zmianę bazy pozostawiłam jako rozszerzenia poza MVP.

To jest przegląd stanu **w trakcie** Sprintu 2, a nie końcowa retrospektywa z 29 listopada. Końcowy rezultat i ewentualne zmiany po 26 listopada dopiszę dopiero po jego zakończeniu.

## 7. Dowody

- Test Explorer: 25/25 testów.
- GitHub Actions: zielone kroki build, test i format, osobny przebieg analizy SonarQube.
- SonarQube: zrzut `Overall Code` z pokryciem 60,0%, 425 liniami oraz zrzut listy plików C#.
- Przebiegi kontroli NuGet: wcześniejszy `NU1903` i nowszy log bez tego ostrzeżenia.

Dokładne adresy URL przebiegów mogę dopisać do Issues; w tym dokumencie nie wpisuję niezweryfikowanych linków.
