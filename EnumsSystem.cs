using System;
using System.Collections.Generic;
using System.Linq;

namespace Zephry
{
    #region Action
    /// <summary>
    /// The actions that can be performed on a POCO object, including List and CRUD.
    /// </summary>
    public enum AccessMode
    {
        List,
        Create,
        Read,
        Update,
        Delete
    }
    #endregion

    #region Action
    /// <summary>
    /// The actions that can be performed on a POCO object, including List and CRUD.
    /// </summary>
    public enum UpdateMode
    {
        None,
        Delete,
        Save
    }
    #endregion

    #region ActiveFilter
    /// <summary>
    ///   Active filter enumeration.
    /// </summary>
    public enum ActiveFilter
    {
        /// <summary>
        ///   Enumeration for <i>Active</i>.
        /// </summary>
        Active,
        /// <summary>
        ///   Enumeration for <i>Inactive</i>.
        /// </summary>
        Inactive,
        /// <summary>
        ///   Enumeration for <i>All</i>.
        /// </summary>
        All
    }
    #endregion

    #region AdminFilter
    /// <summary>
    ///   Admin filter enumeration.
    /// </summary>
    public enum AdminFilter
    {
        /// <summary>
        ///   Enumeration for <i>Administrator</i>.
        /// </summary>
        Admin,
        /// <summary>
        ///   Enumeration for <i>Normal</i> user.
        /// </summary>
        Normal,
        /// <summary>
        ///   Enumeration for <i>All</i>.
        /// </summary>
        All
    }
    #endregion

    public enum AmountOperator
    {
        None,
        LessThan,
        LessEqual,
        GreaterThan,
        GreaterEqual,
        Equal,
        Between
    }

    #region ChangeAction
    /// <summary>
    ///   Change action enumeration.
    /// </summary>
    public enum ChangeAction
    {
        /// <summary>
        ///   Enumeration for <i>Insert</i>.
        /// </summary>
        Insert,
        /// <summary>
        ///   Enumeration for <i>Update</i>.
        /// </summary>
        Update,
        /// <summary>
        ///   Enumeration for <i>Delete</i>.
        /// </summary>
        Delete,
        /// <summary>
        ///   Enumeration for <i>Browse</i>.
        /// </summary>
        Browse
    }
    #endregion

    #region ConnectionContext
    /// <summary>
    ///   Connection context enumeration.
    /// </summary>
    public enum ConnectionContext 
    {
        Smart, Browse, Integrated 
    }
    #endregion

    #region DateOperator
    /// <summary>
    ///   A list of filter operators that can be applied to a <see cref="DateTime"/> type.
    /// </summary>
    public enum DateOperator
    {
        None, LessThan, LessEqual, GreaterThan, GreaterEqual, Equal, Between
    }
    #endregion

    #region Deviance
    /// <summary>
    /// DevianceCorrection
    /// </summary>
    public enum DevianceCorrection 
    { 
        None, 
        CorrectAmount, 
        CorrectList 
    }
    #endregion

    #region EmailPriority
    /// <summary>
    /// Email Priority
    /// </summary>
    public enum EmailPriority
    {
        Low, Normal, High
    }
    #endregion

    public enum FieldType
    {
        Lookup = 0,
        Text = 1,
        Boolean = 2,
        Date = 3,
        DateTime = 4,
        Integer = 5,
        Float = 6,
        Money = 7,
        Percent = 8
    }

