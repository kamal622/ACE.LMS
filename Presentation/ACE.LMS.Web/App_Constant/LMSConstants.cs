using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACE.LMS.Web.App_Constant
{
    public static class LMSConstants
    {


    }

    public enum LibraryBookStatus
    {
        Available = 1,
        Issued = 2,
        Torn = 3
    }

    public enum BookRequestStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Issued = 4,
        Returned = 5
    }



}