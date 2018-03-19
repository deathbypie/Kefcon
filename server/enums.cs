using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}
