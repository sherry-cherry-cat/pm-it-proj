# Sprint 1 — plan bazowy (01.11–15.11.2026)

**Dokument opracowano:** 2026-11-02. 

## Cel
Uruchomić proces pracy i uzyskać minimalny, testowalny szkielet aplikacji WPF wraz z samodzielnym serwisem kryptografii, który generuje losowy DEK, opakowuje go kluczem pochodzącym z hasła master i odrzuca nieprawidłowe hasła lub uszkodzony ciphertext.

## Wybrane pozycje backlogu
DOC-01, TECH-01, TECH-02, SEC-01, SEC-02, SEC-03, TECH-03.

## Kolejność zadań
1. Sprawdzić stan lokalnego forka (szczególnie `chore/bootstrap-dotnet`), skopiować folder, zaktualizować pliki dokumentacji i `.gitignore` na gałęzi `docs/*`.
2. Założyć Issues, milestones i GitHub Project, skonfigurować PR oraz działające CI dokumentacji.
3. Utworzyć `PasswordVault.sln` lub `.slnx` i projekty `PasswordVault.App` (WPF), `PasswordVault.Core` (modele/interfejsy), `PasswordVault.Infrastructure` (krypto/persystencja), `PasswordVault.Tests` (xUnit).
4. Przetestować wersję Argon2id, zdefiniować format nagłówka sejfu i testy poprawnego/błędnego master oraz modyfikacji szyfrogramu.
5. Włączyć prawdziwy workflow .NET build/test. Nie mieszać statusu docs CI z sukcesem testów kodu.

## Definition of Done dla sprintu
- Dokumentacja zgodna z kartą projektu i aktualizowanym backlogiem.
- Minimalne rozwiązanie kompiluje się na Windows z `dotnet build` (jeśli implementacja zakończona), testy krypto przechodzą; jeśli nie, oznaczyć zadania jako Carry-over, nie jako Done.
- CI .NET jest skonfigurowane i uruchomione na kodzie

## Ryzyka / plan awaryjny
Najpierw kryptografia i budowanie rozwiązania, później kosmetyka UI. Jeśli czas się kończy, zredukuje dodatkowe style UI i Windows Hello, nie rezygnując z testów integralności. 
