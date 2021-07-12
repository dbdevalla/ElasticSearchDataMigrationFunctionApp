using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchDataMigrationFunctionApp
{
    public class ElasticSearchDBModel
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string TenantID { get; set; }
        public string PatientVisitId { get; set; }

        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        [Text(Analyzer = "autocomplete", Name = nameof(PatientName))]
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string PatientIdentifierType { get; set; }
        public string AccountNumber { get; set; }
        public string VisitNumber { get; set; }

        [Text(Analyzer = "autocomplete", Name = nameof(DisplayPatientId))]
        public string DisplayPatientId { get; set; }

        [Text(Analyzer = "autocomplete", Name = nameof(DisplayPatientAccountId))]
        public string DisplayPatientAccountId { get; set; }

        [Text(Analyzer = "autocomplete", Name = nameof(AppointmentId))]
        public string AppointmentId { get; set; }

        public string PatientAccountId { get; set; }
        public DateTime? VisitDate { get; set; }

        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string SSN { get; set; }
        public Tenantpatientidentifier[] TenantPatientIdentifier { get; set; }
        public string Gender { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string Email { get; set; }


        public DateTime? DOB { get; set; }
        public string PatientTypeCode { get; set; }
        public string PatientTypeDescription { get; set; }
        public string PointofCareCode { get; set; }
        public string PointofCareDescription { get; set; }


        public string VisitService { get; set; }
        public Diagnosis[] Diagnosis { get; set; }
        public string[] AdmittingDiagnosisCodes { get; set; }
        public string[] Procedure { get; set; }
        public string AdmittingProviderName { get; set; }
        public string ReferringProviderName { get; set; }
        public string CurrentVisitStatusId { get; set; }
        public string CurrentVisitStatusDescription { get; set; }
        public string CurrentBalance { get; set; }
        public string Collected { get; set; }


        public string FinancialClassCode { get; set; }
        public string FinancialClassDescription { get; set; }

        public string PrimaryInsuranceSequence { get; set; }
        public string PrimaryInsurancePlanId { get; set; }
        public string PrimaryInsurancePlanName { get; set; }
        public string PrimaryInsuranceStatusId { get; set; }

        public string SecondaryInsuranceSequence { get; set; }
        public string SecondaryInsurancePlanId { get; set; }
        public string SecondaryInsurancePlanName { get; set; }
        public string SecondaryInsuranceStatusId { get; set; }

        public string TertiaryInsuranceSequence { get; set; }
        public string TertiaryInsurancePlanId { get; set; }
        public string TertiaryInsurancePlanName { get; set; }
        public string TertiaryInsuranceStatusId { get; set; }


        public string EventTypeCode { get; set; }
        public string EventTypeDescription { get; set; }


        public DateTime? EventDateTime { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }

        public Module[] Statuses { get; set; }

        public int? FinancialClearanceStatusId { get; set; }
        public DateTime? StatusEffectiveDate { get; set; }


        #region PostalConfirmation
        public DateTime? PostalConfirmationLastRunDateTime { get; set; }
        public string PostalConfirmationRunType { get; set; }
        public string PostalConfirmationRequestType { get; set; }
        #endregion

        #region IdentityVerifier
        public DateTime? IdentityVerifierLastRunDateTime { get; set; }
        public string IdentityVerifierRunType { get; set; }
        public string IdentityVerifierRequestType { get; set; }
        #endregion

        public ModuleRuleAlerts[] ModuleRuleAlerts { get; set; }

        #region ServiceTracker
        public string ServiceDepartmentId { get; set; }
        public string ServiceDepartmentDescription { get; set; }
        public string LobbyId { get; set; }
        public string Lobbydescription { get; set; }
        public string IntakeStatus { get; set; }

        #endregion


        #region PaymentChange
        public string PaymentChargeTypeRefId { get; set; }
        public string PaymentChargeTypeRefDescription { get; set; }
        public string PaymentChargeAmount { get; set; }
        public string AmountCollected { get; set; }
        public string RemainingBalance { get; set; }

        #endregion


        #region ProspensityToPay
        public DateTime? PropensityToPayLastRunDateTime { get; set; }
        public string PropensityToPayRunType { get; set; }
        public string PropensityToPayRequestType { get; set; }
        public string PropensityToPayStatus { get; set; }

        #endregion


        #region MedicalNecessity
        public DateTime? MedicalNecessityLastRunDateTime { get; set; }
        public string MedicalNecessityRunType { get; set; }
        public string MedicalNecessityPayerRuleSetCode { get; set; }
        public string MedicalNecessityPayerRuleSetDescription { get; set; }
        public string MedicalNecessityStatus { get; set; }
        #endregion
    }

    public class Tenantpatientidentifier
    {
        public string TenantPatientId { get; set; }
        public string TenantPatientIdentifierType { get; set; }
        public string TenantPatientIdentifierDescription { get; set; }
    }

    public class Diagnosis
    {
        public string DiagnosisType { get; set; }
        public string DiagnosisCode { get; set; }

    }

    public class Module
    {
        public string ModuleRefid { get; set; }
        public string ModuleStatusRefId { get; set; }
        public string ModuleRefDescription { get; set; }
        public DateTime? EffectiveDate { get; set; }
    }

    public class ModuleRuleAlerts
    {
        public string ModuleRefId { get; set; }
        public string InsuranceSequenceNumber { get; set; }
        public RuleAlerts[] RuleAlerts { get; set; }
    }

    public class RuleAlerts
    {
        public string PELITASRuleId { get; set; }
        public string RuleId { get; set; }
        public string DisplayId { get; set; }
        public DateTime? RunDateTime { get; set; }
        public string RuleCategoryid { get; set; }
        public string RuleCategoryDescription { get; set; }
        public string RuleTitle { get; set; }
        public string ErrorCategoryID { get; set; }
        public string ErrorCategoryDescription { get; set; }
        public string ErrorCategoryWeight { get; set; }
        public string ErrorCategoryModuleStatusRefID { get; set; }
        public string ErrorCategoryModuleStatusDescription { get; set; }
        public RuleAlertEmployee[] RuleAlertEmployee { get; set; }

    }

    public class RuleAlertEmployee
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAdministrationDepartmentId { get; set; }
        public string EmployeeAdministrationDepartmentName { get; set; }
        public EmployeeChallenge EmployeeChallenge { get; set; }
    }

    public class EmployeeChallenge
    {
        public string ChallengeStatusRefId { get; set; }
        public string ChallengeStatusDescription { get; set; }
        public string ChallengeReasonID { get; set; }
        public string ChallengeReasonDescription { get; set; }
        public DateTime? ChallengeResponseDateTime { get; set; }
        public string ChallengeResponseID { get; set; }
        public string ChallengeResponseDesciption { get; set; }
        public string ChallengeResponseEmployeeId { get; set; }
        public string ChallengeResponseEmployeeFirstName { get; set; }
        public string ChallengeResponseEmployeeLastName { get; set; }
    }

}
