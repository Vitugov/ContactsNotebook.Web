using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ContactsNotebook.Wpf.Services.Validation
{
    class ModelState : IModelState
    {
        private Dictionary<string, List<string>> _errors = [];

        public IModelState LoadFromValidationResult(ICollection<ValidationResult> validationResults)
        {
            Clear();
            foreach (var result in validationResults)
            {
                foreach (var memberName in result.MemberNames)
                {
                    if (!ContainsKey(memberName))
                    {
                        this[memberName] = [];
                    }
                    this[memberName].Add(result.ErrorMessage ?? "");
                }
            }
            return this;
        }

        public List<string> this[string key] { get => _errors[key]; set => _errors[key] = value; }

        public ICollection<string> Keys => _errors.Keys;

        public ICollection<List<string>> Values => _errors.Values;

        public int Count => _errors.Count;

        public bool IsReadOnly => false;

        public void Add(string key, List<string> value) => _errors.Add(key, value);

        public void Add(KeyValuePair<string, List<string>> item) => _errors.Add(item.Key, item.Value);

        public void Clear() => _errors = [];

        public bool Contains(KeyValuePair<string, List<string>> item) =>
            ((IDictionary<string, List<string>>)_errors).Contains(item);

        public bool ContainsKey(string key) => _errors.ContainsKey(key);

        public bool Remove(string key) => _errors.Remove(key);

        public bool Remove(KeyValuePair<string, List<string>> item) =>
            ((IDictionary<string, List<string>>)_errors).Remove(item);

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out List<string> value) =>
            _errors.TryGetValue(key, out value);

        public IEnumerator GetEnumerator() => _errors.GetEnumerator();

        public void CopyTo(KeyValuePair<string, List<string>>[] array, int arrayIndex) =>
            ((IDictionary<string, List<string>>)_errors).CopyTo(array, arrayIndex);

        IEnumerator<KeyValuePair<string, List<string>>> IEnumerable<KeyValuePair<string, List<string>>>.GetEnumerator() =>
            _errors.GetEnumerator();
    }
}
