
namespace Zephry
{
    public class KeyValueCollection : Zephob
    {
        #region Fields
        private List<KeyValue> _keyValueList = new();
        #endregion

        #region  Properties
        public List<KeyValue> KeyValueList { get => _keyValueList; set => _keyValueList = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not KeyValueCollection)
            {
                throw new ArgumentException("aKeyValueCollection");
            }

            _keyValueList.Clear();
            foreach (var vKeyValueSource in ((KeyValueCollection)aSource)._keyValueList)
            {
                var vKeyValueTarget = new KeyValue();
                vKeyValueTarget.AssignFromSource(vKeyValueSource);
                _keyValueList.Add(vKeyValueTarget);
            }
        }
        #endregion
    }
}
