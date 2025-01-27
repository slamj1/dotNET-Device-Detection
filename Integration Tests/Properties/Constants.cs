﻿/* *********************************************************************
 * This Source Code Form is copyright of 51Degrees Mobile Experts Limited. 
 * Copyright © 2015 51Degrees Mobile Experts Limited, 5 Charlotte Close,
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
 * This Source Code Form is “Incompatible With Secondary Licenses”, as
 * defined by the Mozilla Public License, v. 2.0.
 * ********************************************************************* */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiftyOne.Tests.Integration
{
    public class Constants
    {
        public const string GOOD_USERAGENTS_FILE = "20000 User Agents.csv";

        public const string LITE_PATTERN_V31 = "51Degrees-LiteV3.1.dat";

        public const string LITE_PATTERN_V32 = "51Degrees-LiteV3.2.dat";

        public const string LITE_TRIE_V30 = "51Degrees-LiteV3.0.trie";

        public const string LITE_TRIE_V32 = "51Degrees-LiteV3.2.trie";

        public const string ENTERPRISE_PATTERN_V31 = "51Degrees-EnterpriseV3.1.dat";

        public const string ENTERPRISE_PATTERN_V32 = "51Degrees-EnterpriseV3.2.dat";

        public const string ENTERPRISE_TRIE_V30 = "51Degrees-EnterpriseV3.0.trie";

        public const string ENTERPRISE_TRIE_V32 = "51Degrees-EnterpriseV3.2.trie";

        public const string PREMIUM_PATTERN_V31 = "51Degrees-PremiumV3.1.dat";

        public const string PREMIUM_PATTERN_V32 = "51Degrees-PremiumV3.2.dat";

        public const string PREMIUM_TRIE_V30 = "51Degrees-PremiumV3.0.trie";

        public const string PREMIUM_TRIE_V32 = "51Degrees-PremiumV3.2.trie";

        public const int USERAGENT_COUNT = 20000;

        public static readonly string[] DATA_DIRECTORIES = new string[] {
            "data",
            "dotNet-Device-Detection/data",
            "Java-Device-Detection/data",
            "Device-Detection/data"
        };
    }
}
