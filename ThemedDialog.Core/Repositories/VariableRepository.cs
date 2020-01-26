using System;
using System.Collections.Generic;
using System.Text;

namespace ThemedDialog.Core.Repositories
{
    public class VariableRepository : IRepository<Variable, string>
    {
        private readonly Dictionary<string, Variable> _repository;

        public VariableRepository(Dictionary<string, Variable> repository)
        {
            _repository = repository;
        }

        public bool Add(Variable value)
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

        public Variable Get(string key)
        {
            return _repository[key];
        }
    }
}

