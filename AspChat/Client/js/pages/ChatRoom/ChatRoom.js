import React from "react";
import { connect } from "react-redux";
import { Link } from "react-router";

import ChatMessageInputForm from "../../components/ChatMessageInputForm";
import ChatMessagesListView from "../../components/ChatMessagesListView";
import styles from "./ChatRoom.css";

function deleteAllCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}

export class ChatRoom extends React.Component {
    render() {
        const chatUserName = this.props.userName;
        return (
            <div>
                <div className={ styles.userSessionArea }>
                    <div className={ styles.userName }>
                        {chatUserName}
                    </div>
                    <form onSubmit={deleteAllCookies} 
                          className={ styles.logOutForm } 
                          method="POST" 
                          action="logout">
                        <input type="submit" value="Выйти" />
                    </form>
                </div>
                <main className={ styles.chatRoom }>
                    <div className={ styles.chatRoomHeader }></div>

                    <ChatMessagesListView appState={this.props} />
                    
                    <ChatMessageInputForm userName={chatUserName} />
                </main>
            </div>
        );
    }
}

export default connect((store) => {
    return {
        userName: store.userName,
        chatMessages: store.chatMessages
    };
})(ChatRoom);
