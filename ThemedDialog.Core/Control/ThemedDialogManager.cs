using System.Collections.Generic;

namespace ThemedDialog.Core.Control
{
    public class ThemedDialogManager
    {
        private readonly IDialogCharacterLoader _characterLoader;
        private readonly Dictionary<string, DialogCharacter> _characters;
        public bool CaseSensetive;

        public ThemedDialogManager(IDialogCharacterLoader characterLoader, bool caseSensetive = false)
        {
            _characterLoader = characterLoader;
            CaseSensetive = caseSensetive;
        }

        public void LoadCharacters(string key)
        {
            IEnumerable<DialogCharacter> characters = _characterLoader.Load(key);
            foreach (var character in characters)
            {
                _characters.Add(character.Name, character);
            }
        }

        public DialogCharacter GetDialogCharacter(string characterName)
        {
            string name = CaseSensetive? characterName : characterName.ToUpper();
            return _characters[name];
        }

        public IEnumerable<Dialog> GetAvailableCharacterDialogs(string characterName, IConditionChecker checker)
        {
            string name = CaseSensetive ? characterName : characterName.ToUpper();
            return GetAvailableCharacterDialogs(_characters[name], checker);
        }

        public IEnumerable<Dialog> GetAvailableCharacterDialogs(DialogCharacter character, IConditionChecker checker)
        {
            var availableDialogs = new List<Dialog>();
            foreach (var dialog in character.Dialogs)
            {
                foreach (var sentence in dialog.Sentences)
                {
                    if (checker.Check(sentence.Condtions))
                    {
                        availableDialogs.Add(dialog);
                        break;
                    }
                }
            }
            return availableDialogs;
        }

        public IEnumerable<Sentence> GetAvailableSentences(Dialog dialog, IConditionChecker checker)
        {
            var availableSentences = new List<Sentence>();
            foreach (var sentence in dialog.Sentences)
            {
                if (checker.Check(sentence.Condtions))
                {
                    availableSentences.Add(sentence);
                }
            }
            return availableSentences;
        }
    }
}