using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Zephry
{
    /// <summary>
    /// A list of parameters used in the construction of Email and Calendar Messages
    /// </summary>
    public class EmailArgument
    {
        #region Fields
        DateTime _dateStart;
        DateTime _dateEnd;
        string _subject;
        EmailBody _body = new EmailBody();
        string _summary;
        string _location;
        private EmailAddress _organizer = new EmailAddress();
        private List<EmailAddress> _recipientList = new List<EmailAddress>();
        private List<Attachment> _attachmentList = new List<Attachment>();
        #endregion

        #region Properties
        public DateTime DateStart
        {
            get { return _dateStart; }
            set { _dateStart = value; }
        }

        public DateTime DateEnd
        {
            get { return _dateEnd; }
            set { _dateEnd = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public EmailBody Body
        {
            get { return _body; }
            set { _body = value; }
        } 

        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public EmailAddress Organizer
        {
            get { return _organizer; }
            set { _organizer = value; }
        }

        public List<EmailAddress> RecipientList
        {
            get { return _recipientList; }
            set { _recipientList = value; }
        }

        public List<Attachment> AttachmentList
        {
            get { return _attachmentList; }
            set { _attachmentList = value; }
        }

        #endregion
    }
}
