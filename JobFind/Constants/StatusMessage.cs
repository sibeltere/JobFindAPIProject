using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFind.Constants
{
    public static class StatusMessage
    {
        public const string SUCCESS = "İşlem Başarılı.";
        public const string HAS_EXCEPTION = "İşlem Başarısız!";
        public const string ALREADY_HASEMAIL = "Bu e-mail adresiyle kayıtlı bir kullanıcı bulunmaktadır!";
        public const string USER_NOTFOUND = "Kullanıcı bulunmamaktadır";
        public const string ALREADY_HASCV = "Kullanıcının yalnızca bir adet CV'si oluşturulabilir";
        public const string FIRM_NOTFOUND = "Firma bulunamadı!";
    }
}
