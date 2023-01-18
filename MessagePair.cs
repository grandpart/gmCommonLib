using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    /// <summary>
    /// A pair of Message Codes, the first of which is intended to contain a Message about the second, the Subject, which is typically of
    /// either a Property or an Object.
    /// </summary>
    public class MessagePair
    {
        #region Fields
        /// <summary>
        /// A MessageCode that can be translated by the Zephry translater.
        /// </summary>
        private string _message;
        /// <summary>
        /// A SubjectCode that can be translated by the Zephry translater.
        /// </summary>
        private string _subject;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the _message field.
        /// </summary>
        /// <value>
        /// The aMessage.
        /// </value>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        /// <summary>
        /// Gets or sets the _subject field.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePair"/> class.
        /// </summary>
        public MessagePair() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePair"/> class with a Message and Subject pair.
        /// </summary>
        /// <param name="aMessage">A aMessage.</param>
        /// <param name="aSubject">A subject.</param>
        public MessagePair(string aMessage, string aSubject)
        {
            _message = aMessage;
            _subject = aSubject;
        }
        #endregion
    }
}
