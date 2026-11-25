# Scenariusz krótkiego demo

1. Uruchamiam aplikację (`F5`) na Windows 11.
2. Tworzę testowy sejf z wymyślonym hasłem master.
3. Dodaję przykładowe konto: `PrzykladowaStrona`, `test@example.org`, losowe hasło.
4. Zmieniam hasło we wpisie, a następnie wyszukuję nazwę serwisu.
5. Zamykam aplikację i uruchamiam ją ponownie.
6. Sprawdzam, czy złe hasło nie otwiera sejfu, potem wpisuję prawidłowe.
7. Eksportuję sejf do pliku `.pvault` w wybranym folderze.
8. Importuję plik do **pustego środowiska testowego** albo robię kopię obecnej bazy przed nadpisaniem.
9. Pokazuję, że wpis nadal można odczytać.
10. Jeśli mam już przetestowaną poprawkę resetowania, pokazuję okno potwierdzenia i wybieram „Nie”. Pełny reset robię tylko na danych testowych, po wykonaniu eksportu.
11. Otwieram Test Explorer i wynik GitHub Actions.

## Co warto sfotografować

Ekran WPF, dodany wpis (bez prawdziwego hasła), testy xUnit, zielone CI, tablicę Kanban, listę sprintów i plik eksportu. Nie pokazuję rzeczywistych haseł.
