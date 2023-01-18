namespace Zephry
{
    public enum AllocationSearch
    {
        All = 0,
        Project = 1,
        Place = 2
    }

    public enum AllocationType
    {
        Topdown = 0,
        Split = 1
    }

    public enum ClientStatus
    {
        Trial = 0,
        Active = 1,
        Lapsed = 2,
        Suspended = 3
    }

    public enum DayOfMonth
    {
        First = 0,
        Last = 1,
        Actual = 2
    }

    public enum FtxDirection
    {
        In = 0,
        Out = 1
    }

    public enum FtxStatus
    {
        Init = 0,
        Pending = 1,
        Post = 2
    }

    public enum FtxRestriction
    {
        Unrestricted = 0,
        Organization = 1,
        Building = 2,
        Project=3
    }

    public enum FundAction
    {
        ReleaseAll = 0,
        ReleasePart = 1
    }

    public enum FundObject
    {
        All = 0,
        Project = 1,
        Place = 2
    }

    public enum FundStatus
    {
        Open = 0,
        Blocked = 1,
        Posted = 2
    }

    public enum FundStatusSearch
    {
        All = 0,
        Open = 1,
        Blocked = 2,
        Posted = 3
    }

    public enum ObjectType
    {
        Campus = 0,
        Building = 1,
        Category = 2,
        Report = 3,
        Organization = 4
    }

    public enum PersonStatus
    {
        Active = 0,
        Suspended = 1
    }

    public enum ProjectStatus
    {
        Initiated = 0,
        Reviewing = 1,
        Rejected = 2,
        Approved = 3
    }

    public enum RippleSelect
    {
        Single,
        Community,
        All
    };

    public enum SankeyType
    {
        CampusCategory = 0,
        CampusBuildingCategory = 1
    }

    public enum TopupSearch
    {
        All = 0,
        Topup = 1,
        Annual = 2
    }

    public enum UserStatus
    {
        Active = 0,
        Suspended = 1
    }
    
}
