import React from "react";
import { connect } from "react-redux"

import ChatMessageInputForm from "../ChatMessageInputForm";
import ChatMessagesListView from "../ChatMessagesListView";
import styles from "./Layout.css";

export class Layout extends React.Component {
    render() {
        const chatUserName = this.props.userName;
        return (
            <div className={ styles.body }>
                <header className={ styles.header }>
                    <div className={ styles.userSessionArea }>
                        <div className={ styles.userName }>
                            {chatUserName}
                        </div>
                        <div className={ styles.userMenuArrowWrapper }>
                            <div className={ styles.userMenuArrow }></div>
                        </div>  
                    </div>
                </header>
                <ChatMessageInputForm userName={chatUserName} />
                <ChatMessagesListView appState={this.props} />
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
