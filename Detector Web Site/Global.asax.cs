﻿/* *********************************************************************
 * This Source Code Form is copyright of 51Degrees Mobile Experts Limited. 
 * Copyright 2014 51Degrees Mobile Experts Limited, 5 Charlotte Close,
 * Caversham, Reading, Berkshire, United Kingdom RG4 7BY
 * 
 * This Source Code Form is the subject of the following patent 
 * applications, owned by 51Degrees Mobile Experts Limited of 5 Charlotte
 * Close, Caversham, Reading, Berkshire, United Kingdom RG4 7BY: 
 * European Patent Application No. 13192291.6; and 
 * United States Patent Application Nos. 14/085,223 and 14/085,301.
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0.
 * 
 * If a copy of the MPL was not distributed with this file, You can obtain
 * one at http://mozilla.org/MPL/2.0/.
 * 
 * This Source Code Form is "Incompatible With Secondary Licenses", as
 * defined by the Mozilla Public License, v. 2.0.
 * ********************************************************************* */

#if VER4
using System;
using System.Web.Configuration;
#endif

namespace Detector
{
    public class Global : System.Web.HttpApplication
    {
        // .NET v4 ONLY
        // Uncomment the following code to use Global.asax rather than web.config HttpModules
        // to invoke device detection and redirection functionality. Do not use both HttpModules
        // and the following uncommented code. Unpredictable results may be experienced.

#if VER4
        //protected void Application_Start(object sender, EventArgs e)
        //{
        //    // Enable the mobile detection provider.
        //    HttpCapabilitiesBase.BrowserCapabilitiesProvider =
        //        new FiftyOne.Foundation.Mobile.Detection.MobileCapabilitiesProvider();
        //}

        //protected void Application_AcquireRequestState(object sender, EventArgs e)
        //{
        //    // Check if a redirection is needed.
        //    FiftyOne.Foundation.Mobile.Redirection.RedirectModule.OnPostAcquireRequestState(sender, e);
        //}
#endif
    }
}