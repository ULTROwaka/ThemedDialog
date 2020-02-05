using DynamicData;

using System;
using System.Collections.Generic;

using ThemedDialog.Core;

namespace ThemedDialog.Designer.Repositories
{
    internal class Repository
    {
        private readonly SourceList<Theme> _themes;
        private readonly SourceList<Variable> _variables;
        private readonly SourceList<DialogCharacter> _characters;

        internal IObservable<IChangeSet<Theme>> Themes => _themes.Connect();
        internal IObservable<IChangeSet<Variable>> Variables => _variables.Connect();
        internal IObservable<IChangeSet<DialogCharacter>> Characters => _characters.Connect();

        internal Repository(IEnumerable<Theme> themes, IEnumerable<Variable> variables, IEnumerable<DialogCharacter> characters)
        {
            _themes = new SourceList<Theme>();
            _themes.AddRange(themes);

            _variables = new SourceList<Variable>();
            _variables.AddRange(variables);

            _characters = new SourceList<DialogCharacter>();
            _characters.AddRange(characters);
        }

        internal void Add(Theme item)
        {
            _themes.Add(item);
        }

        internal void Add(Variable item)
        {
            _variables.Add(item);
        }

        internal void Add(DialogCharacter item)
        {
            _characters.Add(item);
        }

        internal void Remove(Theme item)
        {
            _themes.Remove(item);
        }

        internal void Remove(Variable item)
        {
            _variables.Remove(item);
        }

        internal void Remove(DialogCharacter item)
        {
            _characters.Remove(item);
        }

        internal void Edit(Theme original, Theme newItem)
        {
            _themes.Replace(original, newItem);
        }

        internal void Edit(Variable original, Variable newItem)
        {
            _variables.Replace(original, newItem);
        }

        internal void Edit(DialogCharacter original, DialogCharacter newItem)
        {
            _characters.Replace(original, newItem);
        }
    }
}