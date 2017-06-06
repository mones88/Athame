using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    public class ServiceMediaId
    {
        public string ServiceName { get; set; }
        public MediaType MediaType { get; set; }
        public string Id { get; set; }

        private const char Separator = '/';

        public static ServiceMediaId Parse(string id)
        {
            var result = id.Split(Separator);
            if (result.Length != 3)
            {
                throw new ArgumentException("id");
            }
            MediaType typeParseResult;
            if (!Enum.TryParse(result[1], out typeParseResult))
            {
                throw new ArgumentException("id");
            }
            return new ServiceMediaId
            {
                ServiceName = result[0],
                MediaType = typeParseResult,
                Id = result[2]
            };
        }

        public override string ToString()
        {
            return String.Join(Separator.ToString(), ServiceName, MediaType, Id);
        }
    }
}
