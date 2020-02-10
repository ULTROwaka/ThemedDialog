using DynamicData;

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
using Windows.Networking.Sockets;

namespace ThemedDialog.Designer.ViewModels
{
    internal class VariablesManageViewModel : ReactiveObject
    {
        private readonly Repository _repository;
        private readonly ReadOnlyObservableCollection<ProxyVariable> _searchResults;
        public ReadOnlyObservableCollection<ProxyVariable> SearchResults => _searchResults;
       
        [Reactive]
        public List<TypeCode> Types { get; set; }

        internal bool CanEdit { [ObservableAsProperty] get; }
        internal bool CanDelete { [ObservableAsProperty] get; }
        internal bool CanAdd { [ObservableAsProperty] get; }

        [Reactive]
        internal ProxyVariable SelectedVariable { get; set; }

        [Reactive]
        internal string NewVariableName { get; set; }

        [Reactive]
        internal TypeCode NewVariableType { get; set; }

        [Reactive]
        internal string EditVariableName { get; set; }

        [Reactive]
        internal TypeCode EditVariableType { get; set; }

        [Reactive]
        internal string SearchTerm { get; set; }

        internal VariablesManageViewModel(Repository repository)
        {
            _repository = repository;

            Types = new List<TypeCode>()
            {
                TypeCode.Int32,
                TypeCode.Single,
                TypeCode.String
            };

            var filter = this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(300))
                .DistinctUntilChanged()
                .Select(BuildFilter);

            _repository.Variables
                .Filter(filter)
                .Sort(new VariableComparer())
                .ObserveOn(RxApp.MainThreadScheduler)
                .Transform(x => new ProxyVariable(x))
                .AutoRefresh()
                .Bind(out _searchResults)
                .DisposeMany()
                .Subscribe();

            this.WhenAnyValue(x => x.NewVariableName)
                .Select(x => !string.IsNullOrEmpty(x))
                .ToPropertyEx(this, x => x.CanAdd);

            this.WhenAnyValue(x => x.SelectedVariable)
                .Where(selected => selected != null)
                .Select(selected => selected.Name)
                .Subscribe(name => EditVariableName = name);

            this.WhenAnyValue(x => x.SelectedVariable)
                .Where(selected => selected != null)
                .Select(selected => selected.Type)
                .Subscribe(type => EditVariableType = Type.GetTypeCode(type));

            this.WhenAnyValue(x => x.SelectedVariable, x => x.EditVariableName)
                .Select(x => x.Item1 != null && !string.IsNullOrEmpty(x.Item2?.Trim() ?? string.Empty))
                .ToPropertyEx(this, x => x.CanEdit);

            this.WhenAnyValue(x => x.SelectedVariable)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.CanDelete);
        }

        private static Func<Variable, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return theme => true;
            }

            return theme => theme.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        }

        internal void Edit()
        {
            var editedVariable = SelectedVariable;

            editedVariable.Name = EditVariableName;           
            editedVariable.Type = Type.GetType("System." + EditVariableType);
        }

        internal void Add()
        {
            _repository.Add(new Variable(NewVariableName, Type.GetType("System." + NewVariableType) ));
        }

        internal void Delete()
        {
            _repository.Remove(SelectedVariable.ExtractModel());
        }
    }

    enum VarType
    {
        Int,
        Float,
        String
    }
}