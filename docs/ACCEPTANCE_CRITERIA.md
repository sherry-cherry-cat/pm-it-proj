# Kryteria akceptacji MVP

- **AC-01:** Przy pierwszym uruchomieniu można utworzyć sejf z hasłem master długości minimum 12 znaków.
- **AC-02:** Po zamknięciu i ponownym uruchomieniu poprawne hasło odblokowuje sejf.
- **AC-03:** Niepoprawne hasło lub zmieniony tag AES-GCM nie ujawniają danych.
- **AC-04:** Dodany wpis pojawia się na liście i da się go ponownie odczytać.
- **AC-05:** Edycja oraz usunięcie wybranego wpisu są zapisywane w bazie.
- **AC-06:** Po ponownym uruchomieniu działa odczyt co najmniej 20 testowych wpisów.
- **AC-07:** W bazie SQLite nie ma jawnych loginów, nazw serwisów ani haseł.
- **AC-08:** Hasło w formularzu jest domyślnie zakryte.
- **AC-09:** Generator tworzy hasło długości 20 znaków i odrzuca niepoprawną długość.
- **AC-10:** Eksport zapisuje plik w lokalizacji wybranej w oknie zapisu.
- **AC-11:** Zaimportowany plik odblokowuje się tym samym hasłem master.
- **AC-12:** Nieudany import nie nadpisuje istniejących danych.

W raporcie końcowym zaznaczę tylko kryteria, które naprawdę sprawdziłam (automatycznie lub ręcznie).
