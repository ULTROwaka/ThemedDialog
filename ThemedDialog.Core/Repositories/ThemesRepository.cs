using System;
using System.Collections.Generic;
using System.Text;

namespace ThemedDialog.Core.Repositories
{
    class ThemesRepository : IRepository<Theme, string>
    {
        private readonly Dictionary<string, Theme> _repository;

        public ThemesRepository(Dictionary<string, Theme> repository)
        {
            _repository = repository;
        }

        public bool Add(Theme value)
        {
            try
            {
                _repository.Add(value.Name, value);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }        
        }

        public Theme Get(string key)
        {
            return _repository[key];
        }
    }
}
