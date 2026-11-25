using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using PasswordVault.Core;
using PasswordVault.Infrastructure;

namespace Password_Vault;

public partial class MainWindow : Window
{
    private readonly VaultRepository _store = new(VaultRepository.DefaultPath);
    private byte[]? _dek;
    private List<VaultEntry> _entries = new();
    private string? _selectedId;
    private bool _refreshing;

    public MainWindow()
    {
        InitializeComponent();
        ShowLocked();
    }

    private void ShowLocked()
    {
        LockSession();
        bool hasVault = _store.Exists();
        LockedPanel.Visibility = Visibility.Visible;
        VaultPanel.Visibility = Visibility.Collapsed;
        LockTitle.Text = hasVault ? "Otwórz sejf" : "Utwórz nowy sejf";
        LockHelp.Text = hasVault
            ? "Wpisz hasło master, aby odblokować zapisane konta."
            : "To pierwsze uruchomienie. Wybierz hasło master (minimum 12 znaków).";
        CreateButton.Visibility = hasVault ? Visibility.Collapsed : Visibility.Visible;
        UnlockButton.Visibility = hasVault ? Visibility.Visible : Visibility.Collapsed;
        ConfirmRow.Visibility = hasVault ? Visibility.Collapsed : Visibility.Visible;
        ResetLockedButton.Visibility = hasVault ? Visibility.Visible : Visibility.Collapsed;
        MasterInput.Clear();
        ConfirmInput.Clear();
        StatusText.Text = hasVault ? "Sejf zablokowany" : "Nie znaleziono sejfu";
    }

    private void ShowOpen()
    {
        LockedPanel.Visibility = Visibility.Collapsed;
        VaultPanel.Visibility = Visibility.Visible;
        RefreshEntries();
        ClearForm();
    }

    private void Create_Click(object sender, RoutedEventArgs e)
    {
        if (MasterInput.Password != ConfirmInput.Password)
        {
            MessageBox.Show("Hasła nie są takie same.", "PasswordVault");
            return;
        }
        RunAction(() =>
        {
            _dek = _store.Create(MasterInput.Password);
            ShowOpen();
            StatusText.Text = "Utworzono pusty sejf.";
        });
    }

    private void Unlock_Click(object sender, RoutedEventArgs e)
    {
        RunAction(() =>
        {
            _dek = _store.Unlock(MasterInput.Password);
            ShowOpen();
            StatusText.Text = "Sejf został odblokowany.";
        });
    }

    private void RefreshEntries()
    {
        // Po zapisie, usunięciu lub odblokowaniu odczytuję dane z bazy tylko raz.
        _entries = _store.GetEntries(RequireKey());
        FilterEntries();
    }

    private void FilterEntries()
    {
        // Wyszukiwanie korzysta z listy w pamięci, bez odczytu i deszyfrowania bazy.
        string search = SearchInput.Text.Trim();
        _refreshing = true;
        try
        {
            EntriesList.ItemsSource = _entries.Where(e =>
                e.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
        finally
        {
            _refreshing = false;
        }
        StatusText.Text = $"Wpisów: {_entries.Count}";
    }

    private void Search_Changed(object sender, TextChangedEventArgs e)
    {
        if (_dek is not null && !_refreshing) FilterEntries();
    }

    private void Entries_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_refreshing || EntriesList.SelectedItem is not VaultEntry entry) return;
        _selectedId = entry.Id;
        TitleInput.Text = entry.Title;
        UrlInput.Text = entry.Url;
        UsernameInput.Text = entry.Username;
        EntryPasswordInput.Password = entry.Password;
        VisiblePasswordInput.Text = entry.Password;
        NotesInput.Text = entry.Notes;
        SetPasswordVisibility(false);
    }

    private void New_Click(object sender, RoutedEventArgs e) => ClearForm();

