# Definition of Ready i Definition of Done

## Definition of Ready (DoR)

Zadanie jest gotowe do rozpoczęcia, kiedy znam jego użytkową lub techniczną wartość, mam określony priorytet i sprint, zidentyfikowałam zależności i zapisałam możliwy do sprawdzenia wynik. Dla funkcji aplikacji opisuję User Story i kryteria akceptacji; dla zadań technicznych wskazuję konkretną kontrolę (np. zielony CI lub raport pokrycia).

## Definition of Done (DoD)

- Zmiana w kodzie została zapisana w Git, rozwiązanie się buduje, a odpowiednie testy przechodzą.
- W razie zmiany UI sprawdziłam wynik ręcznie; jeśli nie miałam na to czasu, nie opisuję tego jako zakończonego testu interfejsu.
- Wynik automatycznej kontroli jest dostępny w GitHub Actions i nie ukrywa niepowodzeń.
- Kryteria akceptacji istotne dla danego zadania są potwierdzone, a status w backlogu odpowiada rzeczywistej pracy.
- Dokumentacja zawiera znane ograniczenia i ryzyka. Dla zmian w konfiguracji jakości wymagany jest widoczny rezultat w SonarQube albo logach.

## Rozróżnienie wyników

`TEST-01` uznaję za ukończone dzięki 25 zaliczonym testom. `QUAL-01` jest ukończone, bo działają analiza i pomiar 60,0%. `SEC-03` jest ukończone jako kontrola zależności i udokumentowanie ograniczeń KDF, bo wcześniejsze NU1903 zniknęło. `TEST-02` nadal wymaga ręcznego scenariusza 20 kont. To odrębne zadania i ich rezultaty nie zastępują się wzajemnie.
