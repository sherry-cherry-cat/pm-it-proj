# Co sprawdzę po wgraniu aktualizacji dokumentacji

1. [ ] W `Git Changes` upewnię się, że zmieniły się tylko pliki dokumentacyjne; nie nadpisałam `.cs`, `.csproj` ani workflow z działającym CI.
2. [ ] Potwierdzę, że `SPRINT1_PLAN.md` i `SPRINT2_PLAN.md` zachowują pierwotne treści; rzeczywiste wyniki znajdują się w `docs/SPRINT1_REVIEW.md` i `docs/SPRINT2_REVIEW.md`.
3. [ ] Jeśli w repozytorium istnieje `docs/SPRINT1_PLAN.md`, zostawię przekierowanie z tej paczki albo usunę niepotrzebny duplikat po weryfikacji.
4. [ ] Jeśli nadal istnieje przykładowy `tests/test_app.py` z Pythona, usunę go oddzielnym commitem — nie jest częścią PasswordVault.
5. [ ] Zaktualizuję GitHub Issues i Projects, bo skopiowanie CSV nie aktualizuje tablicy automatycznie.
6. [ ] Sprawdzę, czy GitHub Actions po commicie dokumentacyjnym pozostaje zielony.
7. [ ] Uzupełnię linki do konkretnego przebiegu CI i dashboardu SonarQube, jeśli będę je publikować.
8. [ ] Zweryfikuję dostęp prowadzącego i wymaganie prywatnego repozytorium.
9. [ ] Nie zamknę Sprintu 3 przed jego terminem; raport PDF i review dopracuję na podstawie rzeczywistych wyników.

## Dane potwierdzone na 26.11

25/25 xUnit, zielone CI, SonarQube 60,0% z 425 linii, jeden problem Maintainability oraz brak wcześniejszego NU1903 po aktualizacji pakietów. Quality Gate pozostaje do osobnego sprawdzenia.
