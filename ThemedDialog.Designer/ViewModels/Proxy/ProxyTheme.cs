﻿using ReactiveUI;
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
        public string Name
        {
            get => _model.Name;
            set => this.RaiseAndSetIfChanged(ref _model.Name, value);
        }

        public ProxyTheme(Theme model)
        {
            _model = model;
            Name = _model.Name;
        }

        public override string ToString()
        {
            return _model.ToString();
        }

        public Theme ExtractModel()
        {
            return _model;
        }
    }
}