    #region FinPeriodOperator
    /// <summary>
    ///   A list of filter operators that can be applied to a Financial Period (int as YYYYMM).
    /// </summary>
    public enum FinPeriodOperator
    {
        None, LessThan, LessEqual, GreaterThan, GreaterEqual, Equal, Between
    }
    #endregion
    
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }

    #region IDType
    /// <summary>
    /// An enumeration of IDType
    /// </summary>
    public enum IdType
    {
        /// <summary>
        /// A South African ID Number
        /// </summary>
        Said=1,
        /// <summary>
        /// A South African Passport
        /// </summary>
        SaPassport=2
    }
    #endregion

    #region IntegrationKey
    /// <summary>
    /// An essentially hard-coded list of integrated organizations.
    /// </summary>
    public enum IntegrationKey
    {
        /// <summary>
        /// The Automated Fingerprint Identification Systems of the South African Police Services
        /// </summary>
        ZaAfis=1,
        /// <summary>
        /// The South African Background Screening company Managed Integrity Evaluation (Pty) Limited
        /// </summary>
        ZaMie=2
    }
    #endregion

    #region JobStatus
    /// <summary>
    /// A Status indication whether a person may work, is working or is no longer working.
    /// </summary>
    public enum JobStatus
    {
        Candidate,
        Active,
        Terminated
    }
    #endregion

    #region JobTerm
    /// <summary>
    /// A Status indication the terms of an emplyee's employment
    /// </summary>
    public enum JobTerm
    {
        None,
        Probation,
        Permanent,
        PartTime,
        Seasonal
    }
    #endregion

    #region LikertScale
    /// <summary>
    ///  A Likert scale is a psychometric scale commonly involved in research that employs questionnaires. 
    /// </summary>
    public enum LikertScale
    {
        StronglyDisagree = 1, Disagree = 2, Neutral = 3, Agree = 4, StronglyAgree = 5
    }
    #endregion

    #region ListAction
    /// <summary>
    ///   List action enumeration.
    /// </summary>
    public enum ListAction
    {
        /// <summary>
        /// Inform the list that double-click should invoke an editor.
        /// </summary>
        Change,
        /// <summary>
        /// Inform the list that double-click should return an item.
        /// </summary>
        Select,
        /// <summary>
        /// Inform the list that a drilldown operation is in progress.
        /// </summary>
        Drilldown
    }
    #endregion

    #region ListBehavior
    /// <summary>
    ///   List behavior enumeration used to specify the behavior of a Smart7  list form.
    /// </summary>
    public enum ListBehavior
    {
        /// <summary>
        ///   Normal.
        /// </summary>
        None,
        /// <summary>
        ///   Multiple selection mode.
        /// </summary>
        Checked
    }
    #endregion

    #region LogonType
    public enum UserType
    {
        Admin = 0,
        User = 1
    }
    #endregion

    #region MailRecipientContext
    /// <summary>
    ///   MailRecipientContext enumeration is used to determine what type of email copy a recipient will receive.
    /// </summary>
    public enum MailRecipientContext
    {
        /// <summary>
        ///   Main recipient.
        /// </summary>
        To,
        /// <summary>
        ///   Carbon Copy.
        /// </summary>
        Cc,
        /// <summary>
        ///   Blind Carbon Copy.
        /// </summary>
        Bcc
    }
    #endregion

    #region MonthOfYear
    /// <summary>
    ///   MonthName enumeration.
    /// </summary>
    public enum MonthOfYear
    {
        /// <summary>
        ///   January
        /// </summary>
        January = 1,

        /// <summary>
        ///   February
        /// </summary>
        February = 2,

        /// <summary>
        ///   March
        /// </summary>
        March = 3,

        /// <summary>
        ///   April
        /// </summary>
        April = 4,

        /// <summary>
        ///   May
        /// </summary>
        May = 5,

        /// <summary>
        ///   June
        /// </summary>
        June = 6,

        /// <summary>
        ///   July
        /// </summary>
        July = 7,

        /// <summary>
        ///   August
        /// </summary>
        August = 8,

        /// <summary>
        ///   September
        /// </summary>
        September = 9,

        /// <summary>
        ///   October
        /// </summary>
        October = 10,

        /// <summary>
        ///   November
        /// </summary>
        November = 11,

        /// <summary>
        ///   December
        /// </summary>
        December = 12
    }
    #endregion

    #region ObjectState
    /// <summary>
    ///   An enumeration of values describing the state of an object. Typically used by lists to disable (exclude)
    ///   items for selection
    /// </summary>
    public enum ObjectState
    {
        /// <summary>
        /// No published ObjectState
        /// </summary>
        None,
        /// <summary>
        /// Disabled ObjectState
        /// </summary>
        Disabled,
        /// <summary>
        /// Checked ObjectState
        /// </summary>
        Checked
    }
    #endregion

    #region SourceAssembly
    /// <summary>
    ///   The SourceAssembly enumeration is used to specify in which code assembly a <see cref="ZpCodedException "/> originated.
    /// </summary>
    public enum SourceAssembly
    {
        /// <summary>
        ///   Common assembly.
        /// </summary>
        Common,
        /// <summary>
        ///   Classes assembly.
        /// </summary>
        Classes,
        /// <summary>
        ///   Data assembly.
        /// </summary>
        Data,
        /// <summary>
        ///   Business assembly.
        /// </summary>
        Business,
        /// <summary>
        ///   Services assembly.
        /// </summary>
        Services,
        /// <summary>
        ///   Consumers assembly.
        /// </summary>
        Consumers,
        /// <summary>
        ///   Controls assembly.
        /// </summary>
        Controls
    }
    #endregion

    #region TokenSource
    /// <summary>
    ///   Token source enumeration.
    /// </summary>
    public enum TokenSource
    {
        /// <summary>
        ///   Enumeration for <i>File</i>.
        /// </summary>
        File,
        /// <summary>
        ///   Enumeration for <i>Command line</i>.
        /// </summary>
        Commandline,
        /// <summary>
        ///   Enumeration for <i>Passed parameters</i>.
        /// </summary>
        Params
    }
    #endregion

    #region TransactionResult
    /// <summary>
    /// The type of an exception that was raised
    /// </summary>
    public enum TransactionResult
    {
        /// <summary>
        /// The transaction completed successfully
        /// </summary>
        Ok = 200,
        /// <summary>
        /// The transaction raised an access exception
        /// </summary>
        Access = 401,
        /// <summary>
        /// The user is not authorized
        /// </summary>
        Role = 403,
        /// <summary>
        /// The user has been hijacked
        /// </summary>
        BadData = 406,
        /// <summary>
        /// The transaction raised a Delete exception
        /// </summary>
        Delete = 409,
        /// <summary>
        /// The user has been hijacked
        /// </summary>
        Hijack = 418,
        /// <summary>
        /// The transaction raised a business rule exception
        /// </summary>
        Rule = 499,
        /// <summary>
        /// The transaction raised a general exception
        /// </summary>
        General = 500
    }
    #endregion

}
