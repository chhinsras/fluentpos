namespace FluentPOS.Shared.Core.Settings
{
    public class SmsSettings
    {
        public string SmsAccountIdentification { get; set; }
        public string SmsAccountPassword { get; set; }
        public string SmsAccountFrom { get; set; }
        public bool EnableVerification { get; set; }
    }
}