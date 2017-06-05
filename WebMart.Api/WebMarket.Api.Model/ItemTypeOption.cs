using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WebMarket.Api.Model
{
    public enum ItemTypeOption
    {
        eAudio,
        eBook,
        Cd,
        Dvd,
        [EnumMember(Value = "MP3 CD")]  
        Mp3cd,
        Playaway,
        Text,
        LargePrint,
        eMagazine
    }
}
