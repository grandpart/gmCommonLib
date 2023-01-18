using System;

namespace Zephry
{
    public class PasswordEdit : Zephob
    {
        private string _current;
        private string _new;
        private string _newRepeat;
        private bool _success ;
        private string _message;

        public string Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public string New
        {
            get { return _new; }
            set { _new = value; }
        }

        public string NewRepeat
        {
            get { return _newRepeat; }
            set { _newRepeat = value; }
        }

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is PasswordEdit))
            {
                throw new ArgumentException("Invalid PasswordChange assignment source", "aSource");
            }

            _current = ((PasswordEdit)aSource)._current;
            _new = ((PasswordEdit)aSource)._new;
            _newRepeat = ((PasswordEdit)aSource)._newRepeat;
            _success = ((PasswordEdit)aSource)._success;
            _message = ((PasswordEdit)aSource)._message;
        }
    }
}
