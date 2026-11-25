# SonarQube / SonarCloud — jeśli wystarczy czasu

Prowadzący wymienia SonarQube jako element na ocenę 5. Nie wpisywałam fikcyjnego wyniku i nie dodawałam nieskonfigurowanego workflow, który automatycznie zepsułby CI.

Jeśli chcę spróbować:

1. Zakładam projekt na SonarQube Cloud albo używam serwera uczelni.
2. Sprawdzam dostępność analizy prywatnego repozytorium i limit planu.
3. Generuję token i dodaję go do repozytorium przez `Settings → Secrets and variables → Actions` jako `SONAR_TOKEN`.
4. Korzystam z instrukcji **SonarScanner for .NET** i wpisuję prawdziwy klucz projektu i adres serwera.
5. W raporcie zamieszczam tylko faktycznie uzyskany wynik analizy.

Źródło: https://docs.sonarsource.com/sonarqube-cloud/advanced-setup/ci-based-analysis/sonarscanner-for-dotnet/introduction/
