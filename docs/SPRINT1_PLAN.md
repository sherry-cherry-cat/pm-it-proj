# Sprint 1 (1–15 listopada 2026)

## Cel
Przygotować projekt i podstawy pod aplikację: działający WPF, oddzielona logika, pierwsze testy szyfrowania oraz automatyczne sprawdzanie kodu na GitHubie.

## Zadania
- [ ] DOC-01 — uporządkować dokumentację, backlog i ryzyka.
- [ ] TECH-01 — sprawdzić repozytorium i `.gitignore`.
- [ ] TECH-02 — dodać projekty Core, Infrastructure i Tests do WPF.
- [ ] SEC-01 — wygenerować DEK i zaszyfrować go przy użyciu KEK z hasła master.
- [ ] SEC-02 — dodać AES-GCM i test błędnego hasła / uszkodzonych danych.
- [ ] SEC-03 — opisać i serializować format pliku sejfu v1.
- [ ] TECH-03 — uruchomić CI na GitHubie.

## Kiedy uznam zadania za skończone?
- Rozwiązanie otwiera się i buduje w Visual Studio 2026.
- Testy xUnit są widoczne w Test Explorerze i przechodzą.
- Pull Request pokazuje zielony wynik CI.
- Dokumentacja opisuje PasswordVault i zgadza się z backlogiem.
- W PR nie ma danych użytkownika ani plików `bin` i `obj`.

WPF ma na tym etapie tylko okno testowe. Operacje na wpisach i SQLite są zaplanowane na Sprint 2.

**Uwaga:** to jest plan, a nie potwierdzenie wykonania. Stan prac zapisuję w `docs/SPRINT1_REVIEW.md` po sprawdzeniu testów i PR.
