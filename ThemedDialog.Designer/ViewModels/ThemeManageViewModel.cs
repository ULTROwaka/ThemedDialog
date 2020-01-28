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
        public List<Theme> SearchResults { [ObservableAsProperty] get;  }
        [Reactive]
        public Theme SelectedTheme { get; set; }
        public bool CanEdit { [ObservableAsProperty] get; }
        public bool CanDelete { [ObservableAsProperty] get; }
        [Reactive]
        public string NewThemeName { get; set; }
        [Reactive]
        public string EditThemeName { get; set; }
        [Reactive]
        public string SerchTerm { get; set; }

        public ThemeManageViewModel(IEnumerable<DialogCharacter> characters, IEnumerable<Theme> themes)
        {
            Themes = new List<Theme>(themes);
            Characters = new List<DialogCharacter>(characters);
            SearchResults = new List<Theme>(Themes);

            this.WhenAnyValue(x => x.SerchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Select(term => Search(term).ToList())
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SearchResults);

          /*this.WhenAnyValue(x => x.SerchTerm)
                .Select(term => term?.Trim())
                .Throttle(TimeSpan.FromMilliseconds(800))
                .DistinctUntilChanged()
                .Where(term => !string.IsNullOrWhiteSpace(term))
                .SelectMany(Search)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx<ThemeManageViewModel, IEnumerable<Theme>>(source: this, property: x => x.SearchResults);*/
               

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.CanEdit);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.CanDelete);
        }
        private IEnumerable<Theme> Search(string term)
        {
            if(term == null)
            {
                term = string.Empty;
            }
            var themes = Themes.Where(t => t.Name.ToUpper().Contains(term.ToUpper()));
            return themes;
        }
    }
}