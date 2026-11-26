# Scenariusz demonstracji — do wykonania na fikcyjnych danych

To jest instrukcja przyszłego testu ręcznego, a nie raport z już wykonanego pomiaru.

1. Utworzę lub otworzę testowy sejf (hasło master co najmniej 12 znaków).
2. Sprawdzę, czy niepoprawne hasło master zostanie odrzucone.
3. Wprowadzę 20 fikcyjnych rekordów, m.in. z różnymi nazwami serwisów.
4. Wyszukam wybrany wpis, wyedytuję jego dane i usunę jeden rekord bez usuwania pozostałych.
5. Skorzystam z generatora hasła i sprawdzę ukrywanie wartości hasła.
6. Zamknę aplikację, uruchomię ją ponownie i odblokuję sejf, aby zweryfikować trwałość wpisów.
7. Wyeksportuję kopię do pliku `.pvault` w kontrolowanej lokalizacji.
8. Sprawdzę anulowanie resetu, następnie wykonam reset po wcześniejszym zachowaniu kopii.
9. Zaimportuję prawidłowy plik i sprawdzę odtworzenie wpisów.
10. Spróbuję importu błędnego lub uszkodzonego pliku i sprawdzę, czy istniejące dane pozostają nienaruszone.
11. W zapisanym wyniku odnotuję datę, system, wynik każdego kroku i ewentualne błędy.

Jednostkowy wynik 25/25 nie oznacza automatycznego przejścia tego scenariusza. Do wykazania kryterium `AC-14` potrzebuję osobnego testu 20 wpisów.
