using Kefcon.Data;
using Kefcon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kefcon.Services
{
    public interface IGameService :IServiceBase<Game>
    {
    }

    public class GameService : ServiceBase<Game>, IGameService
    {
        public GameService(ApplicationDataContext context) : base(context)
        {
        }
    }
}
