using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearcherInfoService.Helpers
{
    public class Constants
    {
        public const int ROLE_CAMPAIGN_MGR = 1;
        public const int ROLE_RESEARCHER = 2;

        public const int APPROVAL_STS_NOT_STARTED= 1;
        public const int APPROVAL_STS_APPROVED = 2;
        public const int APPROVAL_STS_DENIED = 3;
        public const int APPROVAL_STS_NEEDINFO = 4;
        public const int APPROVAL_STS_BACKFORREV = 5;
    }
}