using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.IO;

namespace Zephry
{
    /// <summary>
    /// A static utility class with commonly used Email and Calendar methods
    /// </summary>
    public static class CommonEmail
    {
        #region SendEmailMessage
        /// <summary>
        /// Sends the email message.
        /// </summary>
        /// <param name="aEmailHost">A email host.</param>
        /// <param name="aEmailArgument">A email argument.</param>
        public static void SendEmailMessage(EmailHost aEmailHost, EmailArgument aEmailArgument)
        {
            try
            {
                using (var vMailMessage = GetEmailMessage(aEmailArgument))
                {
                    SendMail(aEmailHost, vMailMessage);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw;
                }
                throw ex.InnerException;
            }
        }
        #endregion

        #region GetEmailMessage
        /// <summary>
        /// Returns an Email Message as a standard <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="aEmailArgument">A email argument.</param>
        /// <returns></returns>
        private static MailMessage GetEmailMessage(EmailArgument aEmailArgument)
        {
            // Create a MailMessage with a subject and from address
            var vMailMessage = new MailMessage
            { 
                Subject = aEmailArgument.Subject, 
                From = new MailAddress(aEmailArgument.Organizer.Address, aEmailArgument.Organizer.DisplayName),
                Sender = new MailAddress(aEmailArgument.Organizer.Address, aEmailArgument.Organizer.DisplayName)
            };
            //  Address the message to not-empty addresses
            aEmailArgument.RecipientList.ForEach(vEmailAddress =>
            {
                if (!String.IsNullOrWhiteSpace(vEmailAddress.Address))
                {
                    vMailMessage.To.Add(new MailAddress(vEmailAddress.Address, vEmailAddress.DisplayName));
                }
            });
            // Load Attachments
            aEmailArgument.AttachmentList.ForEach(vAttachment =>
            {
                if (vAttachment != null)
                {
                    vMailMessage.Attachments.Add(vAttachment);
                }
            });
            vMailMessage.IsBodyHtml = false;
            vMailMessage.Body = GetEmailBody(aEmailArgument.Body);
            //string vBody = GetEmailBody(aEmailArgument.Body);
            //// Add a plain text body
            //AlternateView vPlainView = AlternateView.CreateAlternateViewFromString(
            //    vBody,
            //    new ContentType("text/plain; charset=iso-8859-1"));
            ////vPlainView.TransferEncoding = TransferEncoding.SevenBit;
            //vMailMessage.AlternateViews.Add(vPlainView);
            
            //// Add an html body
            //vMailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(vBody, new ContentType("text/html")));

            return vMailMessage;
        }
        #endregion

        #region SendHtmlEmailMessage
        /// <summary>
        /// Sends an HTML email message.
        /// </summary>
        /// <param name="aEmailHost"></param>
        /// <param name="aEmailArgument"></param>
        public static void SendHtmlEmailMessage(EmailHost aEmailHost, EmailArgument aEmailArgument)
        {
            try
            {
                using (var vMailMessage = GetHtmlEmailMessage(aEmailArgument))
                {
                    SendMail(aEmailHost, vMailMessage);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw;
                }
                throw ex.InnerException;
            }
        }
        #endregion

        #region GetHtmlEmailMessage
        /// <summary>
        /// Gets an HTML email message from an EmailArgument.
        /// </summary>
        /// <param name="aEmailArgument">A email argument.</param>
        /// <returns></returns>
        public static MailMessage GetHtmlEmailMessage(EmailArgument aEmailArgument)
        {
            string htmlMessage = GetEmailBody(aEmailArgument.Body);

            var vMailMessage = new MailMessage
            {
                Subject = aEmailArgument.Subject,
                IsBodyHtml = true,
                From = new MailAddress(aEmailArgument.Organizer.Address, aEmailArgument.Organizer.DisplayName)
            };
            //  Address the message to not-empty addresses
            aEmailArgument.RecipientList.ForEach(vEmailAddress =>
            {
                if (!String.IsNullOrWhiteSpace(vEmailAddress.Address))
                {
                    vMailMessage.To.Add(new MailAddress(vEmailAddress.Address, vEmailAddress.DisplayName));
                }
            });

            // Create the HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                                                         htmlMessage,
                                                         Encoding.UTF8,
                                                         MediaTypeNames.Text.Html);
            // Create a plain text message for client that don't support HTML
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(
                                                        htmlMessage,
                                                        Encoding.UTF8,
                                                        MediaTypeNames.Text.Plain);

