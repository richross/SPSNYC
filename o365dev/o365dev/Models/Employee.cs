using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace o365dev.Models
{
    public class Employee
    {
        public string objectType { get; set; }
        public string objectId { get; set; }
        public object deletionTimestamp { get; set; }
        public bool accountEnabled { get; set; }
        public object[] assignedLicenses { get; set; }
        public object[] assignedPlans { get; set; }
        public object city { get; set; }
        public object country { get; set; }
        public object department { get; set; }
        public object dirSyncEnabled { get; set; }
        public string displayName { get; set; }
        public object facsimileTelephoneNumber { get; set; }
        public object givenName { get; set; }
        public object immutableId { get; set; }
        public object jobTitle { get; set; }
        public object lastDirSyncTime { get; set; }
        public string mail { get; set; }
        public string mailNickname { get; set; }
        public object mobile { get; set; }
        public object onPremisesSecurityIdentifier { get; set; }
        public object[] otherMails { get; set; }
        public string passwordPolicies { get; set; }
        public object passwordProfile { get; set; }
        public object physicalDeliveryOfficeName { get; set; }
        public object postalCode { get; set; }
        public object preferredLanguage { get; set; }
        public object[] provisionedPlans { get; set; }
        public object[] provisioningErrors { get; set; }
        public string[] proxyAddresses { get; set; }
        public object sipProxyAddress { get; set; }
        public object state { get; set; }
        public object streetAddress { get; set; }
        public object surname { get; set; }
        public object telephoneNumber { get; set; }
        public object usageLocation { get; set; }
        public string userPrincipalName { get; set; }
        public string userType { get; set; }
    }

}