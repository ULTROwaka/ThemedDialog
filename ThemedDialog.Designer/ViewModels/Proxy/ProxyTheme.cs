using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemedDialog.Core;

namespace ThemedDialog.Designer.ViewModels.Proxy
{
    public class ProxyTheme : ReactiveObject
    {
        private readonly Theme _model;
        [Reactive]
        public string Name { get; set; }

        public ProxyTheme(Theme model)
        {
            _model = model;
            Name = _model.Name;
        }

        public override string ToString()
        {
            return Name;
        }

        public Theme ExtractModel()
        {
            _model.Name = Name;
            return _model;
        }
    }
}
