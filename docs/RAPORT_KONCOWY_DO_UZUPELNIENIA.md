# Raport końcowy — PasswordVault

**Przedmiot:** Zarządzanie projektami IT  
**Forma:** projekt indywidualny  
**Repozytorium:** [wstaw link do własnego repozytorium]  
**Data oddania:** [wstaw faktyczną datę]

## 1. Opis projektu

Zrobiłam prosty menedżer haseł na Windows w C# i WPF. Aplikacja przechowuje lokalny sejf w SQLite. Hasło master pozwala odblokować klucz danych, a same wpisy są szyfrowane przez AES-GCM. Zależało mi głównie na pokazaniu pracy z backlogiem, podziałem na sprinty i automatycznymi kontrolami jakości.

## 2. Zespół i narzędzia

Projekt realizowałam samodzielnie. Do kodu używałam Visual Studio 2026, do wersjonowania GitHuba, a do testów xUnit i GitHub Actions.

## 3. Backlog i wymagania

Zadania są w `BACKLOG.csv`. Podzieliłam je na trzy sprinty, z przypisanymi historyjkami użytkownika i kryteriami akceptacji. Funkcje takie jak Windows Hello i automatyczna synchronizacja zostały poza MVP ze względu na czas.

[WSTAW: zrzut wybranej części backlogu lub tablicy Kanban]

## 4. Realizacja sprintów

- **Sprint 1 (1–15.11):** szkielet WPF, projekt kryptografii, dokumentacja i konfiguracja CI. [Uzupełnij faktycznie zakończone zadania]
- **Sprint 2 (16–29.11):** SQLite, wpisy i interfejs. [Uzupełnij faktycznie zakończone zadania]
- **Sprint 3 (30.11–13.12):** eksport/import, testy i demonstracja. [Uzupełnij faktycznie zakończone zadania]

[WSTAW: krótkie podsumowanie opóźnień i przeniesionych zadań, jeśli wystąpiły]

## 5. Ryzyka

Największe ryzyka dotyczyły czasu, poprawności szyfrowania i utraty danych. Ograniczyłam zakres funkcjonalny do MVP, dodałam testy negatywne i sprawdzenie importu przed nadpisaniem bazy.

[WSTAW: tabelę 3–5 głównych ryzyk z RISK_REGISTER.csv]

## 6. Testy i CI/CD

[WSTAW: rzeczywistą liczbę testów przechodzących w Test Explorer, datę i wynik]

[WSTAW: screen GitHub Actions z build, test i format — dopiero po wykonaniu]

[Opcjonalnie: screen analizy SonarQube, jeśli udało się ją uruchomić]

## 7. Demo

[WSTAW: screen tworzenia lub odblokowania sejfu]

[WSTAW: screen listy wpisów i eksportu/importu; tylko dane testowe]

## 8. Wnioski

W czasie pracy zobaczyłam, że duży zakres jest trudny do utrzymania w krótkich sprintach. Najbardziej pomogło mi rozdzielenie podstawowych funkcji od dodatków oraz uruchamianie testów po kolejnych zmianach. [Dopisz konkretne problemy, które rzeczywiście miałaś, i sposób ich rozwiązania.]
