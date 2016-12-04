import React from "react";
import { connect } from "react-redux"

import ChatMessageInputForm from "./ChatMessageInputForm";
import ChatMessagesListView from "./ChatMessagesListView";

import socket from "../webSocket";

export class Layout extends React.Component {
    sendChatMessage(msgObj) {
        var chatMessage = {
            Type: 'ChatMessage',
            ChatUserName: msgObj.chatUserName,
            Text: msgObj.chatMessageText
        };
        socket.send(JSON.stringify(chatMessage));
    }

    render() {
        const chatUserName = this.props.userName;
        return (
            <div>
                <ChatMessageInputForm userName={chatUserName} 
                                      addChatMessage={this.sendChatMessage.bind(this)} />
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
