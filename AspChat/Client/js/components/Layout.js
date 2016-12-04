import React from "react";
import { connect } from "react-redux"

import ChatMessageInputForm from "./ChatMessageInputForm";
import ChatMessagesListView from "./ChatMessagesListView";

import store from "../store";

var host = window.location.host;
var serverChatHandler = "ChatHandler.ashx";
var wsProtocol = "ws";

var socket = new WebSocket(`${wsProtocol}://${host}/${serverChatHandler}`);

var dispatch = store.dispatch;

socket.onmessage = function (msg) {
    var trimChatMessage = msg.data.replace(/\0/g, '');
    var chatMsgObj = JSON.parse(trimChatMessage);

    dispatch({
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

function sendChatMessage(msgObj) {
    var chatMessage = {
        Type: 'ChatMessage',
        ChatUserName: msgObj.chatUserName,
        Text: msgObj.chatMessageText
    };
    socket.send(JSON.stringify(chatMessage));
}

window.onunload = function () {
    function deleteCookie() {
        document.cookie = "id=; expires=Thu, 01 Jan 1970 00:00:00 UTC";
    }
    deleteCookie();
}    

export class Layout extends React.Component {
    render() {
        const chatUserName = this.props.userName;
        return (
            <div>
                <ChatMessageInputForm userName={chatUserName} 
                                      addChatMessage={sendChatMessage} />
                <ChatMessagesListView userName={chatUserName}
                                      appState={this.props} />
            </div>
        );
    }
}

export default connect((store) => {
    return {
        userName: store.userName,
        chatMessages: store.chatMessages
    };
})(Layout);
