using System;
using System.Security.Cryptography;

namespace R3M_User_Domain
{
    public class Token
    {
        public int IdToken { get; set; }
        public int IdUsuario { get; set; }
        public string TokenValue { get; set; }
        public DateTime DtInsercao { get; set; }
        public DateTime DtExpiracao { get; set; }

        public void SetTokenValue()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[24];
                rng.GetBytes(tokenData);

                TokenValue = Convert.ToBase64String(tokenData);
            }
        }
    }
}