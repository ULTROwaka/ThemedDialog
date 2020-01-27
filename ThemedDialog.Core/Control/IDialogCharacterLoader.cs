using System.Collections.Generic;

namespace ThemedDialog.Core.Control
{
    public interface IDialogCharacterLoader
    {
        IEnumerable<DialogCharacter> Load(string key);
    }
}