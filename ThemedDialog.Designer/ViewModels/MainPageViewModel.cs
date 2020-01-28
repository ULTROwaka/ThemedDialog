using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThemedDialog.Core;

namespace ThemedDialog.Designer.ViewModels
{
    public class MainPageViewModel : ReactiveObject
    {
        [Reactive]
        public SourceList<DialogCharacter> Characters
        {
            get;
            set;
        }
        [Reactive]
        public SourceList<Theme> Themes
        {
            get;
            set;
        }
        [Reactive]
        public SourceList<Variable> Variables
        {
            get;
            set;
        }

        public MainPageViewModel()
        {
            Characters = new SourceList<DialogCharacter>();
            Themes = new SourceList<Theme>();
            Variables = new SourceList<Variable>();
        }

        void LoadProject(string file)
        {

        }
        void SaveProject(string folder, string name)
        {

        }
    }
}
