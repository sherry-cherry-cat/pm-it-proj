# Raport końcowy – PasswordVault

**Przedmiot:** Zarządzanie projektami IT  
**Forma:** praca indywidualna  
**Repozytorium:** [link]  
**Data:** [dzień oddania]

## 1. Co zrobiłam
Przygotowałam desktopowy menedżer haseł dla Windows w C# i WPF. Wpisy przechowuję w lokalnej bazie SQLite. Do odblokowania sejfu używam hasła master, a same wpisy są szyfrowane. Projekt miał być nieduży, żebym mogła skupić się też na zarządzaniu zadaniami i testach.

## 2. Narzędzia i sposób pracy
Pracowałam sama. Korzystałam z Visual Studio 2026, Gita i GitHuba. Testy napisałam w xUnit, a do automatycznej kontroli użyłam GitHub Actions.

## 3. Backlog i wymagania
Zadania rozpisałam w `BACKLOG.csv`, razem z priorytetami i kryteriami akceptacji. Użyłam trzech sprintów. Windows Hello i synchronizację z chmurą zostawiłam poza zakresem, żeby zdążyć z podstawową wersją.

[WSTAW: screenshot backlogu albo tablicy Kanban]

## 4. Sprinty
- **Sprint 1 (1–15 listopada):** przygotowałam szkielet WPF i pierwszą dokumentację. Nie zamknęłam wszystkich zaplanowanych zadań, bo zakres okazał się zbyt duży.
- **Sprint 2 (16–29 listopada):** [wpiszę, co rzeczywiście skończyłam; przydatne są linki do commitów].
- **Sprint 3 (30 listopada – 13 grudnia):** [uzupełnię po zakończeniu].

## 5. Ryzyka
Najbardziej obawiałam się opóźnień, błędów w szyfrowaniu i utraty danych. Dlatego ograniczyłam funkcje dodatkowe i przygotowałam testy obejmujące błędne hasło oraz nieudany import.

[WSTAW: 3–5 ryzyk z `RISK_REGISTER.csv` i napisz, czy coś faktycznie się wydarzyło]

## 6. Testy i GitHub Actions
- Test Explorer: [wynik / data / screenshot].
- Build, testy i lint w GitHub Actions: [link / wynik / screenshot].
- SonarQube Cloud (jeśli podłączyłam): [wynik Quality Gate / screenshot].

## 7. Demo
[WSTAW: screenshot aplikacji i krótki opis tworzenia, odblokowania oraz eksportu sejfu. Używam tylko fikcyjnych danych.]

## 8. Wnioski
Największym problemem było zaplanowanie zbyt wielu rzeczy na pierwszy sprint. Następnym razem wcześniej uruchomiłabym automatyczne testy i mniej czasu poświęcała na dodatkowe pomysły przed sprawdzeniem podstawowych funkcji.

[Dopisz 2–3 własne obserwacje po testach i zakończeniu projektu.]
