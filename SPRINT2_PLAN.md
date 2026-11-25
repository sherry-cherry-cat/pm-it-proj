# Sprint 2 – zapis danych i interfejs (16–29.11.2026)

## Co zaplanowałam
Chcę mieć prostą aplikację, w której mogę utworzyć sejf, zapisać konto i odczytać je po ponownym uruchomieniu programu.

| Zadanie | Priorytet | Od czego zależy |
|---|---|---|
| Zapis do SQLite (DATA-01) | wysoki | mechanizm szyfrowania |
| Dodawanie i edycja kont (US-01, US-02) | wysoki | baza i model wpisu |
| Usuwanie i wyszukiwanie (US-03, US-04) | wysoki | lista wpisów |
| Generator hasła (US-05) | średni | interfejs formularza |
| Reset lokalnego sejfu (US-06) | średni | zapis danych i ekran startowy |

## Co mam na 25 listopada
- Mam wersję kodu WPF z obsługą sejfu i wpisów. Projekt buduje się w Visual Studio.
- Przygotowałam również poprawkę wyszukiwania i poprawkę resetowania, ale ich finalne działanie muszę jeszcze sprawdzić po wgraniu.
- Nie mam jeszcze potwierdzonych wyników wszystkich testów ani zielonego CI.

## Do zrobienia
- [ ] Uruchomić aplikację i sprawdzić dodanie, edycję, usunięcie i wyszukiwanie na fikcyjnych kontach.
- [ ] Zamknąć aplikację, uruchomić ją ponownie i sprawdzić zapisane wpisy.
- [ ] Sprawdzić błędne hasło master i reset (w tym przycisk „Nie”).
- [ ] Uruchomić testy w Test Explorerze i sprawdzić GitHub Actions.
- [ ] Zapisać wyniki i ewentualne błędy w `docs/SPRINT2_REVIEW.md`.

## Kiedy uznam sprint za skończony
Wpisy pozostają po ponownym otwarciu aplikacji, a podstawowe operacje działają bez utraty pozostałych danych. Testy nie muszą być idealne, ale ich faktyczny wynik musi być zapisany i błędy muszą mieć zadania do poprawy.
