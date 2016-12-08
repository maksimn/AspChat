import React from "react";

import socket from "../../webSocket";
import styles from "./ChatMessageInputForm.css";

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
            <div className={ styles.chatMessageInput }>
                <form className={ styles.chatMessageInputForm } 
                      action="/" method="POST" onSubmit={this.submitHandler.bind(this)}>
                    <textarea className={ styles.messageTextBox } name="messageText"></textarea>
                    <input type="submit" value="Отпр" />
                </form>
            </div>
        );
    }
}
