import React from "react";

import socket from "../../webSocket";

export default class ChatMessageInputForm extends React.Component {
    submitHandler(e) {
        e.preventDefault();
        const messageText = e.target.messageText.value;
        e.target.messageText.value = "";

        var chatMessage = {
            Type: 'ChatMessage',
            ChatUserName: this.props.userName,
            Text: messageText
        };
        socket.send(JSON.stringify(chatMessage));
    }
    render() {
        return (
            <div>
                <form action="/" method="POST" onSubmit={this.submitHandler.bind(this)}>
                    <h4>Вы вошли как {this.props.userName}</h4>
                    <input name="messageText" type="text" />
                    <input type="submit" value="Отправить" />
                </form>
            </div>
        );
    }
}