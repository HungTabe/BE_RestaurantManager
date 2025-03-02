namespace BE_RestaurantManagement.Zalopay.Config
{
    public class ZaloPayConfig
    {
        public static string ConfigName => "ZaloPay"; 
        public string AppUser { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string IpnUrl { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
        public string Keyl { get; set; } = string.Empty;
        public string Key2 { get; set; } = string.Empty;
    }
}
