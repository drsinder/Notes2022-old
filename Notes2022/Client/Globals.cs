﻿/*--------------------------------------------------------------------------
    **
    ** Copyright © 2022, Dale Sinder
    **
    ** Name: Globals.cs
    **
    ** Description:
    **      Adds one item to apps collection of shared global defines
    **
    ** This program is free software: you can redistribute it and/or modify
    ** it under the terms of the GNU General Public License version 3 as
    ** published by the Free Software Foundation.   
    **
    ** This program is distributed in the hope that it will be useful,
    ** but WITHOUT ANY WARRANTY; without even the implied warranty of
    ** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    ** GNU General Public License version 3 for more details.
    **
    **  You should have received a copy of the GNU General Public License
    **  version 3 along with this program in file "license-gpl-3.0.txt".
    **  If not, see<http://www.gnu.org/licenses/gpl-3.0.txt>.
    **
    **--------------------------------------------------------------------------*/

using Notes2022.Shared;

namespace Notes2022.Client
{
    public static class Globals
    {
        public static bool RolesValid { get; set; }
        public static bool IsAdmin  { get; set; }
        public static bool IsUser { get; set; }
        public static EditUserViewModel? EditUserVModel { get; set; }

        public static UserData UserData { get; set; }

        public static UserData GetUserData()
        {
            return UserData;
        }

        public static List<UserData> UserDataList { get; set; }

        public static DateTime StartupDateTime { get; set; }

        public static string StuffUrl { get; } = "https://www.drsinder.com/NotesStuff/Notes2022/";

        public static string AccessOther() { return "Other"; }

        public static string AccessOtherId() { return "Other"; }


        //public static string? DisplayName()
        //{
        //    return EditUserVModel?.UserData?.DisplayName;
        //}

        //public static TimeSpan Uptime()
        //{
        //    return DateTime.Now.ToUniversalTime() - StartupDateTime;
        //}

        //public static string RootUri { get; set; }

        //public static string PathBase { get; set; }

        //public static string MigrateDb { get; set; }
        //public static string SendGridEmail { get; set; }

        //public static string SendGridName { get; set; }

        //public static string SendGridApiKey { get; set; }

        //public static string EmailName { get; set; }

        //public static string ImportedAuthorId() { return "*imported*"; }

        //public static string ProductionUrl { get; set; }

        //public static int TimeZoneDefaultID { get; set; }

        //public static string PusherAppId { get; set; }
        //public static string PusherKey { get; set; }
        //public static string PusherSecret { get; set; }
        //public static string PusherCluster { get; set; }

        //public static string ChatKitAppLoc { get; set; }
        //public static string ChatKitKey { get; set; }

        //public static string DBConnectString { get; set; }

        //public static string PrimeAdminName { get; set; }
        //public static string PrimeAdminEmail { get; set; }

        //public static string GuestId { get; set; } = "x";
        //public static string GuestEmail { get; set; }

        public static DateTime LocalTimeBlazor(DateTime dt)
        {
            int OHours = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours;
            int OMinutes = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Minutes;

            return dt.AddHours(OHours).AddMinutes(OMinutes);
        }

        //public static IWebHostEnvironment Env { get; set; }

    }
}