            // Create linked resource for the HTML view
            if (File.Exists("C:\\webassets\\agilepamLogo.png"))
            {
                string mediaType = MediaTypeNames.Image.Jpeg;
                LinkedResource img = new LinkedResource(@"C:\webassets\agilepamLogo.png", mediaType)
                {
                    ContentId = "AgilePamLogo"
                };
                // Make sure you set all these values!!!
                img.ContentType.MediaType = mediaType;
                img.TransferEncoding = TransferEncoding.Base64;
                img.ContentType.Name = img.ContentId;
                img.ContentLink = new Uri("cid:" + img.ContentId);
                htmlView.LinkedResources.Add(img);
            }

            // Add the views to the message, and return
            vMailMessage.AlternateViews.Add(plainView);
            vMailMessage.AlternateViews.Add(htmlView);

            return vMailMessage;
        }
        #endregion

        #region GetEmailBody
        /// <summary>
        /// Builds an Email body with line breaks
        /// </summary>
        /// <param name="aEmailBody">A email body.</param>
        /// <returns></returns>
        private static string GetEmailBody(EmailBody aEmailBody)
        {
            string vBody = string.Empty;
            foreach (string vPara in aEmailBody)
            {
                vBody = vBody + vPara + Environment.NewLine;
            }
            return vBody;
        }
        #endregion

        #region SendMeetingRequest
        /// <summary>
        /// Sends an Email Message as a meeting request.
        /// </summary>
        /// <param name="aEmailHost">A email host.</param>
        /// <param name="aEmailArgument">A email argument.</param>
        public static void SendMeetingRequest(EmailHost aEmailHost, EmailArgument aEmailArgument)
        {
            try
            {
                using (var vMailMessage = GetMeetingRequest(aEmailArgument))
                {
                    SendMail(aEmailHost, vMailMessage);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    throw;
                }
                throw ex.InnerException;
            }
        }
        #endregion

