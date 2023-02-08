
namespace Zephry
{
    public class KeyValue : Zephob
    {

        #region Fields
        private int _key;
        private string _value = string.Empty;
        #endregion

        #region Properties
        public int Key { get => _key; set => _key = value; }
        public string Value { get => _value; set => _value = value; }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not LogonToken)
            {
                throw new ArgumentException("Invalid Assignment Source for KeyValue");
            }

            _key = ((KeyValue)aSource)._key;
            _value = ((KeyValue)aSource)._value;
        }
        #endregion

    }
}
