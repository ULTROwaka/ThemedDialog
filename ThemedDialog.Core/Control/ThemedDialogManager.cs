using System.Collections.Generic;

namespace ThemedDialog.Core.Control
{
    public class ThemedDialogManager
    {
        private readonly Dictionary<string, DialogCharacter> _characters;

        public DialogCharacter GetDialogCharacter(string characterName)
        {
            string upcasedCharacterName = characterName.ToUpper();
            return _characters[upcasedCharacterName];
        }

        public IEnumerable<Dialog> GetCharacterDialogs(string characterName)
        {
            string upcasedCharacterName = characterName.ToUpper();
            return _characters[upcasedCharacterName].Dialogs;
        }

        public IEnumerable<Dialog> GetAvailableCharacterDialogs(string characterName, IConditionChecker checker)
        {
            string upcasedCharacterName = characterName.ToUpper();
            return GetAvailableCharacterDialogs(_characters[upcasedCharacterName], checker);
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