import React from "react";

import socket from "../../webSocket";
import styles from "./ChatMessageInputForm.css";

export default class ChatMessageInputForm extends React.Component {
    sendMessage(e) {
        if(e.key === 'Enter') {
            const textArea = e.target;
            const messageText = textArea.value;

            var chatMessage = {
                Type: 'ChatMessage',
                ChatUserName: this.props.userName,
                Text: messageText
            };
            socket.send(JSON.stringify(chatMessage));
        }
    }

    clearTextArea(e) {
        if(e.key === 'Enter') {
            e.target.value = "";
        }
    }
    
    render() {
        return (
            <div className={ styles.chatMessageInput }>
                <textarea onKeyPress={ this.sendMessage.bind(this) } 
                          onKeyUp={ this.clearTextArea }
                          className={ styles.messageTextBox }
                          placeholder="Напишите сообщение...">
                </textarea>
            </div>
        );
    }
}
