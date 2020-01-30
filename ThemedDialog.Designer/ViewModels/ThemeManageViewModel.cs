using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ThemedDialog.Core;


namespace ThemedDialog.Designer.ViewModels
{
    public class ThemeManageViewModel : ReactiveObject
    {
        public List<DialogCharacter> Characters { get; set; }
        public List<Theme> Themes { get; set; }
        public List<Theme> SearchResults { [ObservableAsProperty] get; }
        [Reactive]
        public Theme SelectedTheme { get; set; }
        public bool CanEdit { [ObservableAsProperty] get; }
        public bool CanDelete { [ObservableAsProperty] get; }
        [Reactive]
        public string NewThemeName { get; set; }
        [Reactive]
        public string EditThemeName { get; set; }
        [Reactive]
        public string SearchTerm { get; set; }

        public ThemeManageViewModel(IEnumerable<DialogCharacter> characters, IEnumerable<Theme> themes)
        {
            Themes = new List<Theme>(themes);
            Characters = new List<DialogCharacter>(characters);
            SearchResults = new List<Theme>(Themes);

            this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Select(term => Search(term).ToList())
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SearchResults);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Where(selected => selected != null)
                .Select(selected => selected.Name)
                .Subscribe(name => EditThemeName = name);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.CanEdit);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.CanDelete);
        }
        private IEnumerable<Theme> Search(string term)
        {
            if (term == null)
            {
                term = string.Empty;
            }
            var themes = Themes.Where(t => t.Name.ToUpper().Contains(term.ToUpper()));
            return themes;
        }

        public void Edit()
        {
            var theme = Themes.Where(t => t.Name.Equals(SelectedTheme.Name)).Single();
            theme.Name = EditThemeName;
            SearchTerm = SearchTerm + string.Empty;
        }
    }
}