using BE_RestaurantManagement.Helpers;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BE_RestaurantManagement.Zalopay.Request
{
    public class CreateZalopayPayRequest
    {
        public CreateZalopayPayRequest(int appId, string appUser, long appTime, long amount, string appTransId, string bankCode, string description)
        {
            AppId = appId;
            AppUser = appUser;
            AppTime = appTime;
            Amount = amount;
            AppTransId = appTransId;
            BankCode = bankCode;
            Description = description;
        }
        public int AppId { get; set; }
        public string AppUser { get; set; } = string.Empty;
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string AppTransId { get; set; } = string.Empty;
        public string ReturnUrl { get; }
        public string EmbedData { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;
        public string BankCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public void MakeSignature(string key)
        {
            var data = AppId + "" + AppTransId + "/" + AppUser + "/" + Amount + "|" + AppTime + "/" + "]";
            this.Mac = HashHelper.HmacSHA256(data, key);
        }

        public Dictionary<string, string> GetContent()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("appid", AppId.ToString());
            keyValuePairs.Add("appuser", AppUser);
            keyValuePairs.Add("apptime", AppTime.ToString());
            keyValuePairs.Add("amount", Amount.ToString());
            keyValuePairs.Add("apptransid", AppTransId);
            keyValuePairs.Add("description", Description);
            keyValuePairs.Add("bankcode", "zalopayapp");
            keyValuePairs.Add("mac", Mac);

            return keyValuePairs;
        }
        }
}