    private void ClearForm()
    {
        _selectedId = null;
        EntriesList.SelectedItem = null;
        TitleInput.Clear();
        UrlInput.Clear();
        UsernameInput.Clear();
        EntryPasswordInput.Clear();
        VisiblePasswordInput.Clear();
        NotesInput.Clear();
        SetPasswordVisibility(false);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        RunAction(() =>
        {
            var entry = new VaultEntry
            {
                Id = _selectedId ?? Guid.NewGuid().ToString("N"),
                Title = TitleInput.Text.Trim(),
                Url = UrlInput.Text.Trim(),
                Username = UsernameInput.Text.Trim(),
                Password = CurrentPassword(),
                Notes = NotesInput.Text
            };
            _store.Save(entry, RequireKey());
            RefreshEntries();
            ClearForm();
            StatusText.Text = "Zapisano wpis.";
        });
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedId is null) return;
        if (MessageBox.Show("Usunąć zaznaczony wpis?", "PasswordVault",
            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
        RunAction(() =>
        {
            _store.Delete(_selectedId);
            RefreshEntries();
            ClearForm();
            StatusText.Text = "Usunięto wpis.";
        });
    }

    private void Generate_Click(object sender, RoutedEventArgs e)
    {
        string password = PasswordGenerator.Generate();
        EntryPasswordInput.Password = password;
        VisiblePasswordInput.Text = password;
        StatusText.Text = "Wygenerowano hasło (20 znaków).";
    }

    private void Copy_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(CurrentPassword()))
        {
            Clipboard.SetText(CurrentPassword());
            StatusText.Text = "Skopiowano hasło. Pamiętaj, że pozostaje w schowku.";
        }
    }

    private void ShowPassword_Click(object sender, RoutedEventArgs e)
    {
        SetPasswordVisibility(ShowPasswordBox.IsChecked == true);
    }

    private void SetPasswordVisibility(bool visible)
    {
        if (visible)
        {
            VisiblePasswordInput.Text = EntryPasswordInput.Password;
        }
        else
        {
            EntryPasswordInput.Password = VisiblePasswordInput.Visibility == Visibility.Visible
                ? VisiblePasswordInput.Text : EntryPasswordInput.Password;
        }
        ShowPasswordBox.IsChecked = visible;
        VisiblePasswordInput.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        EntryPasswordInput.Visibility = visible ? Visibility.Collapsed : Visibility.Visible;
    }

    private string CurrentPassword() => ShowPasswordBox.IsChecked == true
        ? VisiblePasswordInput.Text : EntryPasswordInput.Password;

    private void Export_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Title = "Eksportuj zaszyfrowany sejf",
            Filter = "Sejf PasswordVault (*.pvault)|*.pvault",
            FileName = "moj-sejf.pvault",
            OverwritePrompt = true
        };
        if (dialog.ShowDialog(this) != true) return;
        RunAction(() =>
        {
            _store.Export(dialog.FileName);
            StatusText.Text = "Zapisano zaszyfrowaną kopię sejfu.";
        });
    }

    private void Import_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(MasterInput.Password))
        {
            MessageBox.Show("Najpierw wpisz hasło master użyte do eksportu.", "PasswordVault");
            return;
        }
        var dialog = new OpenFileDialog
        {
            Title = "Wybierz zaszyfrowany sejf",
            Filter = "Sejf PasswordVault (*.pvault)|*.pvault"
        };
        if (dialog.ShowDialog(this) != true) return;
        if (_store.Exists() && MessageBox.Show(
            "Import zastąpi obecny sejf. Masz jego kopię?", "PasswordVault",
            MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
        RunAction(() =>
        {
            string password = MasterInput.Password;
            _store.Import(dialog.FileName, password);
            _dek = _store.Unlock(password);
            ShowOpen();
            StatusText.Text = "Zaimportowano sejf.";
        });
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        const string warning =
            "Czy na pewno chcesz zresetować menedżer haseł?\n\n" +
            "Wszystkie lokalnie zapisane konta i obecne hasło master zostaną usunięte. " +
            "Bez wcześniej wyeksportowanej kopii sejfu nie będzie można odzyskać tych danych.\n\n" +
            "Pliki .pvault, które zostały wcześniej wyeksportowane, pozostaną bez zmian.";

        if (MessageBox.Show(warning, "Potwierdzenie resetowania",
                MessageBoxButton.YesNo, MessageBoxImage.Warning,
                MessageBoxResult.No) != MessageBoxResult.Yes)
        {
            return;
        }

        RunAction(() =>
        {
            _store.Reset();
            ShowLocked(); // Czyści klucz DEK z pamięci i pokazuje tworzenie nowego sejfu.
            StatusText.Text = "Sejf zresetowany. Możesz utworzyć nowy.";
        });
    }

    private void Lock_Click(object sender, RoutedEventArgs e) => ShowLocked();

    private byte[] RequireKey() => _dek ?? throw new InvalidOperationException("Sejf jest zablokowany.");

    private void LockSession()
    {
        if (_dek is not null)
        {
            CryptographicOperations.ZeroMemory(_dek);
            _dek = null;
        }
        _entries.Clear();
        _selectedId = null;
        EntriesList.ItemsSource = null;
        EntryPasswordInput.Clear();
        VisiblePasswordInput.Clear();
    }

    private void Window_Closing(object? sender, CancelEventArgs e) => LockSession();

    private static void RunAction(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex is CryptographicException ?
                "Nieprawidłowe hasło albo uszkodzone dane." : ex.Message,
                "PasswordVault", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
