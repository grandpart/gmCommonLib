
namespace Zephry
{
    public class KeyValueCollection : Zephob
    {
        #region Fields
        private string _filter = string.Empty;
        private List<KeyValue> _list = new();
        #endregion

        #region  Properties
        public string Filter { get => _filter; set => _filter = value; }
        public List<KeyValue> List { get => _list; set => _list = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not KeyValueCollection)
            {
                throw new ArgumentException("aKeyValueCollection");
            }

            _filter = ((KeyValueCollection)aSource)._filter;
            _list.Clear();
            foreach (var vKeyValueSource in ((KeyValueCollection)aSource)._list)
            {
                var vKeyValueTarget = new KeyValue();
                vKeyValueTarget.AssignFromSource(vKeyValueSource);
                _list.Add(vKeyValueTarget);
            }
        }
        #endregion
    }
}
