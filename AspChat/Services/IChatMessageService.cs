using System.Threading.Tasks;
using System.Web.WebSockets;

namespace AspChat.Services {
    public interface IChatMessageService {
        Task WebSocketRequest(AspNetWebSocketContext context);
    }
}
