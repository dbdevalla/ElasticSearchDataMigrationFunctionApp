using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchDataMigrationFunctionApp.Models
{
    public class PatientVisitModel
    {


        public string TenantId { get; set; }

        public string PatientVisitId { get; set; }
        public string PatientAccountId { get; set; }

        public Patient Patient { get; set; }
        public Visit Visit { get; set; }

    }

    public class Patient
    {
        public Tenantpatientidentifier[] TenantPatientIdentifier { get; set; }
    }

    public class Visit
    {
        public string AccountNumber { get; set; }
        public string VisitNumber { get; set; }
    }

    public class Tenantpatientidentifier
    {
        public string TenantPatientId { get; set; }
        public string TenantPatientIdentifierType { get; set; }
        public string TenantPatientIdentifierDescription { get; set; }
    }
}
