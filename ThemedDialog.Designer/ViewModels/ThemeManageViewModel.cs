﻿using DynamicData;
using DynamicData.Alias;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

using ThemedDialog.Core;
using ThemedDialog.Designer.Repositories;
using ThemedDialog.Designer.ViewModels.Proxy;

namespace ThemedDialog.Designer.ViewModels
{
    internal class ThemeManageViewModel : ReactiveObject
    {
        private readonly Repository _repository;

        private readonly ReadOnlyObservableCollection<ProxyTheme> _searchResults;      
        public ReadOnlyObservableCollection<ProxyTheme> SearchResults => _searchResults;
        [Reactive]
        internal ProxyTheme SelectedTheme { get; set; }
        internal bool CanEdit { [ObservableAsProperty] get; }
        internal bool CanDelete { [ObservableAsProperty] get; }
        internal bool CanAdd { [ObservableAsProperty] get; }

        [Reactive]
        internal string NewThemeName { get; set; }

        [Reactive]
        internal string EditThemeName { get; set; }

        [Reactive]
        internal string SearchTerm { get; set; }

        internal ThemeManageViewModel(Repository repository)
        {
            _repository = repository;

            var filter = this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .DistinctUntilChanged()
                .Select(BuildFilter);

            _repository.Themes
                .Filter(filter)
                .Sort(new ThemeComparer())
                .ObserveOn(RxApp.MainThreadScheduler)
                .Transform(x => new ProxyTheme(x))
                .AutoRefresh()
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

        internal void Edit()
        {
            SelectedTheme.Name = EditThemeName;
            /*var original = SelectedTheme.ExtractModel();
            var newItem = new Theme(NewThemeName);

            _repository.Edit(original, newItem);*/
        }

        internal void Add()
        {
            _repository.Add(new Theme(NewThemeName));
        }

        internal void Delete()
        {
            _repository.Remove(SelectedTheme.ExtractModel());
        }
    }
}