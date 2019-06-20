using System;

namespace WhatsAppBot.Models
{
    public class OrderResponse
    {
        public int IncId { get; set; }
        public int SqlId { get; set; }
        public string Name { get; set; }
        public object CustomerOrderReference { get; set; }
        public string ProjectName { get; set; }
        public string ReceivingLabName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ValidatedOn { get; set; }
        public string ValidatedBy { get; set; }
        public object ImportedOn { get; set; }
        public string Status { get; set; }
        public object Id { get; set; }
        public string Account { get; set; }
        public string EOLOrderCode { get; set; }
        public string AccountCode { get; set; }
        public string EleCode { get; set; }
        public string ContractCode { get; set; }
        public bool IsOsDeleted { get; set; }
        public object IntercoEleCode { get; set; }
        public string AcfCode { get; set; }
        public bool IsOrderFromSubmissionForm { get; set; }
        public string OrderOs { get; set; }
        public bool AccountHasOs { get; set; }
        public bool IsOrderOsAvailableForAccount { get; set; }
    }
}