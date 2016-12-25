import React from "react";
import { connect } from "react-redux";

import ChatMessageInputForm from "../../components/ChatMessageInputForm";
import ChatMessagesListView from "../../components/ChatMessagesListView";
import styles from "./ChatRoom.less";

export class ChatRoom extends React.Component {
    render() {
        const chatUserName = this.props.userName;
        return (
            <div>
                <div className={ styles.userSessionArea }>
                    <div className={ styles.userName }>
                        {chatUserName}
                    </div>
                    <form className={ styles.logOutForm } 
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
