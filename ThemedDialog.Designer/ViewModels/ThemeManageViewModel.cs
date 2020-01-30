using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ThemedDialog.Core;
using Windows.UI.Xaml;

namespace ThemedDialog.Designer.ViewModels
{
    public class ThemeManageViewModel : ReactiveObject
    {
        private readonly SourceList<Theme> Themes;
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
            SearchResults = new List<Theme>();
            Themes = new SourceList<Theme>();
            /*Themes.Connect()
                  .Filter(t => t.Name.ToUpper().Contains(SearchTerm?.ToUpper() ?? string.Empty))                
                  .ObserveOnDispatcher()
                  .Bind(SearchResults)
                  .Subscribe();*/
            Themes.AddRange(themes);

            this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Select(term => Search(term).ToList())
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SearchResults);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Where(theme => theme != null)
                .Select(theme => theme.Name)
                .Subscribe(name => EditThemeName = name);

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(theme => theme != null)
                .ToPropertyEx(this, x => x.CanEdit);             

            this.WhenAnyValue(x => x.SelectedTheme)
                .Select(theme => theme != null)
                .ToPropertyEx(this, x => x.CanDelete);
        }

        private IEnumerable<Theme> Search(string term)
        {
            var result = SearchResults.Where(t => t.Name.ToUpper().Contains(term?.ToUpper() ?? string.Empty));
            return result;
        }

        public void Edit()
        {         
            Themes.Replace(SelectedTheme, new Theme(EditThemeName));
        }
    }
}