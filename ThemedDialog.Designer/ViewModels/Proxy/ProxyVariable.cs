using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemedDialog.Core;

namespace ThemedDialog.Designer.ViewModels.Proxy
{
    class ProxyVariable : ReactiveObject
    {
        private readonly Variable _model;
        public string Name
        {
            get => _model.Name;
            set => this.RaiseAndSetIfChanged(ref _model.Name, value);
        }

        public Type Type
        {
            get => _model.Type;
            set => this.RaiseAndSetIfChanged(ref _model.Type, value);
        }

        public ProxyVariable(Variable model)
        {
            _model = model;
            Name = _model.Name;
            Type = _model.Type;
        }

        public override string ToString()
        {
            return _model.ToString();
        }

        public Variable ExtractModel()
        {
            return _model;
        }
    }
}
