using DynamicData;
using DynamicData.Alias;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

using ThemedDialog.Core;

namespace ThemedDialog.Designer.ViewModels
{
    public class ThemeManageViewModel : ReactiveObject
    {
        private SourceList<Theme> Themes { get; set; }
        private readonly ReadOnlyObservableCollection<Theme> _searchResults;
        public ReadOnlyObservableCollection<Theme> SearchResults => _searchResults;

        [Reactive]
        public Theme SelectedTheme { get; set; }

        public bool CanEdit { [ObservableAsProperty] get; }
        public bool CanDelete { [ObservableAsProperty] get; }
        public bool CanAdd { [ObservableAsProperty] get; }

        [Reactive]
        public string NewThemeName { get; set; }

        [Reactive]
        public string EditThemeName { get; set; }

        [Reactive]
        public string SearchTerm { get; set; }

        public ThemeManageViewModel(IEnumerable<DialogCharacter> characters, IEnumerable<Theme> themes)
        {
            Themes = new SourceList<Theme>();
            Themes.AddRange(themes);

            var filter = this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .DistinctUntilChanged()
                .Select(BuildFilter);

            Themes.Connect()
                .Filter(filter)
                .Sort(new ThemeComparer())
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _searchResults)
                .DisposeMany()
                .Subscribe();

            this.WhenAnyValue(x => x.NewThemeName)
                .Select(x => !string.IsNullOrEmpty(x))
                .ToPropertyEx(this, x => x.CanAdd);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Where(selected => selected != null)
                .Select(selected => selected.Name)
                .Subscribe(name => EditThemeName = name);

            this.WhenAnyValue(x => x.SelectedTheme, x => x.EditThemeName)
                .Select(x => x.Item1 != null && !string.IsNullOrEmpty(x.Item2?.Trim() ?? string.Empty))
                .ToPropertyEx(this, x => x.CanEdit);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.CanDelete);
        }

        private static Func<Theme, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return theme => true;
            }

            return theme => theme.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        }

        public void Edit()
        {
            Themes.Remove(SelectedTheme);
            Themes.Add(new Theme(EditThemeName));
        }

        public void Add()
        {
            Themes.Add(new Theme(NewThemeName));
        }

        public void Delete()
        {
            Themes.Remove(SelectedTheme);
        }
    }
}