        #region GetMeetingRequest
        /// <summary>
        /// Returns a Meeting Request as a standard <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="aEmailArgument">A email argument.</param>
        /// <returns></returns>
        private static MailMessage GetMeetingRequest(EmailArgument aEmailArgument)
        {
            var vMailMessage = new MailMessage();

            // Add text and HTML views
            vMailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(GetMeetingBodyText(aEmailArgument), new ContentType("text/plain")));
            vMailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(GetMeetingBodyHtml(aEmailArgument), new ContentType("text/html")));
            // Add Calendar View
            var calendarView = AlternateView.CreateAlternateViewFromString(GetMeetingBodyCalendar(aEmailArgument), new ContentType("text/calendar; method=REQUEST; charset=utf-8; name=invite.ics"));
            calendarView.TransferEncoding = TransferEncoding.SevenBit;
            vMailMessage.AlternateViews.Add(calendarView);
            //  Address the message to not-empty addresses
            vMailMessage.From = new MailAddress(aEmailArgument.Organizer.Address, aEmailArgument.Organizer.DisplayName);
            aEmailArgument.RecipientList.ForEach(vEmailAddress => 
            {
                if (!String.IsNullOrWhiteSpace(vEmailAddress.Address))
                {
                    vMailMessage.To.Add(new MailAddress(vEmailAddress.Address, vEmailAddress.DisplayName));
                }
            });
            vMailMessage.Subject = aEmailArgument.Subject;

            return vMailMessage;
        }
        #endregion

        #region GetMeetingBodyText
        /// <summary>
        /// Gets the Text body of a Calendar View message.
        /// </summary>
        /// <param name="aEmailArgument">A email argument.</param>
        /// <returns></returns>
        private static string GetMeetingBodyText(EmailArgument aEmailArgument)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("Type:Single Meeting");
            vStringBuilder.AppendLine(String.Format("Organizer: {0}", aEmailArgument.Organizer.DisplayName));
            vStringBuilder.AppendLine(String.Format("Start Time:{0}{1}", aEmailArgument.DateStart.ToLongDateString(), aEmailArgument.DateStart.ToLongTimeString()));
            vStringBuilder.AppendLine(String.Format("End Time:{0}{1}", aEmailArgument.DateEnd.ToLongDateString(), aEmailArgument.DateEnd.ToLongTimeString()));
            vStringBuilder.AppendLine(String.Format("Time Zone:{0}", System.TimeZone.CurrentTimeZone.StandardName));
            vStringBuilder.AppendLine(String.Format("Location: {0}", aEmailArgument.Location));
            vStringBuilder.AppendLine("");
            vStringBuilder.AppendLine("*~*~*~*~*~*~*~*~*~*");
            vStringBuilder.AppendLine("");
            vStringBuilder.AppendLine(String.Format("{0}", aEmailArgument.Summary));
            return vStringBuilder.ToString();
        }
        #endregion

        #region GetMeetingBodyHtml
        /// <summary>
        /// Gets the Html body of a Calendar View message.
        /// </summary>
        /// <param name="aEmailArgument">A email argument.</param>
        /// <returns></returns>
        private static string GetMeetingBodyHtml(EmailArgument aEmailArgument)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\">");
            vStringBuilder.AppendLine("<html>");
            vStringBuilder.AppendLine("    <head>");
            vStringBuilder.AppendLine("        <META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">");
            vStringBuilder.AppendLine("        <META NAME=\"Generator\" CONTENT=\"MS Exchange Server version 6.5.7652.24\">");
            vStringBuilder.AppendLine($"        <title>{aEmailArgument.Summary}</title>");
            vStringBuilder.AppendLine("    </head>");
            vStringBuilder.AppendLine("    <body>");
            vStringBuilder.AppendLine("        <p><font size=2>Type: Meeting<br>");
            vStringBuilder.AppendLine($"           Organizer: {aEmailArgument.Organizer.DisplayName}<br>");
            vStringBuilder.AppendLine($"           Start Time: {aEmailArgument.DateStart.ToLongDateString()} {aEmailArgument.DateStart.ToLongTimeString()}<br>");
            vStringBuilder.AppendLine($"           End Time: {aEmailArgument.DateEnd.ToLongDateString()} {aEmailArgument.DateEnd.ToLongTimeString()}<br>");
            vStringBuilder.AppendLine($"           Time Zone:{TimeZone.CurrentTimeZone.StandardName}<br>");
            vStringBuilder.AppendLine($"           Location: {aEmailArgument.Location}<br>");
            vStringBuilder.AppendLine("           <br>");
            vStringBuilder.AppendLine($"           Additional Notes: {aEmailArgument.Summary}<br></font>");
            vStringBuilder.AppendLine("        </p>");
            vStringBuilder.AppendLine("        ");
            vStringBuilder.AppendLine("    </body>");
            vStringBuilder.AppendLine("</html>");
            //
            return vStringBuilder.ToString();
        }
        #endregion

        #region GetMeetingBodyCalendar
        /// <summary>
        /// Gets the Calendar body of a Calendar View message.
        /// </summary>
        /// <param name="aEmailArgument">A email argument.</param>
        /// <returns></returns>
        private static string GetMeetingBodyCalendar(EmailArgument aEmailArgument)
        {
            const string calDateFormat = "yyyyMMddTHHmmssZ";
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("BEGIN:VCALENDAR");
            vStringBuilder.AppendLine("METHOD:REQUEST");
            vStringBuilder.AppendLine("PRODID:Microsoft Exchange Server 2007");
            vStringBuilder.AppendLine("VERSION:2.0");
            vStringBuilder.AppendLine("BEGIN:VTIMEZONE");
            vStringBuilder.AppendLine("TZID:South Africa Standard Time");
            vStringBuilder.AppendLine("BEGIN:STANDARD");
            vStringBuilder.AppendLine("DTSTART:16010101T000000");
            vStringBuilder.AppendLine("TZOFFSETFROM:+0200");
            vStringBuilder.AppendLine("TZOFFSETTO:+0200");
            vStringBuilder.AppendLine("END:STANDARD");
            vStringBuilder.AppendLine("BEGIN:DAYLIGHT");
            vStringBuilder.AppendLine("DTSTART:16010101T000000");
            vStringBuilder.AppendLine("TZOFFSETFROM:+0200");
            vStringBuilder.AppendLine("TZOFFSETTO:+0200");
            vStringBuilder.AppendLine("END:DAYLIGHT");
            vStringBuilder.AppendLine("END:VTIMEZONE");
            vStringBuilder.AppendLine("BEGIN:VEVENT");
            vStringBuilder.AppendLine(String.Format("ORGANIZER;CN={0}:MAILTO:{1}", aEmailArgument.Organizer.DisplayName, aEmailArgument.Organizer.Address));
            aEmailArgument.RecipientList.ForEach(vEmailAddress =>  
            {
                vStringBuilder.AppendLine(String.Format("ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={0}:MAILTO:{1}", vEmailAddress.Address, vEmailAddress.Address));
            });
            vStringBuilder.AppendLine(String.Format("DESCRIPTION;LANGUAGE=en-US:{0}\\n\\n", aEmailArgument.Subject.Replace(",", "\\,")));
            vStringBuilder.AppendLine(String.Format("SUMMARY;LANGUAGE=en-US:{0}", aEmailArgument.Subject));            
            vStringBuilder.AppendLine(String.Format("DTSTART;TZID=South Africa Standard Time:{0}", aEmailArgument.DateStart.ToUniversalTime().ToString(calDateFormat)));
            vStringBuilder.AppendLine(String.Format("DTEND;TZID=South Africa Standard Time:{0}", aEmailArgument.DateEnd.ToUniversalTime().ToString(calDateFormat)));
            vStringBuilder.AppendLine(String.Format("UID:{0}", Guid.NewGuid().ToString("B")));
            vStringBuilder.AppendLine("CLASS:PUBLIC");
            vStringBuilder.AppendLine("PRIORITY:5");
            vStringBuilder.AppendLine(String.Format("DTSTAMP:{0}", DateTime.Now.ToUniversalTime().ToString(calDateFormat)));
            vStringBuilder.AppendLine("TRANSP:OPAQUE");
            vStringBuilder.AppendLine("STATUS:CONFIRMED");
            vStringBuilder.AppendLine("SEQUENCE:0");
            vStringBuilder.AppendLine(String.Format("LOCATION;LANGUAGE=en-US:{0}", aEmailArgument.Location));
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-APPT-SEQUENCE:0");
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-OWNERAPPTID:-1");
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:TENTATIVE");
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-INTENDEDSTATUS:BUSY");
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-ALLDAYEVENT:FALSE");
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-IMPORTANCE:1");
            vStringBuilder.AppendLine("X-MICROSOFT-CDO-INSTTYPE:0");            
            vStringBuilder.AppendLine("BEGIN:VALARM");
            vStringBuilder.AppendLine("ACTION:DISPLAY");
            vStringBuilder.AppendLine("DESCRIPTION:REMINDER");
            vStringBuilder.AppendLine("TRIGGER;RELATED=START:-PT15M");
            vStringBuilder.AppendLine("END:VALARM");
            vStringBuilder.AppendLine("END:VEVENT");
            vStringBuilder.AppendLine("END:VCALENDAR");
            //
            return vStringBuilder.ToString();
        }
        #endregion

        #region SendMail
        /// <summary>
        /// Sends the mail message via the host.
        /// </summary>
        /// <param name="aEmailHost">A email host.</param>
        /// <param name="aMailMessage">A mail message.</param>
        private static void SendMail(EmailHost aEmailHost, MailMessage aMailMessage)
        {
            using (var smtpClient = new SmtpClient
            {
                Host = aEmailHost.Name,
                Port = aEmailHost.Port,
                EnableSsl = aEmailHost.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = aEmailHost.UseDefaultCredentials,
                Credentials = new NetworkCredential(aEmailHost.UserId, aEmailHost.Password)
            })
            {
                smtpClient.Send(aMailMessage);
            }
        }
        #endregion

    }
}
