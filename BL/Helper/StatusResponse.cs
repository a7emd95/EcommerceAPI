using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helper
{
    public static class StatusResponse
    {
        private static readonly string success = "success";
        public static string Success
        {
            get
            {
                return success;
            }
        }


        private static readonly string failed = "Failed";
        public static string Failed
        {
            get
            {
                return failed;
            }
        }


    }
}
