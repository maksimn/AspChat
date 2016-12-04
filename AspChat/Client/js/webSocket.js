import store from "./store";

var host = window.location.host;
var serverChatHandler = "ChatHandler.ashx";
var wsProtocol = "ws";

var socket = new WebSocket(`${wsProtocol}://${host}/${serverChatHandler}`);

socket.onmessage = function (msg) {
    var trimChatMessage = msg.data.replace(/\0/g, '');
    var chatMsgObj = JSON.parse(trimChatMessage);

    store.dispatch({
        type: "CHAT_MESSAGE_RECEIVED",
        payload: {
            chatUserName: chatMsgObj.ChatUserName,
            chatMessageText: chatMsgObj.Text
        }
    })
};

socket.onclose = function () {
    alert('Соединение закрыто.');
}

socket.onerror = function () {
    alert('Произошла ошибка соединения с чатом.');
};

export default socket;
